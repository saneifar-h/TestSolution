namespace TestSolution.Shared
{
    public static class Messages
    {
        /// <summary>
        ///     Event name when a message is received
        /// </summary>
        public const string Receive = "ReceiveMessage";

        /// <summary>
        ///     Name of the Hub method to register a new user
        /// </summary>
        public const string Register = "Register";

        /// <summary>
        ///     Name of the Hub method to send a message
        /// </summary>
        public const string Send = "SendMessage";
    }
}