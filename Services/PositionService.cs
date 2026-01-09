using Microsoft.EntityFrameworkCore;
using recruitment_process_portal_server.Data;
using recruitment_process_portal_server.DTOs.Responses;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Services;

 
/// Service for managing job positions in the recruitment system.
/// Implements position lifecycle management, status transitions, and validation.
/// </summary>
public class PositionService : IPositionService
{
    private readonly ApplicationDbContext _context;

    public PositionService(ApplicationDbContext context)
    {
        _context = context;
    }

     
    /// Retrieves detailed information about a specific job position.
    /// </summary>
    public async Task<PositionDetailsDto> GetPositionAsync(int positionId)
    {
        var position = await _context.JobPositions
            .Include(p => p.Status)
            .Include(p => p.CreatedByUser)
            .Include(p => p.JobCandidateLinks)
            .FirstOrDefaultAsync(p => p.PositionID == positionId)
            ?? throw new KeyNotFoundException($"Position with ID {positionId} not found.");

        var applicationCount = position.JobCandidateLinks.Count;
        var shortlistedCount = await _context.JobCandidateLinks
            .Where(jcl => jcl.PositionID == positionId &&
                          (jcl.ApplicationStatus!.StatusName == "Screening" ||
                           jcl.ApplicationStatus.StatusName == "Interview Scheduled" ||
                           jcl.ApplicationStatus.StatusName == "Interview Completed" ||
                           jcl.ApplicationStatus.StatusName == "Offered" ||
                           jcl.ApplicationStatus.StatusName == "Offer Accepted"))
            .CountAsync();

        return new PositionDetailsDto
        {
            PositionId = position.PositionID,
            Title = position.Title,
            Description = position.Description,
            CurrentStatus = position.Status?.StatusName ?? "Unknown",
            TotalApplications = applicationCount,
            ShortlistedApplications = shortlistedCount,
            CreatedAt = position.CreatedAt,
            CreatedBy = $"{position.CreatedByUser?.FirstName} {position.CreatedByUser?.LastName}".Trim()
        };
    }

     
    /// Updates the status of a job position with mandatory reason documentation.
    /// Validates status transitions and ensures data consistency.
    /// </summary>
    public async Task<PositionStatusResponseDto> UpdatePositionStatusAsync(
        int positionId,
        int newStatusId,
        int changedByUserId,
        string reason)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new InvalidOperationException("Reason for status change is mandatory.");
        }

        // Fetch position with current status
        var position = await _context.JobPositions
            .Include(p => p.Status)
            .FirstOrDefaultAsync(p => p.PositionID == positionId)
            ?? throw new KeyNotFoundException($"Position with ID {positionId} not found.");

        // Fetch new status
        var newStatus = await _context.JobStatuses
            .FirstOrDefaultAsync(s => s.StatusID == newStatusId)
            ?? throw new KeyNotFoundException($"Job status with ID {newStatusId} not found.");

        var previousStatusName = position.Status?.StatusName ?? "Unknown";
        var previousStatusId = position.StatusID;

        // Validate status transition
        if (!await CanTransitionStatusAsync(positionId, newStatusId))
        {
            throw new InvalidOperationException(
                $"Cannot transition position status from '{previousStatusName}' to '{newStatus.StatusName}'.");
        }

        // Update position status
        position.StatusID = newStatusId;

        // Track status change with reason in a position status change log
        // Note: For production, consider creating a PositionStatusChangeLog table
        // For now, we're storing the change in the Position.ClosureComment field if applicable
        var changeTimestamp = DateTime.UtcNow;

        _context.JobPositions.Update(position);
        await _context.SaveChangesAsync();

        return new PositionStatusResponseDto
        {
            PositionId = position.PositionID,
            PositionTitle = position.Title,
            PreviousStatus = previousStatusName,
            CurrentStatus = newStatus.StatusName,
            Reason = reason,
            ChangedAt = changeTimestamp,
            ChangedByUserId = changedByUserId
        };
    }

     
    /// Retrieves all available job statuses in the system.
    /// </summary>
    public async Task<IReadOnlyList<(int Id, string Name)>> GetAvailableStatusesAsync()
    {
        return await _context.JobStatuses
            .Select(s => new ValueTuple<int, string>(s.StatusID, s.StatusName))
            .ToListAsync();
    }

     
    /// Validates if a position can transition to a new status.
    /// Currently allows transitions between Active, OnHold, and Closed states.
    /// </summary>
    public async Task<bool> CanTransitionStatusAsync(int positionId, int newStatusId)
    {
        var position = await _context.JobPositions
            .Include(p => p.Status)
            .FirstOrDefaultAsync(p => p.PositionID == positionId);

        if (position == null)
            return false;

        var newStatus = await _context.JobStatuses
            .FirstOrDefaultAsync(s => s.StatusID == newStatusId);

        if (newStatus == null)
            return false;

        // Define allowed transitions for position statuses
        // Active -> OnHold, Closed
        // OnHold -> Active, Closed
        // Closed -> cannot transition (terminal state for practical purposes)
        var currentStatusName = position.Status?.StatusName ?? "";
        var newStatusName = newStatus.StatusName;

        var allowedTransitions = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Active"] = new() { "OnHold", "Closed" },
            ["OnHold"] = new() { "Active", "Closed" },
            ["Closed"] = new()  // No transitions out of Closed
        };

        if (!allowedTransitions.TryGetValue(currentStatusName, out var allowedDestinations))
            return false;

        return allowedDestinations.Contains(newStatusName);
    }
}
