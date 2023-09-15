namespace Infrastructure.Entities
{
    public class Merchant : BaseEntity
    {
        public string Name { get; set; }
        public List<Voucher> Vouchers { get; set; }
        public string PictureUrl { get; set; }  
    }
}
