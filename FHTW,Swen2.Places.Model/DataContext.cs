using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace SWEN2.Places.Model
{
    /// <summary>This class provides a data context.</summary>
    public class DataContext: DbContext
    {
        /// <summary>FTS index rebuild required flag.</summary>
        private bool _RebuildRequired = true;


        /// <summary>Creates a new instance of this class.</summary>
        public DataContext(): base()
        {}


        /// <summary>Called when configuring the data context.</summary>
        /// <param name="optionsBuilder">Options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source = {Root.Config.DatabasePath};");
        }


        /// <summary>Saves changes to the context.</summary>
        /// <returns>Returns the number of changed rows.</returns>
        public override int SaveChanges()
        {
            _RebuildRequired = true;
            return base.SaveChanges();
        }

        /// <summary>Called when the data model is created.</summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Place>(new PlaceConfiguration());
            modelBuilder.Entity<Story>().Property(m => m.Pictures).HasConversion
            (
                m => string.Join(',', m),
                m => m.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
        }


        /// <summary>Gets the places in this context.</summary>
        public DbSet<Place> Places { get; set; }


        /// <summary>This class provides an entity type configuration for the Place class.</summary>
        public class PlaceConfiguration: IEntityTypeConfiguration<Place>
        {
            /// <summary>Configures the entity.</summary>
            /// <param name="builder">Entity type builder.</param>
            public void Configure(EntityTypeBuilder<Place> builder)
            {
                builder.Property(m => m._Location);
            }
        }


        /// <summary>Rebuilds the FTS index.</summary>
        public void RebuildFtsIndex()
        {
            IDbContextTransaction t = Database.BeginTransaction();
            Database.ExecuteSql($"DELETE FROM PLACES_FTX");
            Database.ExecuteSql($"INSERT INTO PLACES_FTX (PLACE_ID, TEXT) SELECT ID, NAME FROM PLACES");
            Database.ExecuteSql($"INSERT INTO PLACES_FTX (PLACE_ID, TEXT) SELECT ID, DESCRIPTION FROM PLACES");
            Database.ExecuteSql($"INSERT INTO PLACES_FTX (PLACE_ID, TEXT) SELECT PLACE_ID, TEXT FROM STORIES");
            t.Commit();

            _RebuildRequired = false;
        }


        /// <summary>Performs a full text search for places.</summary>
        /// <param name="search">Search expression.</param>
        /// <returns></returns>
        public IEnumerable<Place> SearchPlaces(string search)
        {
            if(_RebuildRequired) { RebuildFtsIndex(); }
            return Places.FromSql($"SELECT * FROM PLACES P WHERE EXISTS (SELECT 1 FROM PLACES_FTX F WHERE TEXT MATCH {search} AND F.PLACE_ID = P.ID)");
        }
    }
}
