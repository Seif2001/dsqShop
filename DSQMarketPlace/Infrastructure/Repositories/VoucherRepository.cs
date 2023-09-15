using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class VoucherRepository : GenericRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(StoreContext storeContext) : base(storeContext)
        {
        }
        public async Task UpdateVoucher(Voucher voucher)
        {
            _storeContext.Vouchers.Update(voucher);
            await _storeContext.SaveChangesAsync();
        }
    }
}
