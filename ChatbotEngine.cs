using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot
{
    /// <summary>
    /// Stores user memory/recall data across the conversation.
    /// </summary>
    public class UserMemory
    {
        public string Name { get; set; } = string.Empty;
        public string FavouriteTopic { get; set; } = string.Empty;
        public List<string> TopicsDiscussed { get; set; } = new List<string>();
    }

    /// <summary>
    /// Core chatbot engine: keyword recognition, random responses,
    /// sentiment detection, memory, conversation flow, and error handling.
    /// </summary>
    public class ChatbotEngine
    {
        private readonly Random _random = new Random();
        private string _lastTopic = string.Empty;
        private readonly UserMemory _memory = new UserMemory();

        // ── Keyword → multiple responses (random selection) ──────────────────
        private readonly Dictionary<string, List<string>> _keywordResponses =
            new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["password"] = new List<string>
            {
                "Use strong, unique passwords for every account — at least 12 characters with a mix of letters, numbers, and symbols.",
                "Never reuse passwords across sites. A password manager can help you keep track of them securely.",
                "Enable two-factor authentication alongside a strong password for maximum account security.",
                "Avoid using personal details like birthdays or names in your passwords — hackers try these first."
            },
            ["phishing"] = new List<string>
            {
                "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                "Check the sender's email address carefully — phishing emails often use look-alike domains.",
                "Never click links in unsolicited emails. Go directly to the official website instead.",
                "Legitimate companies will never ask for your password or banking details via email."
            },
            ["scam"] = new List<string>
            {
                "If something sounds too good to be true, it usually is. Always verify before sharing personal info.",
                "Online scams often create urgency — take a breath and verify the source before acting.",
                "Report scams to your national cybersecurity authority to help protect others.",
                "Never send money or gift cards to someone you haven't verified in person."
            },
            ["privacy"] = new List<string>
            {
                "Review your social media privacy settings regularly to control who sees your information.",
                "Limit the personal data you share online — the less out there, the safer you are.",
                "Use a VPN on public Wi-Fi to keep your browsing private.",
                "Read privacy policies (especially the data-sharing sections) before signing up to new services."
            },
            ["malware"] = new List<string>
            {
                "Keep your antivirus software up to date to protect against the latest malware threats.",
                "Avoid downloading software from untrusted sources — always use official websites or app stores.",
                "Ransomware can encrypt all your files. Regular backups are your best defence.",
                "Never open email attachments from unknown senders — they may contain malicious code."
            },
            ["firewall"] = new List<string>
            {
                "A firewall monitors incoming and outgoing network traffic and blocks suspicious connections.",
                "Make sure your operating system's built-in firewall is enabled at all times.",
                "For extra protection, consider a hardware firewall or a reputable third-party software firewall."
            },
            ["vpn"] = new List<string>
            {
                "A VPN encrypts your internet traffic, hiding it from your ISP and potential eavesdroppers.",
                "Always use a VPN on public Wi-Fi networks such as coffee shops or airports.",
                "Choose a VPN provider with a strict no-logs policy to protect your privacy."
            },
            ["update"] = new List<string>
            {
                "Software updates often contain critical security patches — install them promptly.",
                "Enable automatic updates so you never miss an important security fix.",
                "Outdated software is one of the most common entry points for cybercriminals."
            },
            ["backup"] = new List<string>
            {
                "Follow the 3-2-1 backup rule: 3 copies, 2 different media types, 1 offsite.",
                "Test your backups regularly — a backup you can't restore is useless.",
                "Cloud backups combined with a local copy give you the best protection against ransomware."
            },
            ["wifi"] = new List<string>
            {
                "Avoid accessing sensitive accounts on public Wi-Fi. Use mobile data or a VPN instead.",
                "Change your home router's default password to something strong and unique.",
                "Use WPA3 encryption on your home Wi-Fi if your router supports it."
            }
        };

        // ── Sentiment word lists ──────────────────────────────────────────────
        private readonly List<string> _positiveWords = new List<string>
        {
            "great", "good", "thanks", "thank", "helpful", "awesome",
            "excellent", "love", "perfect", "brilliant", "nice", "happy"
        };

        private readonly List<string> _negativeWords = new List<string>
        {
            "worried", "scared", "confused", "frustrated", "angry",
            "upset", "concerned", "lost", "help", "hack", "hacked", "dangerous"
        };

        // ── Follow-up triggers ────────────────────────────────────────────────
        private readonly List<string> _followUpTriggers = new List<string>
        {
            "more", "another", "again", "explain", "tell me more",
            "give me more", "elaborate", "go on", "continue", "expand"
        };

        // ═════════════════════════════════════════════════════════════════════
        //  PUBLIC API
        // ═════════════════════════════════════════════════════════════════════

        /// <summary>Processes user input and returns a chatbot response string.</summary>
        public string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "I didn't catch that — could you type something?";

            string input = userInput.Trim();

            // 1. Check for name introduction
            string nameResponse = TryExtractName(input);
            if (nameResponse != null) return nameResponse;

            // 2. Check for favourite topic
            string topicResponse = TryExtractFavouriteTopic(input);
            if (topicResponse != null) return topicResponse;

            // 3. Check for follow-up requests
            if (IsFollowUp(input))
                return HandleFollowUp();

            // 4. Detect sentiment
            string sentiment = DetectSentiment(input);

            // 5. Match keyword
            string keyword = FindKeyword(input);
            if (keyword != null)
            {
                _lastTopic = keyword;
                if (!_memory.TopicsDiscussed.Contains(keyword))
                    _memory.TopicsDiscussed.Add(keyword);

                string tip = GetRandomResponse(keyword);
                return BuildPersonalisedResponse(sentiment, keyword, tip);
            }

            // 6. Greeting
            if (IsGreeting(input))
                return BuildGreeting();

            // 7. Ask for help
            if (input.ToLower().Contains("help") || input.ToLower().Contains("what can you do"))
                return BuildHelpMessage();

            // 8. Default / unknown input
            return "I'm not sure I understand. Can you try rephrasing? " +
                   "You can ask me about passwords, phishing, scams, privacy, malware, VPNs, firewalls, updates, backups, or Wi-Fi safety.";
        }

        public UserMemory Memory => _memory;

        // ═════════════════════════════════════════════════════════════════════
        //  PRIVATE HELPERS
        // ═════════════════════════════════════════════════════════════════════

        private string TryExtractName(string input)
        {
            string lower = input.ToLower();
            string[] namePrefixes = { "my name is ", "i am ", "i'm ", "call me " };
            foreach (string prefix in namePrefixes)
            {
                if (lower.Contains(prefix))
                {
                    int idx = lower.IndexOf(prefix) + prefix.Length;
                    string name = input.Substring(idx).Trim().Split(' ')[0];
                    name = System.Text.RegularExpressions.Regex.Replace(name, @"[^a-zA-Z]", "");
                    if (name.Length > 0)
                    {
                        _memory.Name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
                        return $"Nice to meet you, {_memory.Name}! I'll remember your name. " +
                               "Ask me anything about cybersecurity — passwords, phishing, scams, privacy, and more!";
                    }
                }
            }
            return null;
        }

        private string TryExtractFavouriteTopic(string input)
        {
            string lower = input.ToLower();
            if (lower.Contains("interested in") || lower.Contains("favourite topic") ||
                lower.Contains("favorite topic") || lower.Contains("i like") || lower.Contains("i love"))
            {
                foreach (string keyword in _keywordResponses.Keys)
                {
                    if (lower.Contains(keyword))
                    {
                        _memory.FavouriteTopic = keyword;
                        string name = _memory.Name.Length > 0 ? $", {_memory.Name}" : "";
                        return $"Great{name}! I'll remember that you're interested in {keyword}. " +
                               $"It's a crucial part of staying safe online. " +
                               GetRandomResponse(keyword);
                    }
                }
            }
            return null;
        }

        private bool IsFollowUp(string input)
        {
            string lower = input.ToLower();
            return _followUpTriggers.Any(t => lower.Contains(t));
        }

        private string HandleFollowUp()
        {
            if (_lastTopic == string.Empty)
                return "Sure! What topic would you like to explore? Try asking about passwords, phishing, scams, or privacy.";

            string tip = GetRandomResponse(_lastTopic);
            string name = _memory.Name.Length > 0 ? $", {_memory.Name}" : "";
            return $"Here's another tip on {_lastTopic}{name}: {tip}";
        }

        private string DetectSentiment(string input)
        {
            string lower = input.ToLower();
            bool positive = _positiveWords.Any(w => lower.Contains(w));
            bool negative = _negativeWords.Any(w => lower.Contains(w));

            if (positive && !negative) return "positive";
            if (negative && !positive) return "negative";
            return "neutral";
        }

        private string FindKeyword(string input)
        {
            string lower = input.ToLower();
            foreach (string keyword in _keywordResponses.Keys)
            {
                if (lower.Contains(keyword))
                    return keyword;
            }
            return null;
        }

        private string GetRandomResponse(string keyword)
        {
            List<string> responses = _keywordResponses[keyword];
            return responses[_random.Next(responses.Count)];
        }

        private string BuildPersonalisedResponse(string sentiment, string keyword, string tip)
        {
            string prefix = "";
            string name = _memory.Name.Length > 0 ? $" {_memory.Name}" : "";

            if (sentiment == "negative")
                prefix = $"I understand your concern{name} — let me help. ";
            else if (sentiment == "positive")
                prefix = $"Glad you're staying safe{name}! ";

            // Reference favourite topic if relevant
            string suffix = "";
            if (_memory.FavouriteTopic == keyword && _memory.Name.Length > 0)
                suffix = $" As someone interested in {keyword}, you're already on the right track!";

            return prefix + tip + suffix;
        }

        private bool IsGreeting(string input)
        {
            string lower = input.ToLower();
            string[] greetings = { "hello", "hi", "hey", "howdy", "greetings", "good morning", "good afternoon", "good evening" };
            return greetings.Any(g => lower.Contains(g));
        }

        private string BuildGreeting()
        {
            string name = _memory.Name.Length > 0 ? $", {_memory.Name}" : "";
            return $"Hello{name}! 👋 I'm CyberBot, your cybersecurity awareness assistant. " +
                   "You can ask me about topics like passwords, phishing, scams, privacy, malware, VPNs, firewalls, updates, backups, or Wi-Fi safety!";
        }

        private string BuildHelpMessage()
        {
            string name = _memory.Name.Length > 0 ? $", {_memory.Name}" : "";
            return $"Here's what I can help you with{name}:\n\n" +
                   "🔐 Password safety\n" +
                   "🎣 Phishing awareness\n" +
                   "⚠️ Scam detection\n" +
                   "🔒 Privacy tips\n" +
                   "🦠 Malware protection\n" +
                   "🌐 VPN usage\n" +
                   "🧱 Firewall basics\n" +
                   "🔄 Software updates\n" +
                   "💾 Data backups\n" +
                   "📶 Wi-Fi security\n\n" +
                   "Just type a topic or ask a question!";
        }
    }
}
