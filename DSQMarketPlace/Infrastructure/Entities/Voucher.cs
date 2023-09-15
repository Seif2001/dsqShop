namespace Infrastructure.Entities
{
    public class Voucher : BaseEntity
    {
        public int Price { get; set; }
        public virtual List<Bid> VoucherBids { get; set; }
        //public virtual List<User> UserTrans { get; set; }
        public virtual User Owner { get; set; }
        public int UserId { get; set; }
        public virtual Merchant Merchant { get; set; }
        public int MerchantId { get; set; }
        public int VoucherNumber { get; set; }
    }
}
