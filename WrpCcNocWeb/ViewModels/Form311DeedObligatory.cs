﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.ViewModels
{
    public class Form311DeedObligatory
    {
        public CcModAppProjectCommonDetail CommonDetail { get; set; }
        //public CcModAppProject_311_IndvDetail Project311Indv { get; set; }
        public List<CcModPrjCompatNWPDetail> CompatNWPDetail { get; set; }
        public List<CcModPrjCompatNWMPDetail> CompatNWMPDetail { get; set; }
        public List<CcModPrjCompatSDGDetail> CompatSDGDetail { get; set; }
        public List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail { get; set; }
        public List<CcModBDP2100GoalDetail> BDP2100GoalDetail { get; set; }
        public List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail { get; set; }
    }
}