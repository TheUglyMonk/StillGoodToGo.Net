using Microsoft.EntityFrameworkCore;
using StillGoodToGo.Models;

namespace StillGoodToGo.DataContext
{
    /// <summary>
    /// Represents the database context for the StillGoodToGo application.
    /// </summary>
    public class StillGoodToGoContext : DbContext
    {
        /// <summary>
        /// Constructor to create a new database context.
        /// </summary>
        /// <param name="options"></param>
        public StillGoodToGoContext(DbContextOptions<StillGoodToGoContext> options) : base(options){}

        /// <summary>
        /// Represents the establishments table in the database.
        /// </summary>
        public DbSet<Establishment> Establishments { get; set; }

        /// <summary>
        /// Represents the publications table in the database.
        /// </summary>
        public DbSet<Publication> Publications { get; set; }
    }
}
