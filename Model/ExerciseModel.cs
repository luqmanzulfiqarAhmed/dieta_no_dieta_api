using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class ExerciseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid exerciseId { get; set; }
        [Required(ErrorMessage = "Exercise type is required")]        
        public String exerciseType { get; set; }

        [Required(ErrorMessage = "Exercise type details are required")]
        public List<ExerciseRowModel> exerciseRows { get; set; }


    }
}
