namespace StillGoodToGo.Exceptions
{
    public class EstablishmentNotFound : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EstablishmentNotFound() : base("Establishment not found.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public EstablishmentNotFound(string message) : base(message) { }
    }
}
