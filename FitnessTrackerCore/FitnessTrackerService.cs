using FitnessTrackerCore.Entities;
using FitnessTrackerCore.Interfaces;

namespace FitnessTrackerCore
{
    public class FitnessTrackerService
    {
        private readonly IRepository<User, int> _userRepository;
        private readonly IRepository<Exercise, int> _exerciseRepository;
        private readonly IRepository<WorkoutSession, int> _workoutSessionRepository;
        private readonly IRepository<WorkoutExercise, int> _workoutExerciseRepository;

        public FitnessTrackerService(
            IRepository<User, int> userRepository,
            IRepository<Exercise, int> exerciseRepository,
            IRepository<WorkoutSession, int> workoutSessionRepository,
            IRepository<WorkoutExercise, int> workoutExerciseRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
            _workoutSessionRepository = workoutSessionRepository;
            _workoutExerciseRepository = workoutExerciseRepository;
        }

        public void RegisterUser(string name, string email, DateTime dateOfBirth, decimal height, decimal weight)
        {
            if (_userRepository.Read().Any(u => u.Email == email))
            {
                throw new ArgumentException("Email already in use");
            }

            var user = new User
            {
                Name = name,
                Email = email,
                DateofBirth = dateOfBirth,
                Height = height,
                Weight = weight
            };

            _userRepository.Create(user);
        }

        public void AddExercise(string name, string category, string description)
        {
            var exercise = new Exercise
            {
                Name = name,
                Category = category,
                Description = description
            };

            _exerciseRepository.Create(exercise);
        }

        public void LogWorkoutSession(int userId, DateTime date, int totalDuration, List<(int exerciseId, int sets, int repetitions, double weightUsed)> exercises)
        {
            var workoutSession = new WorkoutSession
            {
                UserId = userId,
                DateTime = date,
                TotalWorkoutduration = totalDuration,
                WorkoutExercises = exercises.Select(e => new WorkoutExercise
                {
                    ExerciseId = e.exerciseId,
                    Sets = e.sets,
                    Repetitions = e.repetitions,
                    WeightUsed = e.weightUsed
                }).ToList()
            };

            _workoutSessionRepository.Create(workoutSession);
        }

        public List<WorkoutSession> GetWorkoutHistory(int userId)
        {
            return _workoutSessionRepository.Read()
                .Where(ws => ws.UserId == userId)
                .OrderByDescending(ws => ws.DateTime)
                .ToList();
        }

        public List<Exercise> ListExercises()
        {
            return _exerciseRepository.Read().ToList();
        }
    }
}
