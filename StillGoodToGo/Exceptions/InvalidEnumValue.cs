namespace StillGoodToGo.Exceptions
{
    public class InvalidEnumValue : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidEnumValue() : base("Invalid Enum value") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidEnumValue(string message) : base(message) { }
    }
}
