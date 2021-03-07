using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class FoodMacrosCarbsStatsViewModel
    {
       public String FoodTime { get; set; }
        public double CarboHydrateInPercent { get; set; }
        public double CarboHydrateInNumber { get; set; }

        public double FatInPercent { get; set; }
        public double FatInNumber { get; set; }
        public double ProtienInNumber { get; set; }
        public double ProtienInPercent { get; set; }

        public List<FoodItemsMacros> foodItemsMacros { get; set; }

    }
}
