namespace StillGoodToGo.Exceptions
{
    public class ParamIsNull : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ParamIsNull() : base("Param is null.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public ParamIsNull(string message) : base(message) { }
    }
}
