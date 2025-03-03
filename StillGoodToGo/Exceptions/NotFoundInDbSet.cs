namespace StillGoodToGo.Exceptions
{
    public class NotFoundInDbSet : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NotFoundInDbSet() : base("Object not found in context.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public NotFoundInDbSet(string message) : base(message) { }
    }
}
