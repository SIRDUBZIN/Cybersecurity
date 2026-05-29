using CybersecurityAwarenessBotPart2.Models;
using CybersecurityAwarenessBotPart2.Services;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityAwarenessBotPart2
{
    public partial class MainWindow : Window
    {
        private readonly UserProfile profile = new();
        private readonly ChatbotService chatbot;
        private readonly AudioPlayer audioPlayer = new();

        public MainWindow()
        {
            InitializeComponent();
            chatbot = new ChatbotService(profile);
            LoadAsciiArt();
            AddBotMessage("Welcome. Please enter your name first, for example: My name is Thabiso. Then ask me about password safety, phishing, privacy, scams, malware, or 2FA.");
            audioPlayer.PlayGreeting();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void QuickTopic_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is string topic)
            {
                UserInputBox.Text = $"Tell me about {topic}";
                SendMessage();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ChatPanel.Children.Clear();
            AddBotMessage("Chat cleared. You can continue asking cybersecurity questions.");
        }

        private void UserInputBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserInputBox.Text == "Type your cybersecurity question here...")
            {
                UserInputBox.Text = "";
            }
        }

        private void SendMessage()
        {
            string message = UserInputBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(message) || message == "Type your cybersecurity question here...")
            {
                AddBotMessage("Please type a question first.");
                return;
            }

            AddUserMessage(message);
            string reply = chatbot.GetResponse(message);
            AddBotMessage(reply);
            UserInputBox.Clear();
        }

        private void AddUserMessage(string message)
        {
            AddMessageBubble(message, "You", HorizontalAlignment.Right, "#2F80ED", "White");
        }

        private void AddBotMessage(string message)
        {
            AddMessageBubble(message, "Bot", HorizontalAlignment.Left, "#16243A", "#E5EEF8");
        }

        private void AddMessageBubble(string message, string sender, HorizontalAlignment alignment, string background, string foreground)
        {
            StackPanel container = new()
            {
                HorizontalAlignment = alignment,
                MaxWidth = 720,
                Margin = new Thickness(0, 8, 0, 8)
            };

            TextBlock senderBlock = new()
            {
                Text = sender,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#94A3B8")),
                FontSize = 12,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(8, 0, 8, 4)
            };

            Border bubble = new()
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(background)),
                CornerRadius = new CornerRadius(14),
                Padding = new Thickness(14),
                Child = new TextBlock
                {
                    Text = message,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(foreground)),
                    FontSize = 15,
                    TextWrapping = TextWrapping.Wrap,
                    LineHeight = 22
                }
            };

            container.Children.Add(senderBlock);
            container.Children.Add(bubble);
            ChatPanel.Children.Add(container);
            ChatScrollViewer.ScrollToEnd();
        }

        private void LoadAsciiArt()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media", "ascii-bot.txt");

            if (!File.Exists(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ascii-bot.txt");
            }

            AsciiArtBlock.Text = File.Exists(path)
                ? File.ReadAllText(path)
                : "[ CYBER BOT ]";
        }
    }
}
