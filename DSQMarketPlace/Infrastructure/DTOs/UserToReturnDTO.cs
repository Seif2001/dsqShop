using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{
    public class UserToReturnDTO:BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
