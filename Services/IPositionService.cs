using recruitment_process_portal_server.DTOs.Responses;

namespace recruitment_process_portal_server.Services;

 
/// Service interface for managing job positions in the recruitment system.
/// Handles position lifecycle, status transitions, and related operations.
/// </summary>
public interface IPositionService
{
     
    /// Retrieves detailed information about a specific job position.
    /// </summary>
    /// <param name="positionId">The ID of the position to retrieve.</param>
    /// <returns>Position details including current status and application count.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when position does not exist.</exception>
    Task<PositionDetailsDto> GetPositionAsync(int positionId);

     
    /// Updates the status of a job position (e.g., Active, OnHold, Closed).
    /// </summary>
    /// <param name="positionId">The ID of the position to update.</param>
    /// <param name="newStatusId">The new status ID for the position.</param>
    /// <param name="changedByUserId">The ID of the user making the change.</param>
    /// <param name="reason">Mandatory reason for the status change.</param>
    /// <returns>Response DTO containing change details.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when position or status does not exist.</exception>
    /// <exception cref="InvalidOperationException">Thrown when status change is invalid (e.g., invalid transition).</exception>
    Task<PositionStatusResponseDto> UpdatePositionStatusAsync(
        int positionId,
        int newStatusId,
        int changedByUserId,
        string reason);

     
    /// Retrieves all available job statuses in the system.
    /// </summary>
    /// <returns>List of available status IDs and names.</returns>
    Task<IReadOnlyList<(int Id, string Name)>> GetAvailableStatusesAsync();

     
    /// Checks if a position can be transitioned to a new status.
    /// </summary>
    /// <param name="positionId">The ID of the position.</param>
    /// <param name="newStatusId">The proposed new status ID.</param>
    /// <returns>True if transition is valid; false otherwise.</returns>
    Task<bool> CanTransitionStatusAsync(int positionId, int newStatusId);
}
