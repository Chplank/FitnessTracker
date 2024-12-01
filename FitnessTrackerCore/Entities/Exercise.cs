using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackerCore.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public string Category { get; set; } // Strength, Cardio, Flexibility
        public string Description { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
