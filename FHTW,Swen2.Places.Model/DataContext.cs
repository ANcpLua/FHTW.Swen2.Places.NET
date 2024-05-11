using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SWEN2.Places.Model
{
    /// <summary>This class provides a data context.</summary>
    public class DataContext: DbContext
    {
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
    }
}
