# Cybersecurity Awareness Chatbot — Part 3

A C# Windows Forms desktop application extending the chatbot with a MySQL task assistant, cybersecurity quiz, simulated NLP intent recognition, and timestamped activity history.

## Requirements
- Windows 10/11
- .NET 10 SDK
- MySQL 8 or compatible server

## Run
1. Execute [database/schema.sql](database/schema.sql) in MySQL.
2. Set environment variable `CYBERBOT_MYSQL`, for example: `Server=localhost;Database=cyber_awareness;User ID=your_user;Password=your_password;`
3. Open `CyberSecurityAwarenessChatbot.csproj` in Visual Studio 2026 or a compatible .NET 10 environment, restore NuGet packages, and run.

The chat and quiz work without MySQL; task operations require a configured database.

## Features
- Windows Forms chat interface and quick-topic buttons.
- Cybersecurity guidance covering phishing, passwords, malware, privacy, and suspicious links.
- NLP simulation using text normalization, keyword matching, and regular expressions.
- MySQL-backed task creation, listing, reminders, completion, and deletion.
- Twelve quiz questions with immediate explanations and final scoring.
- Timestamped activity log showing up to ten recent events.

## Project structure
- `MainForm.cs`: main chat interface and intent orchestration.
- `ChatbotEngine.cs`: knowledge responses and conversational handling.
- `NlpIntentRouter.cs`: keyword/regex intent detection.
- `TaskRepository.cs`: parameterized MySQL data access.
- `TaskManagerForm.cs`: task management UI.
- `QuizForm.cs`: quiz flow and scoring.
- `ActivityLog.cs`: timestamped in-memory activity log.
- `database/schema.sql`: database setup.

## Notes
The activity log is session-based. Never commit database passwords or secrets; use an environment variable. This is the separate Part 3 repository; Part 2 is not modified.

## Presentation
Add the unlisted YouTube presentation URL after recording and uploading the voice-over demonstration.

## Part 2 functionality carried forward

The Part 3 chatbot retains the core Part 2 conversational behaviour:
- A welcome/name prompt and in-session name memory.
- Multiple varied responses for phishing, password safety, suspicious links, malware, privacy and scams.
- Sentiment-aware replies for worried, anxious, confused, overwhelmed and curious users.
- Contextual follow-up prompts such as "tell me more" and "another tip".
- An optional voice-greeting player. If the original `Assets/greeting.wav` file is available, the project copies it to the output folder and plays it at startup. The binary greeting file still needs to be present in the repository's `Assets` folder for audio playback.

Part 2 source repository (read-only reference): https://github.com/mphorambau/CyberSecurityAwarenessChatbot-Part2

## Build verification

A Windows GitHub Actions workflow runs NuGet restore and a Release build on pushes and pull requests to `main`. It can also be started manually from the repository's **Actions** tab using **Windows .NET Build**. The workflow was configured in commit `6e28e9e`; check the Actions tab for the latest run result before treating the project as build-verified.
