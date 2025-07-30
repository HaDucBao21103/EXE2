using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Response
{
    public class CampaignTransactionSummaryResponse
    {
        public string CampaignId { get; set; }
        public float TotalIn { get; set; }
        public float TotalOut { get; set; }
        public float Remaining { get; set; }
        public decimal TargetAmount { get; set; }
    }
}
