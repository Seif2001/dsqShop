using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Transaction
    {
        public int UserId { get; set; }
        public int VoucherId { get; set;}
        public string TransactionType { get; set; }
    }
}
