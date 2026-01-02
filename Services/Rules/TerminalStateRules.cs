namespace recruitment_process_portal_server.Services;

public static class TerminalStateRules
{
    private static readonly HashSet<string> _terminalStatuses =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "Hired",
            "Rejected",
            "Withdrawn",
            "Closed"
        };

    public static bool IsTerminal(string statusName)
    {
        return _terminalStatuses.Contains(statusName);
    }
}
