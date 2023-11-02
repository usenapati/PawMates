using Microsoft.EntityFrameworkCore;
using PawMates.CORE.Models;

namespace PawMates.DAL;

public partial class PawMatesContext : DbContext
{
    public PawMatesContext()
    {
    }

    public PawMatesContext(DbContextOptions<PawMatesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<PetParent> PetParents { get; set; }

    public virtual DbSet<PetType> PetTypes { get; set; }

    public virtual DbSet<PlayDate> PlayDates { get; set; }

    public virtual DbSet<RestrictionType> RestrictionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventTyp__3214EC07043F78F2");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.RestrictionType).WithMany(p => p.EventTypes)
                .HasForeignKey(d => d.RestrictionTypeId)
                .HasConstraintName("FK_EventTypes_RestrictionTypes");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC07805BB3E0");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(15);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.Street1).HasMaxLength(50);

            entity.HasOne(d => d.PetType).WithMany(p => p.Locations)
                .HasForeignKey(d => d.PetTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Locations_PetTypes");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pets__3214EC07D7B27635");

            entity.Property(e => e.Breed).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e=> e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.PetParent).WithMany(p => p.Pets)
                .HasForeignKey(d => d.PetParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Pets_PetParents");

            entity.HasOne(d => d.PetType).WithMany(p => p.Pets)
                .HasForeignKey(d => d.PetTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pets_PetTypes");
        });

        modelBuilder.Entity<PetParent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PetParen__3214EC07B81FA18B");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(15);
        });

        modelBuilder.Entity<PetType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PetTypes__3214EC07AD48CBA1");

            entity.Property(e => e.Species).HasMaxLength(50);
        });

        modelBuilder.Entity<PlayDate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PlayDate__3214EC07BE997E49");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.EventType).WithMany(p => p.PlayDates)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayDates_EventTypes");

            entity.HasOne(d => d.Location).WithMany(p => p.PlayDates)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayDates_Locations");

            entity.HasOne(d => d.PetParent).WithMany(p => p.PlayDates)
                .HasForeignKey(d => d.PetParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayDates_PetParents");

            entity.HasMany(d => d.Pets).WithMany(p => p.PlayDates)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayDatesPet",
                    r => r.HasOne<Pet>().WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PlayDates_Pets_PetId"),
                    l => l.HasOne<PlayDate>().WithMany()
                        .HasForeignKey("PlayDateId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PlayDates_Pets_PlayDateId"),
                    j =>
                    {
                        j.HasKey("PlayDateId", "PetId").HasName("PK_PlayDates_Pets");
                        j.ToTable("PlayDatesPets");
                    });
        });

        modelBuilder.Entity<RestrictionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restrict__3214EC076F3B4259");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07BAE20558");

            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.PetParentId).IsRequired(false);

            entity.HasOne(d => d.PetParent).WithMany(p => p.Users)
                .HasForeignKey(d => d.PetParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_PetParents");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
