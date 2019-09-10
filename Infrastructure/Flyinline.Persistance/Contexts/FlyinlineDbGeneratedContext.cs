using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Flyinline.Domain.Entities.Flyinline
{
    public partial class FlyinlineDbGeneratedContext : DbContext
    {
        public FlyinlineDbGeneratedContext()
        {
        }

        public FlyinlineDbGeneratedContext(DbContextOptions<FlyinlineDbGeneratedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserDetail> UserDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=flyinline_dev;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.ToTable("UserDetail", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(512);
            });
        }
    }
}
