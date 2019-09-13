using System;
using Flyinline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Flyinline.Persistance.Contexts
{
    public partial class FlyinlineDbContext : DbContext
    {
        public FlyinlineDbContext()
        {
        }

        public FlyinlineDbContext(DbContextOptions<FlyinlineDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlwaysExecutingTask> AlwaysExecutingTask { get; set; }
        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<Customization> Customization { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Principal> Principal { get; set; }
        public virtual DbSet<PrincipalHasRole> PrincipalHasRole { get; set; }
        public virtual DbSet<PrincipalPermission> PrincipalPermission { get; set; }
        public virtual DbSet<Revision> Revision { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
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
            AdditionalModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AlwaysExecutingTask>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__AlwaysEx__3214EC26C1E77C51")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("AlwaysExecutingTask", "DBTimeLine");

                entity.HasIndex(e => new { e.Executed, e.Id })
                    .HasName("IX_DBTimeLineAlwaysExecutingTask_Clustered")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Executed)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModuleKey)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectFullName)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RevisionKey)
                    .IsRequired()
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.RevisionType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SchemaName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SchemaObjectName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

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

            modelBuilder.Entity<Customization>(entity =>
            {
                entity.ToTable("Customization", "Config");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CustomizationKey)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module", "DBTimeLine");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DefaultSchemaName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
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

            modelBuilder.Entity<Revision>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Revision__3214EC263203A205")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Revision", "DBTimeLine");

                entity.HasIndex(e => new { e.RevisionKey, e.Id })
                    .HasName("IX_DBTimeLineRevision_Clustered")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Executed)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModuleKey)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectFullName)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RevisionKey)
                    .IsRequired()
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.RevisionType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SchemaName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SchemaObjectName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
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

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UserDetail)
                    .HasForeignKey<UserDetail>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_UserDetail_ID_Common_Principal_ID");
            });
        }
    }
}
