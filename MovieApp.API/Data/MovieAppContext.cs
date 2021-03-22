using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entities;

#nullable disable

namespace MovieApp.API.Data
{
    public partial class MovieAppContext : DbContext
    {
        public MovieAppContext()
        {
        }

        public MovieAppContext(DbContextOptions<MovieAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MovieItem> MovieItems { get; set; }
        public virtual DbSet<MovieList> MovieLists { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MovieItem>(entity =>
            {
                entity.ToTable("MovieItem");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MovieList)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.MovieListId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MovieItem_MovieList");
            });

            modelBuilder.Entity<MovieList>(entity =>
            {
                entity.ToTable("MovieList");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MovieLists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieList_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.Updated).HasColumnType("date");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
