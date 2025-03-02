namespace StillGoodToGo.Exceptions
{
    public class EstablishmentNotUnique : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EstablishmentNotUnique() : base("Establishment Email or Location is not unique") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public EstablishmentNotUnique(string message) : base(message) { }
    }
}
