
namespace FitnessTrackerCore.Entities
{
    public class WorkoutSession : BaseEntity
    {
        public int? UserId { get; set; }
        public User User { get; set; }
        public DateTime DateTime { get; set; }
        public int TotalWorkoutduration { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; init; } = new HashSet<WorkoutExercise>();
    }
}
