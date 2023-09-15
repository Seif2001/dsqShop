using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{
    public class VoucherToReturnDTO:BaseEntity
    {
        public int Price { get; set; }
        public int UserId { get; set; }
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
        public int VoucherNumber { get; set; }
        public int BidCount { get; set; }
    }
}
