using Microsoft.EntityFrameworkCore;
using recruitment_process_portal_server.Data;
using recruitment_process_portal_server.DTOs;
using recruitment_process_portal_server.Models;
using recruitment_process_portal_server.Services.Rules;

namespace recruitment_process_portal_server.Services;

public class ApplicationService : IApplicationService
{
    private readonly ApplicationDbContext _context;

    public ApplicationService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Internal helpers

    private async Task EnsureNotTerminalAsync(JobCandidateLink link)
    {
        var statusName = await _context.ApplicationStatuses
            .Where(s => s.StatusID == link.CurrentStatusID)
            .Select(s => s.StatusName)
            .SingleAsync();

        if (TerminalStateRules.IsTerminal(statusName))
        {
            throw new InvalidOperationException(
                $"Application is in terminal state '{statusName}' and cannot be modified.");
        }
    }

    private async Task<int> GetStatusIdByNameAsync(string statusName)
    {
        return await _context.ApplicationStatuses
            .Where(s => s.StatusName == statusName)
            .Select(s => s.StatusID)
            .SingleAsync();
    }

    // Application lifecycle

    public async Task<int> CreateApplicationAsync(
        int candidateId,
        int positionId,
        int createdByUserId)
    {
        var appliedStatusId = await GetStatusIdByNameAsync("Applied");

        var link = new JobCandidateLink
        {
            CandidateID = candidateId,
            PositionID = positionId,
            CurrentStatusID = appliedStatusId,
            LinkedByUserID = createdByUserId,
            LinkDate = DateTime.UtcNow
        };

        _context.JobCandidateLinks.Add(link);
        await _context.SaveChangesAsync();

        return link.LinkID;
    }

    public async Task ChangeApplicationStatusAsync(
    int linkId,
    int newStatusId,
    int changedByUserId,
    string? reason)
    {
        var link = await _context.JobCandidateLinks
            .SingleOrDefaultAsync(l => l.LinkID == linkId)
            ?? throw new InvalidOperationException("Application not found.");

        await EnsureNotTerminalAsync(link);

        if (!StatusTransitionRules.IsValidTransition(
                link.CurrentStatusID,
                newStatusId))
        {
            throw new InvalidOperationException(
                $"Invalid status transition from {link.CurrentStatusID} to {newStatusId}");
        }

        var user = await _context.AppUsers
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.UserID == changedByUserId)
            ?? throw new InvalidOperationException("User not found.");

        var userRole = user.Role?.RoleName
            ?? throw new InvalidOperationException("User has no role assigned.");

        var fromStatusName = await _context.ApplicationStatuses
            .Where(s => s.StatusID == link.CurrentStatusID)
            .Select(s => s.StatusName)
            .SingleAsync();

        var toStatusName = await _context.ApplicationStatuses
            .Where(s => s.StatusID == newStatusId)
            .Select(s => s.StatusName)
            .SingleAsync();

        // ROLE CHECK — BEFORE mutation
        if (!RoleTransitionRules.IsAllowed(
                userRole,
                fromStatusName,
                toStatusName))
        {
            throw new UnauthorizedAccessException(
                $"Role '{userRole}' is not allowed to change status from '{fromStatusName}' to '{toStatusName}'.");
        }

        // Now it is safe to mutate state
        var log = new StatusChangeLog
        {
            LinkID = linkId,
            OldStatusID = link.CurrentStatusID,
            NewStatusID = newStatusId,
            ChangedByUserID = changedByUserId,
            ChangedAt = DateTime.UtcNow
        };

        link.CurrentStatusID = newStatusId;

