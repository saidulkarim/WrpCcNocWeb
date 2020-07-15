using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.ViewModels
{
    public class Form312TechInfo
    {
        public CcModAppProjectCommonDetail CommonDetail { get; set; }
        public CcModAppProject_312_IndvDetail Project312Indv { get; set; }
        public List<CcModPrjHydroRegionDetail> HydroRegion { get; set; }
        public List<CcModBDP2100HotSpotDetail> BDP2100HotSpot { get; set; }

        public List<CcModPurposeOfWaterUseDetail> PurposeOfWaterUseDetail { get; set; }
        public List<CcModCategoryOfAquiferDetail> CategoryOfAquiferDetail { get; set; }
        public List<CcModGroundWaterQualityDetail> GroundWaterQualityDetail { get; set; }
        public List<CcModSoilTypeDetail> SoilTypeDetail { get; set; }
    }
}