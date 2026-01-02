namespace recruitment_process_portal_server.Services;

// 1 = Applied
// 2 = Screening
// 3 = Interview Scheduled
// 4 = Interview Completed
// 5 = Offer Created
// 6 = Offer Accepted
// 7 = Offer Rejected
// 8 = Hired
// 9 = On Hold
// 10 = Rejected
// 11 = Closed

public static class StatusTransitionRules
{
    private static readonly Dictionary<int, HashSet<int>> _allowedTransitions
        = new()
        {
            // map to ApplicationStatus table
            { 1, new() { 2, 9, 10 } }, // Applied
            { 2, new() { 3, 9 } },     // Screening
            { 3, new() { 4, 9 } },     // InterviewScheduled
            { 4, new() { 5, 10 } },    // InterviewCompleted
            { 5, new() { 6, 7 } },     // OfferCreated
            { 6, new() { 8 } },        // OfferAccepted
            { 7, new() { 11 } },       // OfferRejected
        };

    public static bool IsValidTransition(int fromStatusId, int toStatusId)
    {
        return _allowedTransitions.TryGetValue(fromStatusId, out var allowed)
               && allowed.Contains(toStatusId);
    }
}
