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
        public virtual DbSet<BusinessDay> BusinessDay { get; set; }
        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Customization> Customization { get; set; }
        public virtual DbSet<Line> Line { get; set; }
        public virtual DbSet<LineBusinessHour> LineBusinessHour { get; set; }
        public virtual DbSet<LineEmployee> LineEmployee { get; set; }
        public virtual DbSet<LineEmployeeAccepted> LineEmployeeAccepted { get; set; }
        public virtual DbSet<LineEmployeeInvited> LineEmployeeInvited { get; set; }
        public virtual DbSet<LineLocation> LineLocation { get; set; }
        public virtual DbSet<LinePhoto> LinePhoto { get; set; }
        public virtual DbSet<LineStatus> LineStatus { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Principal> Principal { get; set; }
        public virtual DbSet<PrincipalHasRole> PrincipalHasRole { get; set; }
        public virtual DbSet<PrincipalPermission> PrincipalPermission { get; set; }
        public virtual DbSet<Revision> Revision { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<TempLine> TempLine { get; set; }
        public virtual DbSet<TempLineEmployee> TempLineEmployee { get; set; }
        public virtual DbSet<TempLineEmployeeAccepted> TempLineEmployeeAccepted { get; set; }
        public virtual DbSet<TempLineEmployeeInvited> TempLineEmployeeInvited { get; set; }
        public virtual DbSet<TempOrganization> TempOrganization { get; set; }
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
                    .HasName("PK__AlwaysEx__3214EC2681263225")
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

            modelBuilder.Entity<BusinessDay>(entity =>
            {
                entity.ToTable("BusinessDay", "Flyinline");

                entity.HasIndex(e => e.FullName)
                    .HasName("PK_Flyinline_BusinessDay_FullName")
                    .IsUnique();

                entity.HasIndex(e => e.ShortName)
                    .HasName("PK_Flyinline_BusinessDay_ShortName")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(512);
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

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "Flyinline");

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

            modelBuilder.Entity<Line>(entity =>
            {
                entity.ToTable("Line", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Line)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_Line_OrganizationID_Flyinline_Organization_ID");
            });

            modelBuilder.Entity<LineBusinessHour>(entity =>
            {
                entity.ToTable("LineBusinessHour", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BusinessDayId).HasColumnName("BusinessDayID");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.HasOne(d => d.BusinessDay)
                    .WithMany(p => p.LineBusinessHour)
                    .HasForeignKey(d => d.BusinessDayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LineBusinessHour_BusinessDayID_Flyinline_BusinessDay_ID");

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.LineBusinessHour)
                    .HasForeignKey(d => d.LineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LineBusinessHour_LineID_Flyinline_Line_ID");
            });

            modelBuilder.Entity<LineEmployee>(entity =>
            {
                entity.ToTable("LineEmployee", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LineEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LineEmployee_EmployeeID_Flyinline_UserDetail_ID");

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.LineEmployee)
                    .HasForeignKey(d => d.LineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LineEmployee_LineID_Flyinline_Line_ID");
            });

            modelBuilder.Entity<LineEmployeeAccepted>(entity =>
            {
                entity.ToTable("LineEmployeeAccepted", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.InviteAcceptedOn).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.LineEmployeeAccepted)
                    .HasForeignKey<LineEmployeeAccepted>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LineEmployeeAccepted_ID_Flyinline_LineEmployee_ID");
            });

            modelBuilder.Entity<LineEmployeeInvited>(entity =>
            {
                entity.ToTable("LineEmployeeInvited", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.InviteEmail)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.InviteSentOn).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.LineEmployeeInvited)
                    .HasForeignKey<LineEmployeeInvited>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LineEmployeeInvited_ID_Flyinline_LineEmployee_ID");
            });

            modelBuilder.Entity<LineLocation>(entity =>
            {
                entity.ToTable("LineLocation", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.State).HasMaxLength(512);

                entity.Property(e => e.StreetAddress)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.ZipCode).HasMaxLength(512);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.LineLocation)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LineLocation_CountryID_Flyinline_Country_ID");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.LineLocation)
                    .HasForeignKey<LineLocation>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LineLocation_ID_Flyinline_Line_ID");
            });

            modelBuilder.Entity<LinePhoto>(entity =>
            {
                entity.ToTable("LinePhoto", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.PhotoName)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.PhotoUrl).IsRequired();

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.LinePhoto)
                    .HasForeignKey(d => d.LineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LinePhoto_LineID_Flyinline_Line_ID");
            });

            modelBuilder.Entity<LineStatus>(entity =>
            {
                entity.ToTable("LineStatus", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.LineStatus)
                    .HasForeignKey<LineStatus>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flyinline_LineStatus_ID_Flyinline_Line_ID");
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

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization", "Flyinline");

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

            modelBuilder.Entity<Revision>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Revision__3214EC26970FFA76")
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

            modelBuilder.Entity<TempLine>(entity =>
            {
                entity.ToTable("temp_Line", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.TempLine)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Line_temp_Organization_temp");
            });

            modelBuilder.Entity<TempLineEmployee>(entity =>
            {
                entity.ToTable("temp_LineEmployee", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.Phone).HasMaxLength(512);
            });

            modelBuilder.Entity<TempLineEmployeeAccepted>(entity =>
            {
                entity.ToTable("temp_LineEmployeeAccepted", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.InviteAcceptedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TempLineEmployeeInvited>(entity =>
            {
                entity.ToTable("temp_LineEmployeeInvited", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.InviteSentOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TempOrganization>(entity =>
            {
                entity.ToTable("temp_Organization", "Flyinline");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.ToTable("UserDetail", "Flyinline");

                entity.HasIndex(e => e.Username)
                    .HasName("PK_Flyinline_UserDetail_Username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.LastName)
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
