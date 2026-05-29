using System.Collections.Generic;

namespace CybersecurityChatbot
{
    /// <summary>
    /// Stores user details remembered across the conversation session.
    /// </summary>
    public class UserMemory
    {
        public string Name { get; set; } = string.Empty;
        public string FavouriteTopic { get; set; } = string.Empty;
        public List<string> TopicsDiscussed { get; set; } = new List<string>();
    }
}
