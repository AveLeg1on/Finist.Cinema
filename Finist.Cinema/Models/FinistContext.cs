using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Finist.Cinema.Models;

public partial class FinistContext : DbContext
{
    public FinistContext()
    {
    }

    public FinistContext(DbContextOptions<FinistContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<ActorForMovie> ActorForMovies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieSchedule> MovieSchedules { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<ViewWorkerToRoom> ViewWorkerToRooms { get; set; }

    public virtual DbSet<Viewer> Viewers { get; set; }

    public virtual DbSet<ViewingRoom> ViewingRooms { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;database=Finist;user=ИСП-31;password=1234567890;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.ToTable("Actor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateBirthday).HasColumnType("date");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.Secondname)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasMany(d => d.Idfilms).WithMany(p => p.Idactors)
                .UsingEntity<Dictionary<string, object>>(
                    "ActorToFilm",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ActorToFilm_Movies"),
                    l => l.HasOne<Actor>().WithMany()
                        .HasForeignKey("Idactor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ActorToFilm_Actor1"),
                    j =>
                    {
                        j.HasKey("Idactor", "Idfilm").HasName("PK_ActorToFilm_1");
                        j.ToTable("ActorToFilm");
                        j.IndexerProperty<int>("Idactor").HasColumnName("IDActor");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                    });
        });

        modelBuilder.Entity<ActorForMovie>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ActorForMovies");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Secondname).IsRequired();
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasMany(d => d.Idfilms).WithMany(p => p.Idcountries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryToFilm",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CountryToFilm_Movies"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("Idcountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CountryToFilm_Country1"),
                    j =>
                    {
                        j.HasKey("Idcountry", "Idfilm").HasName("PK_CountryToFilm_1");
                        j.ToTable("CountryToFilm");
                        j.IndexerProperty<int>("Idcountry").HasColumnName("IDCountry");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                    });
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.ToTable("Food", tb => tb.HasTrigger("trg_Food_Cost"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.NameFood)
                .IsRequired()
                .HasMaxLength(150);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Film");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("BudgetCheck");
                    tb.HasTrigger("RatingDefault");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Budget).IsRequired();
            entity.Property(e => e.CompanyPublisher)
                .IsRequired()
                .HasMaxLength(80);
            entity.Property(e => e.DateRelease).HasColumnType("date");
            entity.Property(e => e.Director).IsRequired();
            entity.Property(e => e.FilmName)
                .IsRequired()
                .HasMaxLength(80);
            entity.Property(e => e.Poster).IsRequired();
        });

        modelBuilder.Entity<MovieSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieSch__3214EC0788E25511");

            entity.ToTable("MovieSchedule");

            entity.Property(e => e.Idroom).HasColumnName("IDRoom");
            entity.Property(e => e.ScreeningTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdroomNavigation).WithMany(p => p.MovieSchedules)
                .HasForeignKey(d => d.Idroom)
                .HasConstraintName("FK_MovieSchedule_ViewingRoom");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieSchedules)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MovieSche__Movie__160F4887");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idfilm).HasColumnName("IDFilm");
            entity.Property(e => e.IdmovieSchedule).HasColumnName("IDMovieSchedule");
            entity.Property(e => e.Idrooms).HasColumnName("IDRooms");
            entity.Property(e => e.Idviewer).HasColumnName("IDViewer");

            entity.HasOne(d => d.IdfilmNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Idfilm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Movies");

            entity.HasOne(d => d.IdmovieScheduleNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdmovieSchedule)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_MovieSchedule");

            entity.HasOne(d => d.IdviewerNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Idviewer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Viewer");

            entity.HasMany(d => d.Idfoods).WithMany(p => p.Idtickets)
                .UsingEntity<Dictionary<string, object>>(
                    "FoodToTicket",
                    r => r.HasOne<Food>().WithMany()
                        .HasForeignKey("Idfood")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FoodToViewer_Food"),
                    l => l.HasOne<Ticket>().WithMany()
                        .HasForeignKey("Idticket")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FoodToTicket_Ticket"),
                    j =>
                    {
                        j.HasKey("Idticket", "Idfood").HasName("PK_FoodToViewer_1");
                        j.ToTable("FoodToTicket");
                        j.IndexerProperty<int>("Idticket").HasColumnName("IDTicket");
                        j.IndexerProperty<int>("Idfood").HasColumnName("IDFood");
                    });
        });

        modelBuilder.Entity<ViewWorkerToRoom>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewWorkerToRoom");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idworker).HasColumnName("IDWorker");
            entity.Property(e => e.Patronymic).IsRequired();
            entity.Property(e => e.Secondname).IsRequired();
        });

        modelBuilder.Entity<Viewer>(entity =>
        {
            entity.ToTable("Viewer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateBirthday).HasColumnType("date");
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Secondname)
                .IsRequired()
                .HasMaxLength(30);
        });

        modelBuilder.Entity<ViewingRoom>(entity =>
        {
            entity.ToTable("ViewingRoom");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idworker).HasColumnName("IDWorker");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.ToTable("Worker", tb =>
                {
                    tb.HasTrigger("CheckWorkerName");
                    tb.HasTrigger("trg_Worker_Telephone_Check");
                });

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.Patronymic).IsRequired();
            entity.Property(e => e.Secondname)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasMany(d => d.IdnumberViewingRooms).WithMany(p => p.Idworkers)
                .UsingEntity<Dictionary<string, object>>(
                    "WorkerToViewingRoom",
                    r => r.HasOne<ViewingRoom>().WithMany()
                        .HasForeignKey("IdnumberViewingRoom")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_WorkerToViewingRoom_ViewingRoom1"),
                    l => l.HasOne<Worker>().WithMany()
                        .HasForeignKey("Idworker")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_WorkerToViewingRoom_Worker1"),
                    j =>
                    {
                        j.HasKey("Idworker", "IdnumberViewingRoom").HasName("PK_WorkerToViewingRoom_1");
                        j.ToTable("WorkerToViewingRoom");
                        j.IndexerProperty<int>("Idworker").HasColumnName("IDWorker");
                        j.IndexerProperty<int>("IdnumberViewingRoom").HasColumnName("IDNumberViewingRoom");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
