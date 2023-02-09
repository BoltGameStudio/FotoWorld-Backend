using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FotoWorldBackend.Models;

public partial class FotoWorldContext : DbContext
{
    public FotoWorldContext()
    {
    }

    public FotoWorldContext(DbContextOptions<FotoWorldContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FollowedOffer> FollowedOffers { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<OfferGallery> OfferGalleries { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<OperatorRating> OperatorRatings { get; set; }

    public virtual DbSet<OperatorService> OperatorServices { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-4A4VQ9L;Database=FotoWorld;Trusted_Connection=True;Encrypt=False;");
                                        //tutaj server trzeba podmienic na swoj lokalny a potem na serwer
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FollowedOffer>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.OfferId).HasColumnName("offerID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Offer).WithMany(p => p.FollowedOffers)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FollowedOffers_Offers");

            entity.HasOne(d => d.User).WithMany(p => p.FollowedOffers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FollowedOffers_Users");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.OperatorId).HasColumnName("operatorID");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Operator).WithMany(p => p.Offers)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Offers_Operators");
        });

        modelBuilder.Entity<OfferGallery>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.OfferId).HasColumnName("offerID");
            entity.Property(e => e.PhotoId).HasColumnName("photoID");

            entity.HasOne(d => d.Offer).WithMany(p => p.OfferGalleries)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfferGalleries_Offers");

            entity.HasOne(d => d.Photo).WithMany(p => p.OfferGalleries)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfferGalleries_Photos");
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Availability)
                .HasMaxLength(50)
                .HasColumnName("availability");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.HashedPassword).HasColumnName("hashedPassword");
            entity.Property(e => e.IsCompany).HasColumnName("isCompany");
            entity.Property(e => e.LocationCity)
                .HasMaxLength(75)
                .HasColumnName("locationCity");
            entity.Property(e => e.OperatingRadius).HasColumnName("operatingRadius");
            entity.Property(e => e.PasswordSalt).HasColumnName("passwordSalt");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Services).HasColumnName("services");
            entity.Property(e => e.Username)
                .HasMaxLength(75)
                .HasColumnName("username");

            entity.HasOne(d => d.ServicesNavigation).WithMany(p => p.Operators)
                .HasForeignKey(d => d.Services)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operators_OperatorServices");
        });

        modelBuilder.Entity<OperatorRating>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(150)
                .HasColumnName("comment");
            entity.Property(e => e.OperatorId).HasColumnName("operatorID");
            entity.Property(e => e.Stars).HasColumnName("stars");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Operator).WithMany(p => p.OperatorRatings)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperatorRatings_OperatorRatings");

            entity.HasOne(d => d.User).WithMany(p => p.OperatorRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperatorRatings_Users");
        });

        modelBuilder.Entity<OperatorService>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DroneFilm).HasColumnName("droneFilm");
            entity.Property(e => e.DronePhoto).HasColumnName("dronePhoto");
            entity.Property(e => e.Filming).HasColumnName("filming");
            entity.Property(e => e.Photo).HasColumnName("photo");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .HasColumnName("photoURL");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.HashedPassword).HasColumnName("hashedPassword");
            entity.Property(e => e.PasswordSalt).HasColumnName("passwordSalt");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
