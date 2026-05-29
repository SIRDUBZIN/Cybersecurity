# YouTube Presentation Script

Hi, my name is SIZOLWETHU MNCULWANE, and this is my PROG6221 Part 2 Cybersecurity Awareness Chatbot.

In Part 1, the chatbot was a command-line application. In Part 2, I upgraded it into a WPF graphical user interface to make it more user-friendly and interactive.

The application starts with a voice greeting using the SoundPlayer class. The greeting WAV file is stored in the Media folder and copied with the project when it runs.

The interface has a dark cybersecurity theme, a chatbot title section, an ASCII art display, quick topic buttons, a chat area, and a message input box. The user can type questions or click a quick topic button such as Password, Phishing, Privacy, Scam, Malware, or 2FA.

The code is organised using object-oriented programming. The UserProfile class stores the user's name, favourite cybersecurity topic, last topic, and sentiment. The ChatbotService class handles the chatbot logic. The AudioPlayer class handles the greeting sound. MainWindow.xaml controls the layout, and MainWindow.xaml.cs connects the interface to the chatbot logic.

The chatbot uses keyword recognition to identify cybersecurity topics from the user's input. For example, if the user types password, login, or passcode, the bot recognises the topic as password safety and gives a relevant response.

The chatbot also uses dictionaries and lists to store multiple responses. This allows the bot to randomly select different answers for the same topic, which makes the conversation feel less repetitive.

Another important feature is memory and recall. The bot can remember the user's name when they type something like, My name is Thabiso. It can also remember a favourite cybersecurity topic, for example, when the user says they are interested in privacy.

The chatbot supports conversation flow. If the user asks follow-up questions such as tell me more, why, explain more, or give me another tip, the bot continues the current topic instead of starting over.

The bot also includes sentiment detection. If the user says they are worried, scared, confused, or stressed, the bot responds in a calmer and more supportive way before giving the cybersecurity advice.

For error handling, if the user types something the bot does not understand, it does not crash. Instead, it gives a default response and asks the user to rephrase or ask about supported topics.

Overall, this project meets the Part 2 requirements by adding a complete GUI, voice greeting, ASCII art, keyword recognition, random responses, memory, sentiment detection, conversation flow, error handling, and organised code that can be expanded further in Part 3.

Thank you for watching.
