using System;
using System.Collections.Generic;

namespace recruitment_process_portal_server.Services.Rules;

public static class RoleTransitionRules
{

    private static readonly Dictionary<string, HashSet<(string From, string To)>> Rules
        = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Recruiter"] = new()
            {
                ("Applied", "Screening"),
                ("Screening", "Interview"),
                ("Applied", "Rejected"),
                ("Screening", "Rejected")
            },

            ["Interviewer"] = new()
            {
                ("Interview", "Interview Completed")
            },

            ["HR"] = new()
            {
                ("Offered", "Offer Accepted"),
                ("Offered", "Offer Rejected"),
                ("Offer Accepted", "Hired"),
                ("Any", "On Hold")
            },

            // Explicit admin override
            ["Admin"] = new()
            {
                ("Any", "Any")
            }
        };

    public static bool IsAllowed(
        string roleName,
        string fromStatus,
        string toStatus)
    {
        if (string.IsNullOrWhiteSpace(roleName) ||
            string.IsNullOrWhiteSpace(fromStatus) ||
            string.IsNullOrWhiteSpace(toStatus))
            return false;

        if (!Rules.TryGetValue(roleName.Trim(), out var allowedTransitions))
            return false;

        fromStatus = fromStatus.Trim();
        toStatus = toStatus.Trim();

        // Admin-style override
        if (allowedTransitions.Contains(("Any", "Any")))
            return true;

        if (allowedTransitions.Contains(("Any", toStatus)))
            return true;

        if (allowedTransitions.Contains((fromStatus, "Any")))
            return true;

        return allowedTransitions.Contains((fromStatus, toStatus));
    }
}
