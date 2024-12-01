
namespace FitnessTrackerCore.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateofBirth { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public ICollection<WorkoutSession> WorkoutSessions { get; init; } = new HashSet<WorkoutSession>();

    }
}
