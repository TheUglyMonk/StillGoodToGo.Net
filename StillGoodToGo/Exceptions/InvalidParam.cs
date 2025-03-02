namespace StillGoodToGo.Exceptions
{
    public class InvalidParam : Exception
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidParam() : base("Param is invalid.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidParam(string message) : base(message) { }
    }
}
