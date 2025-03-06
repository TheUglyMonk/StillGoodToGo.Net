namespace StillGoodToGo.Exceptions
{
    public class InvalidStatus : Exception
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidStatus() : base("Status is invalid, please insert available, sold or unvailable status.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidStatus(string message) : base(message) { }
    }
}
