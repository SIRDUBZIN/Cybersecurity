# Cybersecurity Awareness Bot 🤖

A console-based C# application that teaches basic cybersecurity concepts through an interactive menu. Built for the Programming 2A PRC module.

## Features

- 🎤 **Welcome audio** - Plays `welcome.wav` on startup using `System.Media.SoundPlayer`
- 🌈 **Colorized console UI** - Uses `Console.ForegroundColor` for better UX
- 🧑 **Personalized greeting** - Asks for user name and displays ASCII art
- 📚 **Topic menu** with 3 core lessons:
    1. **Password Safety** - Importance of strong, unique passwords
    2. **Phishing** - How to spot malicious emails/links  
    3. **Safe Browsing** - VPNs, HTTPS, avoiding suspicious links
- 🔁 **Input validation** - Menu loops until valid option 1-4 is entered
- 👋 **Exit message** - Clean goodbye on option 4

## Prerequisites

- .NET SDK 6.0 or later
- Windows OS for `System.Media.SoundPlayer` support
- `welcome.wav` file in the same directory as the `.exe`
Project Structure
CybersecurityAwareness/
├── Program.cs              # Main entry point + menu loop
├── CybersecurityBot.cs     # Bot UI, sound, ASCII art, messages  
├── Menu.cs                # Menu display + topic logic
├── welcome.wav            # Audio played on startup
└── README.md              # You are here
Add audio file
Place your welcome.wav in CybersecurityAwareness/bin/Debug/net6.0/ or update the path in PlayWelcomeSound()
1. **Clone the repo**
   ```bash
   FGHJK.....
   Known LimitationsSystem.Media.SoundPlayer only works on Windowswelcome.wav path is hardcoded
   - will throw if file missingMenu only accepts "1", "2", "3", "4" as stringsFuture Improvements Add more topics: Malware,
   Social Engineering, 2FA Cross-platform audio with NAudio or similar Load topics from JSON file instead of hardcoded
   Add quiz after each topic Embed welcome.wav as resource to avoid missing file
    errorsAuthorCreated for IIE Rosebank College Programming 2A PRC
2026LicenseMIT - Use and modify freely for educational purposes cd CybersecurityAwareness
