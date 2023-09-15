using Infrastructure.Entities;

namespace Infrastructure.DTOs
{
    public class UserVoucherToReturnDTO : BaseEntity
    {
        public int Price { get; set; }
        public int UserId { get; set; }
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
        public int VoucherNumber { get; set; }
        public int BidCount { get; set; }
        public List<VoucherToReturnDTO> BiddedVouchers { get; set; } = new List<VoucherToReturnDTO>();

    }
}
