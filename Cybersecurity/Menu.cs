using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity
{
    internal class Menu
    {
            public string ShowMainMenu()
            {
                bool validOption = false;
                string purpose = string.Empty;

                while (!validOption)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("\n════════════ MENU ════════════");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("  1. Password Safety ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("  2. Phishing");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("  3. Safe Browsing");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  4. Exit");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("══════════════════════════════");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("What is your purpose: ");
                    Console.ResetColor();

                    purpose = Console.ReadLine();
                    Console.WriteLine();

                    switch (purpose)
                    {
                        case "1":
                            Console.WriteLine("Password Safe is a secure, open-source password management tool that allows users to create an encrypted database to store, manage, and retrieve usernames and passwords.");
                            break;
                        case "2":
                            Console.WriteLine("Phishing is a type of cyberattack where attackers masquerade as trusted sources to steal sensitive information.");
                            break;
                        case "3":
                            Console.WriteLine("Safe Browsing is a security service that protects users by warning them before they visit dangerous websites, download malicious software, or encounter phishing attempts");
                            break;
                        case "4":
                            Console.WriteLine("Goodbye");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("I did not quite understand that please rephrase");
                            break;
                    }

                    // To avoid an infinite loop, you can break the loop if a valid option was selected
                    if (purpose == "1" || purpose == "2" || purpose == "3" || purpose == "4")
                    {
                        validOption = true;
                    }
                }

                // Return the option selected (for future use)
                return purpose; // Return the user's selected purpose.
            }
        }
    }

