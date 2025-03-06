namespace StillGoodToGo.Exceptions
{
    public class InvalidStatusFound : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidStatusFound() : base("Invalid Status passed") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidStatusFound(string message) : base(message) { }
    }
}
