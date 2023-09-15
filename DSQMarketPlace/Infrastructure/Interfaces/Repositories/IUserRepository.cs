using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<Voucher>> GetCurrentVouchers(int UserId);
        Task<bool> IsUserThereAsync(string phoneNumber);
    }
}
