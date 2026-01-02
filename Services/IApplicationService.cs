using recruitment_process_portal_server.DTOs;

namespace recruitment_process_portal_server.Services
{
    public interface IApplicationService
    {
        // 1. Application Creation & Linking

        /// Links a candidate to a job position and creates an application.
        /// Initial status is typically "Applied" or "Screening".
        Task<int> CreateApplicationAsync(
            int positionId,
            int candidateId,
            int createdByUserId
        );


        // 2. Application Status Management

        /// Updates the current status of an application.
        /// Automatically logs the change in StatusChangeLog.
        Task UpdateApplicationStatusAsync(
            int applicationId,
            int newStatusId,
            int changedByUserId,
            string? reason = null
        );

        /// Puts an application on hold with a mandatory reason.
        Task PutApplicationOnHoldAsync(
            int applicationId,
            int changedByUserId,
            string holdReason
        );

        /// Rejects an application with a closure reason.
        Task RejectApplicationAsync(
            int applicationId,
            int changedByUserId,
            int closureReasonId,
            string? remarks = null
        );


        // 3. Screening Phase

        /// Records screening feedback by a reviewer.
        Task AddScreeningFeedbackAsync(
            int applicationId,
            int reviewerUserId,
            string comments,
            string screeningDecision
        );

        /// Marks screening as completed and moves to next status.
        Task CompleteScreeningAsync(
            int applicationId,
            int changedByUserId,
            int nextStatusId
        );


        // 4. Interview Lifecycle

        /// Schedules an interview round for an application.
        Task<int> ScheduleInterviewAsync(
            int applicationId,
            int interviewTypeId,
            int roundNumber,
            DateTime scheduledStart,
            DateTime scheduledEnd,
            string? meetingLink
        );

        /// Marks an interview as completed.
        Task CompleteInterviewAsync(
            int interviewId,
            int completedByUserId
        );


        // 5. Offer Management

        /// Creates an offer letter for a selected candidate.
        Task<int> CreateOfferAsync(
            int applicationId,
            DateTime offerDate,
            decimal ctc,
            DateTime? expectedJoiningDate
        );

        /// Updates offer status (Accepted / Rejected / On Hold).
        Task UpdateOfferStatusAsync(
            int offerId,
            string offerStatus,
            int updatedByUserId
        );


        // 6. Final Selection & Employee Conversion

        /// Marks a candidate as joined and creates an employee record.
        Task ConvertToEmployeeAsync(
            int applicationId,
            DateTime joiningDate,
            string workEmail,
            int processedByUserId
        );


        // 7. Query Operations (Read Models)

        /// Retrieves full application details including candidate,
        /// job, current status, interviews, and feedback.
        Task<ApplicationDetailsDto> GetApplicationDetailsAsync(
            int applicationId
        );

        /// Lists applications for a given job position.
        Task<IReadOnlyList<ApplicationSummaryDto>> GetApplicationsByPositionAsync(
            int positionId
        );

        /// Retrieves the complete status history of an application.
        Task<IReadOnlyList<ApplicationStatusLogDto>> GetApplicationStatusHistoryAsync(
            int applicationId
        );
        Task ChangeApplicationStatusAsync(
            int jobCandidateLinkId,
            int newStatusId,
            int changedByUserId,
            string? remarks = null
        );

        Task<ApplicationSummaryDto> GetApplicationSummaryAsync(int jobCandidateLinkId);

        Task<IReadOnlyList<ApplicationStatusLogDto>> GetStatusHistoryAsync(int jobCandidateLinkId);
    }
}
