using System.Media;
using System.Speech.Synthesis;

namespace CyberSecurityAwarenessChatbot;

/// <summary>
/// Plays the supplied recorded greeting when available. If it is missing,
/// uses the Windows text-to-speech voice so the welcome is still spoken.
/// </summary>
internal static class VoicePlayer
{
    private static SoundPlayer? _player;
    private static SpeechSynthesizer? _speech;

    public static bool TryPlayGreeting(string? fallbackText = "Welcome to the Cybersecurity Awareness Assistant. Stay safe online.")
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Assets", "greeting.wav");

        if (File.Exists(path))
        {
            try
            {
                _player = new SoundPlayer(path);
                _player.Load();
                _player.Play();
                return true;
            }
            catch
            {
                // Fall through to Windows speech synthesis if the recording cannot play.
            }
        }

        try
        {
            _speech?.Dispose();
            _speech = new SpeechSynthesizer();
            _speech.Volume = 100;
            _speech.Rate = 0;
            _speech.SpeakAsync(fallbackText ?? "Welcome to the Cybersecurity Awareness Assistant.");
            return true;
        }
        catch
        {
            return false;
        }
    }
}
