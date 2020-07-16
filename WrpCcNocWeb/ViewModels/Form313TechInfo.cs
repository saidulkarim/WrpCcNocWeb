using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.ViewModels
{
    public class Form313TechInfo
    {
        public CcModAppProjectCommonDetail CommonDetail { get; set; }
        public CcModAppProject_313_IndvDetail Project313Indv { get; set; }
        public List<CcModPrjHydroRegionDetail> HydroRegion { get; set; }
        public List<CcModBDP2100HotSpotDetail> BDP2100HotSpot { get; set; }
        public List<CcModRiverTypeDetail> RiverType { get; set; }
        public List<CcModPrjTypesOfFloodDetail> TypesOfFlood { get; set; }
    }
}
