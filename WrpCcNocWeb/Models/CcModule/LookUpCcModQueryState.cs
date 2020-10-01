using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModQueryState
    {
		[Key]
		[Column("QueryStateId", Order = 0)]
		public int QueryStateId { get; set; }

		[Required]
		[Column("QueryStateName", Order = 1)]
		[Display(Name = "Query State")]
		public string QueryStateName { get; set; }
    }
}