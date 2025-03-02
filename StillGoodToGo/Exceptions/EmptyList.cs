namespace StillGoodToGo.Exceptions
{
    public class EmptyList : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmptyList() : base("List is empty.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public EmptyList(string message) : base(message) { }
    }
}
