using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.ViewModels
{
    public class Form38TechInfo
    {
        public CcModAppProjectCommonDetail CommonDetail { get; set; }
        public CcModAppProject_38_IndvDetail Project38Indv { get; set; }
        public List<CcModPrjHydroRegionDetail> HydroRegion { get; set; }
        public List<CcModBDP2100HotSpotDetail> BDP2100HotSpot { get; set; }
        public List<CcModRiverTypeDetail> RiverTypeDetail { get; set; }        
    }
}