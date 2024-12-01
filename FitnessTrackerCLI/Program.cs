using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FitnessTrackerCore.Interfaces;
using FitnessTrackerCore;
using FitnessTrackerCore.Repository;
using FitnessTrackerCore.Entities;

//var serviceProvider = new ServiceCollection()
//    .AddDbContext<BloggingContext>(options => options.UseSqlite("Data Source=fitness_tracker.db"))
//    .AddScoped(typeof(IRepository<,>), typeof(DbRepository<,>)) // Register generic repository
//    .AddScoped<FitnessTrackerService>()
//    .BuildServiceProvider();

var serviceProvider = new ServiceCollection()
    .AddScoped<BloggingContext>()
    .AddScoped<IRepository<Exercise, int>, DbRepository<Exercise>>()
    .AddScoped<IRepository<User, int>, DbRepository<User>>()
    .AddScoped<IRepository<WorkoutExercise, int>, DbRepository<WorkoutExercise>>()
    .AddScoped<IRepository<WorkoutSession, int>, DbRepository<WorkoutSession>>()
    .AddScoped(typeof(IRepository<,>), typeof(IRepository<,>))
    .AddScoped<FitnessTrackerService>()
    .BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BloggingContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate(); // Apply any pending migrations and create the database
        context.SaveChanges();
    }

    var service = scope.ServiceProvider.GetRequiredService<FitnessTrackerService>();

    service.RegisterUser("John Doe", "john.doe@example.com", new DateTime(1990, 1, 1), 180, 75);
    service.AddExercise("Push Up", "Strength", "Push ups exercise");
    service.AddExercise("Running", "Cardio", "Running exercise");

    var exercises = new List<(int exerciseId, int sets, int repetitions, double weightUsed)>
                {
                    (1, 3, 15, 0), // Push Ups
                    (2, 1, 0, 0) // Running
                };
    service.LogWorkoutSession(1, DateTime.Now, 60, exercises);

    var workouts = service.GetWorkoutHistory(1);
    foreach (var workout in workouts)
    {
        Console.WriteLine($"Workout on {workout.DateTime}: {workout.TotalWorkoutduration} minutes");
    }

    var allExercises = service.ListExercises();
    foreach (var exercise in allExercises)
    {
        Console.WriteLine($"Exercise: {exercise.Name}, Category: {exercise.Category}");
    }
}