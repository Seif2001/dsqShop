using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(StoreContext storeContext) : base(storeContext)
        {
        }

        public async Task<List<Voucher>> GetCurrentVouchers(int userId)
        {
            var vouchers = await _storeContext.Vouchers.Include(u=>u.Merchant).Include(u=>u.VoucherBids).Where(v=>v.UserId == userId).ToListAsync();
            
            if (vouchers.Count == 0) { throw new Exception("No Vouchers found for User"); }
            return vouchers;
        }

        public async Task<bool> IsUserThereAsync(string phoneNumber)
        {
            bool isThere = await _storeContext.Users.AnyAsync(u=>u.PhoneNumber == phoneNumber);
            return isThere;
        }
    }
}

