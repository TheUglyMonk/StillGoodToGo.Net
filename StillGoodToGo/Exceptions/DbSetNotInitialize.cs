namespace StillGoodToGo.Exceptions
{
    public class DbSetNotInitialize : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DbSetNotInitialize() : base("DbSet is not initialized.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public DbSetNotInitialize(string message) : base(message) { }
    }
}
