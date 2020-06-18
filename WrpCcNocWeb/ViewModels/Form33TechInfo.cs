using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.ViewModels
{
    public class Form33TechInfo
    {
        public CcModAppProjectCommonDetail CommonDetail { get; set; }
        public CcModAppProject_33_IndvDetail Project33Indv { get; set; }
        public List<CcModPrjHydroRegionDetail> HydroRegion { get; set; }
        public List<CcModBDP2100HotSpotDetail> BDP2100HotSpot { get; set; }        
        public List<CcModGroundWaterQualityDetail> GroundWaterQltDetail { get; set; }
        public List<CcModWaterDiversSourceDetail> WaterDivSourceDetail { get; set; }
    }
}