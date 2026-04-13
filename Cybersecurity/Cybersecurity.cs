using System;
using System.Media;
using System.Threading;

namespace CybersecurityAwareness
{
    public class CybersecurityBot
    {
        private readonly string asciiBot = @"
         _____________________________
        | CYBER KNOWLEGDE & Awareness |
         -----------------------------
";

        public void PlayWelcomeSound()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(Cybersecurity.Properties.Resources.welcome);
                player.Play();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"(Audio failed to play: {ex.Message})");
            }
        }

        public void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("CYBERSECURITY AWARENESS");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------");
            Console.ResetColor();
        }

        public string GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("What is your name? : ");
            Console.ResetColor();
            return Console.ReadLine();
        }

        public void ShowAsciiBot()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(asciiBot);
            Console.ResetColor();
        }

        public void ShowWelcomeMessage(string name)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Hello {name} WELCOME TO THE CYBERSECURITY AWARENESS BOT!", 20);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("How are you?: ");
            Console.ResetColor();
            Console.ReadLine(); // Just for interaction
        }

        public void DisplayInvalidOptionMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I didn't quite understand that. Please rephrase.", 20);
            Console.ResetColor();
        }

        public void DisplayGoodbyeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Goodbye", 20);
            Console.ResetColor();
        }

        public void TypeText(string text, int delay)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine("\n");
        }
    }
}