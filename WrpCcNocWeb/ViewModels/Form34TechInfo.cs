using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.ViewModels
{
    public class Form34TechInfo
    {
        public CcModAppProjectCommonDetail CommonDetail { get; set; }
        public CcModAppProject_34_IndvDetail Project34Indv { get; set; }
        public List<CcModPrjHydroRegionDetail> HydroRegion { get; set; }
        public List<CcModBDP2100HotSpotDetail> BDP2100HotSpot { get; set; }        
        public List<CcModNavigationClassDetail> NavigationClassDetail { get; set; }
        public List<CcModRiverTypeDetail> RiverTypeDetail { get; set; }
    }
}