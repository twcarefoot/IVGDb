namespace IVGDb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class ivgdb_Entities : DbContext
    {
        public ivgdb_Entities()
            : base("name=ivgdb_Entities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VideoGame> VideoGames { get; set; }
    }
}