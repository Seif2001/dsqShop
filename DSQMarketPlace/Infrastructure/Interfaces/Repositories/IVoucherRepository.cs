using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories
{
    public interface IVoucherRepository: IGenericRepository<Voucher>
    {
        Task UpdateVoucher(Voucher voucher);
    }
}
