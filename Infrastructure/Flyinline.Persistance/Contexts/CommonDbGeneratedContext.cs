using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Flyinline.Domain.Entities.Common
{
    public partial class CommonDbGeneratedContext : DbContext
    {
        public CommonDbGeneratedContext()
        {
        }

        public CommonDbGeneratedContext(DbContextOptions<CommonDbGeneratedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<Principal> Principal { get; set; }
        public virtual DbSet<PrincipalHasRole> PrincipalHasRole { get; set; }
        public virtual DbSet<PrincipalPermission> PrincipalPermission { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }

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

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.ToTable("Claim", "Common");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<Principal>(entity =>
            {
                entity.ToTable("Principal", "Common");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<PrincipalHasRole>(entity =>
            {
                entity.ToTable("PrincipalHasRole", "Common");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PrincipalId).HasColumnName("PrincipalID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Principal)
                    .WithMany(p => p.PrincipalHasRole)
                    .HasForeignKey(d => d.PrincipalId)
                    .HasConstraintName("FK_Common_PrincipalHasRole_PrincipalID_Common_Principal_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PrincipalHasRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Common_PrincipalHasRole_RoleID_Common_Role_ID");
            });

            modelBuilder.Entity<PrincipalPermission>(entity =>
            {
                entity.ToTable("PrincipalPermission", "Common");

                entity.HasIndex(e => new { e.CanExecute, e.PrincipalId, e.ClaimId })
                    .HasName("IX_Common_PrincipalPermission_PrincipalID_ClaimID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.PrincipalId).HasColumnName("PrincipalID");

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.PrincipalPermission)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Common_PrincipalPermission_ClaimID_Common_Claim_ID");

                entity.HasOne(d => d.Principal)
                    .WithMany(p => p.PrincipalPermission)
                    .HasForeignKey(d => d.PrincipalId)
                    .HasConstraintName("FK_Common_PrincipalPermission_PrincipalID_Common_Principal_ID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "Common");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolePermission", "Common");

                entity.HasIndex(e => new { e.CanExecute, e.RoleId, e.ClaimId })
                    .HasName("IX_Common_RolePermission_RoleID_ClaimID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.ClaimId)
                    .HasConstraintName("FK_Common_RolePermission_ClaimID_Common_Claim_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Common_RolePermission_RoleID_Common_Role_ID");
            });
        }
    }
}
