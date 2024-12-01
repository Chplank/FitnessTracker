using FitnessTrackerCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTrackerCore
{
    public class BloggingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public string DbPath { get; }
        public BloggingContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "FitnessTracker.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<WorkoutSession>()
                .HasOne(ws => ws.User)
                .WithMany(u => u.WorkoutSessions)
                .HasForeignKey(ws => ws.UserId);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.WorkoutSession)
                .WithMany(ws => ws.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutSessionId);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(we => we.ExerciseId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
            ////.UseLazyLoadingProxies()
            //.UseSeeding((context, _) =>
            //{
            //    var blog = new Blog() { Url = "https://github.com" };
            //    blog.Posts.Add(new Post() { Content = "Hello World!" });

            //    context.Set<Blog>().Add(blog);
            //    context.SaveChanges();

            //});

        }
    }
}
