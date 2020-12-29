using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class ExerciseRowModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid exerciseRowId { get; set; }
        [Required(ErrorMessage = "Exercise Name is required")]        
        public String exerciseName { get; set; }
        [Required(ErrorMessage = "Raps is required")]
        public String raps { get; set; }
        [Required(ErrorMessage = "Time is required")]
        public String time { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public String description { get; set; }

    }
}
