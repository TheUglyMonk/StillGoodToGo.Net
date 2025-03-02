namespace StillGoodToGo.Exceptions
{
    public class ObjIsNull : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ObjIsNull() : base("Object is null.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public ObjIsNull(string message) : base(message) { }
    }
}
