using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModProjectQueryDetail
    {
		[Key]
		[Column("ProjectQueryId", Order = 0)]
		public long ProjectQueryId { get; set; }

		
		[Column("ProjectId", Order = 1)]
		[Display(Name = "Project")]
		public long? ProjectId { get; set; }
		[ForeignKey("ProjectId")]
		public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

		[Required]
		[Column("SenderUserId", Order = 2)]
		[Display(Name = "Sender")]
		public long SenderUserId { get; set; }
		[ForeignKey("SenderUserId")]
		public virtual AdminModUsersDetail AdminModUsersDetail_SenderId { get; set; }
		
		[Required]
		[Column("QuerySubject", Order = 3)]
		[MaxLength(250)]
		public string QuerySubject { get; set; }

		[Required]
		[Column("QueryBody", Order = 4)]
		[MaxLength(5000)]
		public string QueryBody { get; set; }

		[Required]
		[Column("ReceiverUserId", Order = 5)]
		[Display(Name = "Receiver")]
		public long ReceiverUserId { get; set; }		
		[ForeignKey("ReceiverUserId")]
		public virtual AdminModUsersDetail AdminModUsersDetail_ReceiverId { get; set; }
		
		[Column("QueryStateId", Order = 6)]
		[Display(Name = "Is Query Read by User?")]
		public int? QueryStateId { get; set; }
		[ForeignKey("QueryStateId")]
		public virtual LookUpCcModQueryState LookUpCcModQueryState { get; set; }
		
		[Column("QuerySentOn", Order = 7)]
		[Display(Name = "Query Sent On")]
		public DateTime QuerySentOn { get; set; }
    }
}
