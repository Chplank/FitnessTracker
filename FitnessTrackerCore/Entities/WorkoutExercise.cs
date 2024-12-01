using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackerCore.Entities
{
    public class WorkoutExercise : BaseEntity
    {
        public int WorkoutSessionId { get; set; }
        public WorkoutSession WorkoutSession { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public double WeightUsed { get; set; } // in kg
    }
}
