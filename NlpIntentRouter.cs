using System.Text.RegularExpressions;

namespace CyberSecurityAwarenessChatbot;

public enum ChatIntent { TaskAdd, TaskList, TaskDelete, TaskComplete, Quiz, Activity, Reminder, Cybersecurity, Unknown }

public sealed class NlpIntentRouter
{
    private static readonly (ChatIntent Intent, string[] Terms)[] Rules =
    [
        (ChatIntent.TaskAdd, ["add task", "create task", "new task", "remind me to", "remember to"]),
        (ChatIntent.TaskList, ["list tasks", "show tasks", "my tasks", "view tasks"]),
        (ChatIntent.TaskDelete, ["delete task", "remove task"]),
        (ChatIntent.TaskComplete, ["complete task", "finish task", "mark task"]),
        (ChatIntent.Quiz, ["quiz", "test me", "cyber challenge"]),
        (ChatIntent.Activity, ["activity log", "recent activity", "what have i done"]),
        (ChatIntent.Reminder, ["reminder", "remind me", "when is"]),
        (ChatIntent.Cybersecurity, ["phishing", "password", "malware", "privacy", "suspicious link", "ransomware", "two-factor"])
    ];

    public ChatIntent Detect(string input)
    {
        var text = Normalize(input);
        foreach (var rule in Rules)
            if (rule.Terms.Any(term => text.Contains(term, StringComparison.OrdinalIgnoreCase)))
                return rule.Intent;
        if (Regex.IsMatch(text, @"\b(task|todo)\b", RegexOptions.IgnoreCase)) return ChatIntent.TaskList;
        return ChatIntent.Unknown;
    }

    public static string Normalize(string value) =>
        Regex.Replace(value.Trim().ToLowerInvariant(), @"[^a-z0-9\s'-]", " ");
}