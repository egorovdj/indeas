using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace indeas.Models
{
    public partial class PlatformData : DbContext
    {
        public PlatformData()
        {
        }

        public PlatformData(DbContextOptions<PlatformData> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Competency> Competencies { get; set; } = null!;
        public virtual DbSet<Dictionary> Dictionaries { get; set; } = null!;
        public virtual DbSet<Idea> Ideas { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ms-sql-8.in-solve.ru;Initial Catalog=1gb_indeas;User ID=1gb_egorovdj;Password=3zzbbc78bqwr;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Author1)
                    .HasColumnName("Author")
                    .HasComment("Автор");

                entity.Property(e => e.Idea).HasComment("Идея");

                entity.HasOne(d => d.Author1Navigation)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.Author1)
                    .HasConstraintName("FK_Authors_People");

                entity.HasOne(d => d.IdeaNavigation)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.Idea)
                    .HasConstraintName("FK_Authors_Ideas");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Senior).HasComment("Старшая");

                entity.Property(e => e.Tags).HasComment("Тэги");

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .HasComment("Наименование");

                entity.HasOne(d => d.SeniorNavigation)
                    .WithMany(p => p.InverseSeniorNavigation)
                    .HasForeignKey(d => d.Senior)
                    .HasConstraintName("FK_Categories_Categories");
            });

            modelBuilder.Entity<Competency>(entity =>
            {
                entity.Property(e => e.Person).HasComment("Лицо");

                entity.Property(e => e.Profession)
                    .HasMaxLength(256)
                    .HasComment("Профессия");

                entity.Property(e => e.Specialist)
                    .HasMaxLength(256)
                    .HasComment("Специалист");

                entity.Property(e => e.Tags).HasComment("Теги");

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.Competencies)
                    .HasForeignKey(d => d.Person)
                    .HasConstraintName("FK_Competencies_People");
            });

            modelBuilder.Entity<Dictionary>(entity =>
            {
                entity.Property(e => e.Category).HasComment("Категория");

                entity.Property(e => e.Tags).HasComment("Тэги");

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .HasComment("Наименование");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Dictionaries)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_Dictionaries_Categories");
            });

            modelBuilder.Entity<Idea>(entity =>
            {
                entity.Property(e => e.Annotation).HasComment("Аннотация");

                entity.Property(e => e.Category).HasComment("Категория");

                entity.Property(e => e.Closed)
                    .HasColumnType("date")
                    .HasComment("Закрыта");

                entity.Property(e => e.Description).HasComment("Описание");

                entity.Property(e => e.Files).HasComment("Файлы");

                entity.Property(e => e.Implemented).HasComment("Реализуемая");

                entity.Property(e => e.Innovative).HasComment("Инновационная");

                entity.Property(e => e.Links).HasComment("Ссылки");

                entity.Property(e => e.Minus).HasComment("Минус - число отрицательных оценок");

                entity.Property(e => e.Moderation).HasComment("Модерация");

                entity.Property(e => e.Plus).HasComment("Плюс - число положительных оценок");

                entity.Property(e => e.Promising).HasComment("Перспективная");

                entity.Property(e => e.Published)
                    .HasColumnType("date")
                    .HasComment("Опубликована");

                entity.Property(e => e.Rating).HasComment("Рейтинг");

                entity.Property(e => e.Tags).HasComment("Тэги");

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .HasComment("Заголовок");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_Ideas_Categories");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.AboutMe).HasComment("О себе");

                entity.Property(e => e.Contacts)
                    .HasMaxLength(256)
                    .HasComment("Контакты");

                entity.Property(e => e.Gender).HasComment("Пол");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(256)
                    .HasComment("Отчество");

                entity.Property(e => e.Minus).HasComment("Минус - число отрицательных оценок");

                entity.Property(e => e.Moderation).HasComment("Модерация");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasComment("Имя");

                entity.Property(e => e.Nic)
                    .HasMaxLength(256)
                    .HasComment("Ник");

                entity.Property(e => e.Plus).HasComment("Плюс - число положительных оценок");

                entity.Property(e => e.Private).HasComment("Закрытый");

                entity.Property(e => e.Rating).HasComment("Рейтинг");

                entity.Property(e => e.Surname)
                    .HasMaxLength(256)
                    .HasComment("Фамилия");

                entity.Property(e => e.Tags).HasComment("Тэги");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasComment("Идентификатор пользователя");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_People_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
