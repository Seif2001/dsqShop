using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class User:BaseEntity
    {
        public string PhoneNumber { get; set; }
        public virtual List<Voucher> CurrentVouchers { get; set; }
        //public virtual List<Voucher> VoucherTrans {  get; set; }
    }
}
