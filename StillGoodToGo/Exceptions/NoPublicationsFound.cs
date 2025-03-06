namespace StillGoodToGo.Exceptions
{
    public class NoPublicationsFound : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NoPublicationsFound() : base("No Publications were found") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public NoPublicationsFound(string message) : base(message) { }
    }
}
