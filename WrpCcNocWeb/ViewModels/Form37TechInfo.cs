using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.ViewModels
{
    public class Form37TechInfo
    {
        public CcModAppProjectCommonDetail CommonDetail { get; set; }
        public CcModAppProject_37_IndvDetail Project37Indv { get; set; }
        public List<CcModPrjHydroRegionDetail> HydroRegion { get; set; }
        public List<CcModBDP2100HotSpotDetail> BDP2100HotSpot { get; set; }
        public List<CcModRiverTypeDetail> RiverTypeDetail { get; set; }
        public List<CcModGroundWaterQualityDetail> GroundWaterQualityDetail { get; set; }
        public List<CcModTypeOfWaterUseDetail> TypeOfWaterUseDetail { get; set; }
        public List<CcModWaterDiversSourceDetail> WaterDiversSourceDetail { get; set; }
    }
}