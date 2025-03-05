namespace StillGoodToGo.Exceptions
{
    public class InvalidPrice : Exception
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidPrice() : base("Price is invalid, please insert a number higher than 0.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidPrice(string message) : base(message) { }
    }
}