        _context.StatusChangeLogs.Add(log);
        await _context.SaveChangesAsync();
    }


    public async Task PutApplicationOnHoldAsync(
        int linkId,
        int changedByUserId,
        string reason)
    {
        var onHoldStatusId = await GetStatusIdByNameAsync("On Hold");

        await ChangeApplicationStatusAsync(
            linkId,
            onHoldStatusId,
            changedByUserId,
            reason);
    }

    public Task RejectApplicationAsync(
        int linkId,
        int rejectedStatusId,
        int changedByUserId,
        string? reason)
    {
        return ChangeApplicationStatusAsync(
            linkId,
            rejectedStatusId,
            changedByUserId,
            reason);
    }

    // Interface-required aliases

    public Task UpdateApplicationStatusAsync(
        int linkId,
        int newStatusId,
        int changedByUserId,
        string? reason)
    {
        return ChangeApplicationStatusAsync(
            linkId,
            newStatusId,
            changedByUserId,
            reason);
    }

    public Task<IReadOnlyList<ApplicationStatusLogDto>> GetApplicationStatusHistoryAsync(
        int linkId)
    {
        return GetStatusHistoryAsync(linkId);
    }

    // Screening

    public async Task AddScreeningFeedbackAsync(
        int linkId,
        int reviewerUserId,
        string comments,
        string decision)
    {
        var link = await _context.JobCandidateLinks
            .SingleOrDefaultAsync(l => l.LinkID == linkId)
            ?? throw new InvalidOperationException("Application not found.");

        await EnsureNotTerminalAsync(link);

        var feedback = new ScreeningFeedback
        {
            LinkID = linkId,
            ReviewerUserID = reviewerUserId,
            Comments = comments,
            ScreeningDecision = decision,
            ReviewDate = DateTime.UtcNow
        };

        _context.ScreeningFeedbacks.Add(feedback);
        await _context.SaveChangesAsync();
    }

    public Task CompleteScreeningAsync(
        int linkId,
        int nextStatusId,
        int changedByUserId)
    {
        return ChangeApplicationStatusAsync(
            linkId,
            nextStatusId,
            changedByUserId,
            "Screening completed");
    }

    // Interviews

    public async Task<int> ScheduleInterviewAsync(
        int linkId,
        int interviewTypeId,
        int scheduledByUserId,
        DateTime start,
        DateTime end,
        string? meetingLink)
    {
        var link = await _context.JobCandidateLinks
            .SingleOrDefaultAsync(l => l.LinkID == linkId)
            ?? throw new InvalidOperationException("Application not found.");

        await EnsureNotTerminalAsync(link);

        var interview = new InterviewSchedule
        {
            LinkID = linkId,
            TypeID = interviewTypeId,
            RoundNumber = 1,
            ScheduledStart = start,
            ScheduledEnd = end,
            MeetingLink = meetingLink,
            IsCompleted = false
        };

        _context.InterviewSchedules.Add(interview);
        await _context.SaveChangesAsync();

        return interview.InterviewID;
    }

    public async Task CompleteInterviewAsync(
        int interviewId,
        int changedByUserId)
    {
        var interview = await _context.InterviewSchedules
            .SingleOrDefaultAsync(i => i.InterviewID == interviewId)
            ?? throw new InvalidOperationException("Interview not found.");

        var link = await _context.JobCandidateLinks
            .SingleOrDefaultAsync(l => l.LinkID == interview.LinkID)
            ?? throw new InvalidOperationException("Application not found.");

        await EnsureNotTerminalAsync(link);

        interview.IsCompleted = true;
        await _context.SaveChangesAsync();
    }

    // Offers & hiring

    public async Task<int> CreateOfferAsync(
        int linkId,
        DateTime offerDate,
        decimal ctc,
        DateTime? joiningDate)
    {
        var link = await _context.JobCandidateLinks
            .SingleOrDefaultAsync(l => l.LinkID == linkId)
            ?? throw new InvalidOperationException("Application not found.");

        await EnsureNotTerminalAsync(link);

        var offer = new OfferLetter
        {
            LinkID = linkId,
            OfferDate = offerDate,
            JoiningDate = joiningDate,
            CTC = ctc,
            OfferStatus = "Offered"
        };

        _context.OfferLetters.Add(offer);
        await _context.SaveChangesAsync();

        return offer.OfferID;
    }

    public async Task UpdateOfferStatusAsync(
        int offerId,
        string newStatus,
        int updatedByUserId)
    {
        var offer = await _context.OfferLetters
            .SingleOrDefaultAsync(o => o.OfferID == offerId)
            ?? throw new InvalidOperationException("Offer not found.");

        var link = await _context.JobCandidateLinks
            .SingleOrDefaultAsync(l => l.LinkID == offer.LinkID)
            ?? throw new InvalidOperationException("Application not found.");

        await EnsureNotTerminalAsync(link);

        offer.OfferStatus = newStatus;
        await _context.SaveChangesAsync();
    }

    public async Task ConvertToEmployeeAsync(
        int linkId,
        DateTime joiningDate,
        string workEmail,
        int createdByUserId)
    {
        var link = await _context.JobCandidateLinks
            .SingleOrDefaultAsync(l => l.LinkID == linkId)
            ?? throw new InvalidOperationException("Application not found.");

        await EnsureNotTerminalAsync(link);

        var employee = new EmployeeRecord
        {
            CandidateID = link.CandidateID,
            PositionID = link.PositionID,
            DateOfJoining = joiningDate,
            WorkEmail = workEmail,
            WorkStatus = "Active"
        };

        _context.EmployeeRecords.Add(employee);
        await _context.SaveChangesAsync();
    }


    public async Task<ApplicationDetailsDto> GetApplicationDetailsAsync(int linkId)
    {
        var application = await _context.JobCandidateLinks
            .Where(l => l.LinkID == linkId)
            .Select(l => new
            {
                l.LinkID,
                CandidateName = l.CandidateProfile.FirstName + " " + l.CandidateProfile.LastName,
                l.CandidateProfile.Email,
                JobTitle = l.JobPosition.Title,
                CurrentStatus = l.ApplicationStatus.StatusName,
                AppliedOn = l.LinkDate
            })
            .SingleOrDefaultAsync();

        if (application == null)
            throw new InvalidOperationException("Application not found.");

        var history = await GetStatusHistoryAsync(linkId);

        return new ApplicationDetailsDto
        {
            LinkId = application.LinkID,
            CandidateName = application.CandidateName,
            JobTitle = application.JobTitle,
            CurrentStatus = application.CurrentStatus,
            AppliedOn = application.AppliedOn,
            StatusHistory = history
        };
    }

    public async Task<ApplicationSummaryDto> GetApplicationSummaryAsync(int linkId)
    {
        var dto = await _context.JobCandidateLinks
            .Where(l => l.LinkID == linkId)
            .Select(l => new ApplicationSummaryDto
            {
                LinkId = l.LinkID,
                CandidateName = l.CandidateProfile.FirstName + " " + l.CandidateProfile.LastName,
                JobTitle = l.JobPosition.Title,
                CurrentStatus = l.ApplicationStatus.StatusName,
                AppliedOn = l.LinkDate
            })
            .SingleOrDefaultAsync();

        return dto ?? throw new InvalidOperationException("Application not found.");
    }

    public async Task<IReadOnlyList<ApplicationSummaryDto>> GetApplicationsByPositionAsync(int positionId)
    {
        return await _context.JobCandidateLinks
            .Where(l => l.PositionID == positionId)
            .OrderByDescending(l => l.LinkDate)
            .Select(l => new ApplicationSummaryDto
            {
                LinkId = l.LinkID,
                CandidateName = l.CandidateProfile.FirstName + " " + l.CandidateProfile.LastName,
                JobTitle = l.JobPosition.Title,
                CurrentStatus = l.ApplicationStatus.StatusName,
                AppliedOn = l.LinkDate
            })
            .ToListAsync();
    }


    public async Task<IReadOnlyList<ApplicationStatusLogDto>> GetStatusHistoryAsync(int linkId)
    {
        return await _context.StatusChangeLogs
            .Where(l => l.LinkID == linkId)
            .OrderByDescending(l => l.ChangedAt)
            .Select(l => new ApplicationStatusLogDto
            {
                OldStatus = l.OldStatus.StatusName,
                NewStatus = l.NewStatus.StatusName,
                ChangedBy = l.ChangedByUser.FirstName + " " + l.ChangedByUser.LastName,
                ChangedAt = l.ChangedAt,
            })
            .ToListAsync();
    }

}
