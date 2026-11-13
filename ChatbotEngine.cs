namespace CyberSecurityAwarenessChatbot;

public sealed class ChatbotEngine
{
    private readonly NlpIntentRouter _router = new();
    private readonly Dictionary<string,string> _knowledge = new(StringComparer.OrdinalIgnoreCase)
    {
        ["phishing"]="Phishing uses deceptive messages to steal information. Check the sender, inspect links before opening them, and never share passwords or one-time codes.",
        ["password"]="Use a long, unique password for every account. A password manager and multi-factor authentication add important protection.",
        ["malware"]="Malware is harmful software. Keep devices updated, install trusted software only, and scan unexpected attachments before opening them.",
        ["privacy"]="Limit the personal information you share, review app permissions, use privacy settings, and be cautious with public Wi-Fi.",
        ["suspicious link"]="Do not click unexpected links. Verify the sender through another channel and navigate to the organisation's website yourself."
    };
    private string? _userName;
    private string? _lastTopic;
    private readonly Random _random = new();

    public ChatIntent DetectIntent(string input) => _router.Detect(input);

    public string Respond(string input)
    {
        var text = NlpIntentRouter.Normalize(input);
        if (string.IsNullOrWhiteSpace(text)) return "Please type a question and I'll do my best to help.";
        if (text.Contains("my name is")) { _userName = input[(input.IndexOf("my name is", StringComparison.OrdinalIgnoreCase)+10)..].Trim(); return $"Nice to meet you, {_userName}. I'll use your name in this chat."; }
        if (text.Contains("thank")) return Pick("You're welcome! Stay alert online.","Happy to help. Good security habits make a difference.");
        if (text.Contains("worried") || text.Contains("scared") || text.Contains("frustrated")) return "It's understandable to feel concerned. Take it one step at a time: pause, verify unexpected requests, and ask for help if something seems wrong.";
        foreach (var item in _knowledge)
            if (text.Contains(item.Key, StringComparison.OrdinalIgnoreCase)) { _lastTopic=item.Key; return item.Value; }
        if (text.Contains("more") && _lastTopic is not null) return _knowledge[_lastTopic] + " Remember: pause before acting, verify the source, and report anything suspicious.";
        if (text.Contains("hello") || text.Contains("hi")) return _userName is null ? "Hello! I'm your Cybersecurity Awareness Assistant. What would you like to learn about?" : $"Hello again, {_userName}! What can I help you with?";
        return Pick("I can help with phishing, passwords, malware, privacy, suspicious links, tasks, reminders, the quiz, or the activity log.",
                    "Let's keep your digital life safer. Ask me about passwords, phishing, malware, privacy, or say 'start quiz'.");
    }

    private string Pick(params string[] options) => options[_random.Next(options.Length)];
}