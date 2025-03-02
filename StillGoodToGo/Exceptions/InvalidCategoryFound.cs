namespace StillGoodToGo.Exceptions
{
    public class InvalidCategoryFound : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidCategoryFound() : base("Invalid Category passed") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidCategoryFound(string message) : base(message) { }
    }
}
