using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FHTW.Swen2.Places.Model
{
    public class DataContext: DbContext
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private members                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Indicates a FTS rebuild is required.</summary>
        private bool _RebuildRequired = false;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [override] DbContext                                                                                     //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Configures the database.</summary>
        /// <param name="optionsBuilder">Option builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {Root.Config.DatabaseFile}");
        }


        /// <summary>Configures the model.</summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>().Property(m => m._Location);
            modelBuilder.Entity<Story>().Property(m => m.Pictures)
                        .HasConversion(m => string.Join(',', m),
                                       m => m.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }


        /// <summary>Saves the changes to the data model.</summary>
        /// <returns>Number of entries written to the database.</returns>
        public override int SaveChanges()
        {
            _RebuildRequired = true;

            return base.SaveChanges();
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public method                                                                                            //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
        /// <returns>Places found.</returns>
        public IEnumerable<Place> SearchPlaces(string search)
        {
            if(_RebuildRequired) { RebuildFtsIndex(); }

            return Places.FromSql($"SELECT * FROM PLACES P WHERE EXISTS (SELECT 1 FROM PLACES_FTX F WHERE F.PLACE_ID = P.ID AND F.TEXT MATCH {search})");
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets the places from the database.</summary>
        public DbSet<Place> Places { get; set; }
    }
}
