using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace CybersecurityChatbot
{
    public partial class MainForm : Form
    {
        private readonly ChatbotEngine _chatbot = new ChatbotEngine();

        // ── Controls ──────────────────────────────────────────────────────────
        private Panel _headerPanel;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private PictureBox _logoBox;

        private RichTextBox _chatDisplay;
        private Panel _inputPanel;
        private TextBox _inputBox;
        private Button _sendButton;
        private Button _clearButton;

        private Panel _sidePanel;
        private Label _memoryTitle;
        private Label _memoryNameLabel;
        private Label _memoryTopicLabel;
        private ListBox _topicsList;
        private Label _topicsTitle;

        private Panel _statusBar;
        private Label _statusLabel;

        // ── Colour scheme ─────────────────────────────────────────────────────
        private readonly Color _darkBg       = Color.FromArgb(15,  20,  40);
        private readonly Color _panelBg      = Color.FromArgb(22,  30,  55);
        private readonly Color _accent       = Color.FromArgb(0,  180, 216);
        private readonly Color _accentHover  = Color.FromArgb(0,  210, 255);
        private readonly Color _userBubble   = Color.FromArgb(0,  100, 160);
        private readonly Color _botBubble    = Color.FromArgb(30,  42,  80);
        private readonly Color _textPrimary  = Color.FromArgb(220, 230, 255);
        private readonly Color _textSecond   = Color.FromArgb(140, 160, 200);
        private readonly Color _sendBtn      = Color.FromArgb(0,  150, 180);
        private readonly Color _clearBtn     = Color.FromArgb(180,  50,  50);

        public MainForm()
        {
            InitializeComponent();
            WireEvents();
            ShowWelcomeMessage();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  DESIGNER SETUP
        // ══════════════════════════════════════════════════════════════════════
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────────────────
            this.Text          = "CyberBot — Cybersecurity Awareness Chatbot";
            this.Size          = new Size(950, 700);
            this.MinimumSize   = new Size(800, 600);
            this.BackColor     = _darkBg;
            this.ForeColor     = _textPrimary;
            this.Font          = new Font("Segoe UI", 10f);
            this.StartPosition = FormStartPosition.CenterScreen;

            // ── Header ───────────────────────────────────────────────────────
            _headerPanel = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 80,
                BackColor = _panelBg,
                Padding   = new Padding(16, 0, 16, 0)
            };

            _logoBox = new PictureBox
            {
                Size     = new Size(50, 50),
                Location = new Point(16, 15),
                BackColor = Color.Transparent
            };
            DrawShieldIcon(_logoBox);

            _titleLabel = new Label
            {
                Text      = "🛡  CyberBot",
                Font      = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = _accent,
                AutoSize  = true,
                Location  = new Point(76, 14),
                BackColor = Color.Transparent
            };

            _subtitleLabel = new Label
            {
                Text      = "Your Cybersecurity Awareness Assistant",
                Font      = new Font("Segoe UI", 9f),
                ForeColor = _textSecond,
                AutoSize  = true,
                Location  = new Point(78, 46),
                BackColor = Color.Transparent
            };

            _clearButton = new Button
            {
                Text      = "Clear Chat",
                Size      = new Size(100, 36),
                Anchor    = AnchorStyles.Right | AnchorStyles.Top,
                BackColor = _clearBtn,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 9f),
                Cursor    = Cursors.Hand
            };
            _clearButton.FlatAppearance.BorderSize = 0;
            _clearButton.Location = new Point(_headerPanel.Width - 120, 22);
            _clearButton.Anchor   = AnchorStyles.Right | AnchorStyles.Top;

            _headerPanel.Controls.AddRange(new Control[] { _logoBox, _titleLabel, _subtitleLabel, _clearButton });

            // ── Side Panel ───────────────────────────────────────────────────
            _sidePanel = new Panel
            {
                Dock      = DockStyle.Right,
                Width     = 210,
                BackColor = _panelBg,
                Padding   = new Padding(12)
            };

            _memoryTitle = new Label
            {
                Text      = "🧠  Memory",
                Font      = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = _accent,
                AutoSize  = true,
                Location  = new Point(12, 12)
            };

            _memoryNameLabel = new Label
            {
                Text      = "Name: —",
                Font      = new Font("Segoe UI", 9f),
                ForeColor = _textSecond,
                AutoSize  = false,
                Size      = new Size(186, 20),
                Location  = new Point(12, 42)
            };

            _memoryTopicLabel = new Label
            {
                Text      = "Favourite topic: —",
                Font      = new Font("Segoe UI", 9f),
                ForeColor = _textSecond,
                AutoSize  = false,
                Size      = new Size(186, 20),
                Location  = new Point(12, 66)
            };

            _topicsTitle = new Label
            {
                Text      = "📚  Topics Covered",
                Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = _accent,
                AutoSize  = true,
                Location  = new Point(12, 100)
            };

            _topicsList = new ListBox
            {
                Location  = new Point(12, 126),
                Size      = new Size(186, 300),
                BackColor = _darkBg,
                ForeColor = _textPrimary,
                BorderStyle = BorderStyle.None,
                Font      = new Font("Segoe UI", 9f)
            };

            _sidePanel.Controls.AddRange(new Control[]
            {
                _memoryTitle, _memoryNameLabel, _memoryTopicLabel,
                _topicsTitle, _topicsList
            });

            // ── Chat Display ─────────────────────────────────────────────────
            _chatDisplay = new RichTextBox
            {
                Dock       = DockStyle.Fill,
                BackColor  = _darkBg,
                ForeColor  = _textPrimary,
                ReadOnly   = true,
                BorderStyle = BorderStyle.None,
                Font       = new Font("Segoe UI", 10f),
                ScrollBars = RichTextBoxScrollBars.Vertical,
                Padding    = new Padding(12)
            };

            // ── Input Panel ──────────────────────────────────────────────────
            _inputPanel = new Panel
            {
                Dock      = DockStyle.Bottom,
                Height    = 60,
                BackColor = _panelBg,
                Padding   = new Padding(12, 10, 12, 10)
            };

            _inputBox = new TextBox
            {
                Dock        = DockStyle.Fill,
                BackColor   = Color.FromArgb(30, 40, 70),
                ForeColor   = _textPrimary,
                BorderStyle = BorderStyle.None,
                Font        = new Font("Segoe UI", 11f),
                PlaceholderText = "Ask me about cybersecurity..."
            };

            _sendButton = new Button
            {
                Text      = "Send  ➤",
                Dock      = DockStyle.Right,
                Width     = 110,
                BackColor = _sendBtn,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor    = Cursors.Hand
            };
            _sendButton.FlatAppearance.BorderSize = 0;

            _inputPanel.Controls.Add(_inputBox);
            _inputPanel.Controls.Add(_sendButton);

            // ── Status Bar ───────────────────────────────────────────────────
            _statusBar = new Panel
            {
                Dock      = DockStyle.Bottom,
                Height    = 24,
                BackColor = Color.FromArgb(10, 15, 30)
            };
            _statusLabel = new Label
            {
                Text      = "Ready",
                ForeColor = _textSecond,
                Font      = new Font("Segoe UI", 8f),
                AutoSize  = true,
                Location  = new Point(8, 4)
            };
            _statusBar.Controls.Add(_statusLabel);

            // ── Assemble ─────────────────────────────────────────────────────
            this.Controls.Add(_chatDisplay);
            this.Controls.Add(_sidePanel);
            this.Controls.Add(_inputPanel);
            this.Controls.Add(_statusBar);
            this.Controls.Add(_headerPanel);

            this.ResumeLayout(false);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  EVENTS
        // ══════════════════════════════════════════════════════════════════════
        private void WireEvents()
        {
            _sendButton.Click         += SendButton_Click;
            _inputBox.KeyDown         += InputBox_KeyDown;
            _clearButton.Click        += (s, e) => ClearChat();

            _sendButton.MouseEnter    += (s, e) => _sendButton.BackColor = _accentHover;
            _sendButton.MouseLeave    += (s, e) => _sendButton.BackColor = _sendBtn;

            this.Resize               += (s, e) => PositionClearButton();
        }

        private void PositionClearButton()
        {
            _clearButton.Location = new Point(_headerPanel.Width - 120, 22);
        }

        private void SendButton_Click(object sender, EventArgs e) => ProcessInput();

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                ProcessInput();
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  CORE LOGIC
        // ══════════════════════════════════════════════════════════════════════
        private void ProcessInput()
        {
            string userText = _inputBox.Text.Trim();
            if (string.IsNullOrEmpty(userText)) return;

            AppendUserMessage(userText);
            _inputBox.Clear();

            _statusLabel.Text = "CyberBot is thinking...";
            Application.DoEvents();

            string response = _chatbot.GetResponse(userText);
            AppendBotMessage(response);
            UpdateMemoryPanel();

            _statusLabel.Text = "Ready";
        }

        private void ShowWelcomeMessage()
        {
            string ascii =
                "  ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗\n" +
                " ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝\n" +
                " ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   \n" +
                " ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   \n" +
                " ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝   ██║   \n" +
                "  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝   ╚═╝   ";

            _chatDisplay.SelectionFont  = new Font("Consolas", 7f);
            _chatDisplay.SelectionColor = _accent;
            _chatDisplay.AppendText(ascii + "\n\n");

            AppendBotMessage(
                "Welcome! I'm CyberBot 🛡 — your personal cybersecurity awareness assistant.\n\n" +
                "Tell me your name to get started, or ask me about:\n" +
                "  • Passwords  • Phishing  • Scams  • Privacy\n" +
                "  • Malware    • VPNs      • Firewalls  • Wi-Fi\n\n" +
                "Type 'help' at any time to see all topics.");
        }

        // ── Message rendering ─────────────────────────────────────────────────
        private void AppendUserMessage(string text)
        {
            _chatDisplay.SelectionFont  = new Font("Segoe UI", 9f, FontStyle.Bold);
            _chatDisplay.SelectionColor = _accent;
            _chatDisplay.AppendText("You  ▶  ");

            _chatDisplay.SelectionFont  = new Font("Segoe UI", 10f);
            _chatDisplay.SelectionColor = Color.FromArgb(200, 220, 255);
            _chatDisplay.AppendText(text + "\n\n");
            _chatDisplay.ScrollToCaret();
        }

        private void AppendBotMessage(string text)
        {
            _chatDisplay.SelectionFont  = new Font("Segoe UI", 9f, FontStyle.Bold);
            _chatDisplay.SelectionColor = Color.FromArgb(0, 230, 150);
            _chatDisplay.AppendText("🛡 CyberBot  ▶  ");

            _chatDisplay.SelectionFont  = new Font("Segoe UI", 10f);
            _chatDisplay.SelectionColor = _textPrimary;
            _chatDisplay.AppendText(text + "\n\n");
            _chatDisplay.ScrollToCaret();
        }

        private void ClearChat()
        {
            _chatDisplay.Clear();
            ShowWelcomeMessage();
        }

        // ── Side panel memory update ──────────────────────────────────────────
        private void UpdateMemoryPanel()
        {
            var mem = _chatbot.Memory;
            _memoryNameLabel.Text  = $"Name: {(mem.Name.Length > 0 ? mem.Name : "—")}";
            _memoryTopicLabel.Text = $"Fav. topic: {(mem.FavouriteTopic.Length > 0 ? mem.FavouriteTopic : "—")}";

            _topicsList.Items.Clear();
            foreach (string topic in mem.TopicsDiscussed)
                _topicsList.Items.Add("✔  " + topic);
        }

        // ── Draw shield icon ──────────────────────────────────────────────────
        private void DrawShieldIcon(PictureBox pb)
        {
            Bitmap bmp = new Bitmap(50, 50);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(_accent))
                {
                    Point[] shield = {
                        new Point(25, 4), new Point(46, 12), new Point(46, 28),
                        new Point(25, 46), new Point(4, 28), new Point(4, 12)
                    };
                    g.FillPolygon(brush, shield);
                }
                using (var pen = new Pen(Color.White, 2))
                {
                    g.DrawLine(pen, 18, 24, 23, 30);
                    g.DrawLine(pen, 23, 30, 33, 18);
                }
            }
            pb.Image    = bmp;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
