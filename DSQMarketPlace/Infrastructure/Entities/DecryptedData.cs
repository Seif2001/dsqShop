using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class DecryptedData
    {
        public string UserPhoneNumber { get; set; }
        public int Price { get; set; }
        public int MerchantId { get; set; }
        public int VoucherNumber { get; set; }
    }
}
