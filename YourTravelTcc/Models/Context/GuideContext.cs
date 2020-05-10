using Microsoft.EntityFrameworkCore;


namespace YourTravelTcc.Models.Context
{
    public class GuideContext : DbContext
    {
        //Table name
        public DbSet<Guide> Guide { get; set; }

        public GuideContext( DbContextOptions<GuideContext> options ) : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            builder.Entity<Guide>().HasKey( m => m.ID );
            base.OnModelCreating( builder );
        }


    }
}
