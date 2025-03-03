namespace StillGoodToGo.Exceptions
{
    public class EstablishmentAlreadyDesactivated : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EstablishmentAlreadyDesactivated() : base("Establishment Already Desactivated") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public EstablishmentAlreadyDesactivated(string message) : base(message) { }
    }
}
