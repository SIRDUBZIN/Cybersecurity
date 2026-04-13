# 🛡️ Cybersecurity Awareness Bot

## 📌 Overview

The **Cybersecurity Awareness Bot** is a simple C# console application designed to educate users about basic cybersecurity concepts. It provides an interactive menu where users can learn about topics such as password safety, phishing, and safe browsing.

The program also includes user interaction features like a welcome message, ASCII art display, and optional audio playback.

---

## 🎯 Features

* 🔊 Plays a welcome sound (if available)
* 👤 Prompts user for their name
* 🤖 Displays an ASCII-style bot banner
* 📚 Provides cybersecurity tips through a menu system:

  * Password Safety
  * Phishing Awareness
  * Safe Browsing
* 🎨 Uses colored console output for better user experience
* 🔁 Runs in a loop until the user chooses to exit

---

## 🏗️ Project Structure

### 1. **CybersecurityBot Class**

Handles user interaction and display elements:

* `PlayWelcomeSound()` – Plays a `.wav` file
* `GreetUser()` – Displays the application title
* `GetUserName()` – Prompts user input
* `ShowAsciiBot()` – Displays ASCII banner
* `ShowWelcomeMessage(string name)` – Personalized greeting
* `DisplayGoodbyeMessage()` – Exit message

---

### 2. **Menu Class**

Handles the menu system:

* Displays options for cybersecurity topics
* Validates user input
* Provides educational feedback based on selection

---

### 3. **Program Class**

Main entry point of the application:

* Initializes bot and menu
* Controls application flow
* Runs loop until user exits

---

## ▶️ How to Run

### ✅ Requirements

* .NET SDK installed (e.g., .NET 6 or later)
* C# compatible IDE (e.g., Visual Studio)

### 🚀 Steps

1. Open the project in Visual Studio or your preferred IDE
2. Ensure the file `welcome.wav` is in the correct directory (or update the file path in code)
3. Build and run the project
4. Follow the on-screen prompts

---

## 🔊 Audio 
* Catch the error
* Display a message instead of crashing

---

## 💡 Example Output

```
CYBERSECURITY AWARENESS
------------------------
Enter your name: John

_____________________________
| CYBER KNOWLEDGE & Awareness |
-----------------------------

Welcome John! Let's begin learning about cybersecurity.

════════════ MENU ════════════
1. Password Safety
2. Phishing
3. Safe Browsing
4. Exit
```

---

## 🔐 Cybersecurity Topics Covered

### 🔑 Password Safety

* Use strong, unique passwords
* Avoid reusing credentials

### 🎣 Phishing

* Be cautious of suspicious emails
* Never share personal information blindly

### 🌐 Safe Browsing

* Use secure (HTTPS) websites
* Avoid clicking unknown links
* Consider using VPNs

---

## 🛠️ Future Improvements

* Add more cybersecurity topics
* Implement quiz functionality
* Store user progress
* Improve UI with GUI (Windows Forms or WPF)
* Add more sound effects

---

## 👨‍💻 Author

Sizolwethu Mnculwane
