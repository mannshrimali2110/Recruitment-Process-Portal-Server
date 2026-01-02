namespace recruitment_process_portal_server.Services;

public static class RolePermissionRules
{
    private static readonly Dictionary<string, HashSet<string>> _permissions
        = new()
        {
            ["Recruiter"] = new()
            {
                "CreateApplication",
                "ChangeStatus",
                "ScheduleInterview",
                "CreateOffer",
                "RejectApplication",
                "PutOnHold"
            },
            ["Interviewer"] = new()
            {
                "AddScreeningFeedback",
                "CompleteInterview"
            },
            ["HiringManager"] = new()
            {
                "CreateOffer",
                "MarkHired"
            },
            ["Admin"] = new()
            {
                "*" // full access
            }
        };

    public static bool CanPerform(string roleName, string action)
    {
        if (!_permissions.ContainsKey(roleName))
            return false;

        var allowed = _permissions[roleName];
        return allowed.Contains("*") || allowed.Contains(action);
    }
}
