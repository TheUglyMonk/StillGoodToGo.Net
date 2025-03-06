namespace StillGoodToGo.Exceptions
{
    public class InvalidEndDate : Exception
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidEndDate() : base("End Date is invalid, please insert a valid date.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidEndDate(string message) : base(message) { }
    }
}
