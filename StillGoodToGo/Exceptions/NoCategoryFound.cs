namespace StillGoodToGo.Exceptions
{
    public class NoCategoryFound : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NoCategoryFound() : base("Category Not Found.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public NoCategoryFound(string message) : base(message) { }
    }
}
