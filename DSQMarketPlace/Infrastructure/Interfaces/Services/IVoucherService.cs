using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;

namespace Infrastructure.Interfaces.Services
{
    public interface IVoucherService
    {
        Task<Pagination<VoucherToReturnDTO>> GetVoucherPagination(ProductSpecParams specParams);
        Task UpdateVoucherUser(int voucherId, int userId);
        Task DeleteVoucher(int voucherId);
        Task AddVoucher(VoucherToAddDTO voucherToAdd);
        Task AddBid(BidToAddDTO bidToAdd);
        Task<Bid> AcceptBid(int bidId);
        Task<List<Bid>> GetBidByVoucherId(int voucherId);
        Task<Pagination<Merchant>> GetAllMerchants(MerchantSpecParams specs);
        DecryptedData DecryptData(string data);
    }
}
