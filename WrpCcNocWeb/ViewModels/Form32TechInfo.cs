using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.ViewModels
{
    public class Form32TechInfo
    {
        public CcModAppProjectCommonDetail CommonDetail { get; set; }
        public CcModAppProject_32_IndvDetail Project32Indv { get; set; }
        public List<CcModPrjHydroRegionDetail> HydroRegion { get; set; }
        public List<CcModBDP2100HotSpotDetail> BDP2100HotSpot { get; set; }
        public List<CcModProposedWaterUseDetail> ProposedWaterUseDetail { get; set; }
        public List<CcModRiverTypeDetail> RiverTypeDetail { get; set; }
        public List<CcModGroundWaterQualityDetail> GroundWaterQltDetail { get; set; }
        public List<CcModWaterDiversSourceDetail> WaterDivSourceDetail { get; set; }
    }
}