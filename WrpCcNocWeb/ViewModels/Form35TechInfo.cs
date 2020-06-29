using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.ViewModels
{
    public class Form35TechInfo
    {
        public CcModAppProjectCommonDetail CommonDetail { get; set; }
        public CcModAppProject_35_IndvDetail Project35Indv { get; set; }
        public List<CcModPrjHydroRegionDetail> HydroRegion { get; set; }
        public List<CcModBDP2100HotSpotDetail> BDP2100HotSpot { get; set; }        
        public List<CcModStructTypeConservDetail> StructTypeConservDetail { get; set; }
        public List<CcModConservLocationDetail> ConservLocationDetail { get; set; }
    }
}