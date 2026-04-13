// See https://aka.ms/new-console-template for more information
using Cybersecurity;
using System;


namespace CybersecurityAwareness
{
    class Program
    {
        static void Main(string[] args)
        {
            CybersecurityBot bot = new CybersecurityBot();
            Menu menu = new Menu();

            bot.PlayWelcomeSound();
            bot.GreetUser();

            string name = bot.GetUserName();
            bot.ShowAsciiBot();
            bot.ShowWelcomeMessage(name);

            bool continueRunning = true;

            while (continueRunning)
            {
                string purpose = menu.ShowMainMenu();

                if (purpose == "4")
                {
                    bot.DisplayGoodbyeMessage();
                    continueRunning = false;
                }
                else
                {

                }
            }
        }
    }
}