using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Bid
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public Voucher Voucher { get; set; }
        public int VoucherBidId { get; set; }
        public Voucher VoucherBid { get; set; }
        public bool IsAccepted { get; set; } = false;
    }
}
