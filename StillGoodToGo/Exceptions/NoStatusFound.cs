namespace StillGoodToGo.Exceptions
{
    public class NoStatusFound : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NoStatusFound() : base("Status Not Found.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public NoStatusFound(string message) : base(message) { }
    }
}
