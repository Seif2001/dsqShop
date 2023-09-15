using AutoMapper;
using Infrastructure.DTOs;
using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Core.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Bid> _bidRepository;
        private readonly IGenericRepository<Merchant> _merchantRepository;

        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri(""),
        };

        public VoucherService(IVoucherRepository voucherRepository, IGenericRepository<Bid> BidRepository, IGenericRepository<Merchant> merchantRepo)
        {
            _voucherRepository = voucherRepository;
            _bidRepository = BidRepository;
            _merchantRepository = merchantRepo;
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VoucherToAddDTO, Voucher>();
                cfg.CreateMap<Voucher, VoucherToReturnDTO>()
                .ForMember(d => d.MerchantName, o => o.MapFrom(s => s.Merchant.Name))
                .ForMember(d => d.BidCount, o => o.MapFrom(s => s.VoucherBids.Count));
                cfg.CreateMap<Voucher, UserVoucherToReturnDTO>()
                .ForMember(d => d.MerchantName, o => o.MapFrom(s => s.Merchant.Name))
                .ForMember(d => d.BidCount, o => o.MapFrom(s => s.VoucherBids.Count));
                cfg.CreateMap<BidToAddDTO, Bid>();


            }));
        }

        public async Task<Pagination<VoucherToReturnDTO>> GetVoucherPagination(ProductSpecParams specs)
        {

            var query = _voucherRepository.ListAllAsync();

            query = query
            .Include(d => d.Merchant)
            .Include(d => d.VoucherBids);

            if (specs.MerchantId != null)
            {
                query = query
                .Where(v => v.MerchantId == specs.MerchantId);
            }
            if (!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort)
                {
                    case "priceAsc":
                        query = query.OrderBy(v => v.Price);
                        break;
                    case "priceDsc":
                        query = query.OrderByDescending(v => v.Price);
                        break;
                    default:
                        break;
                }
            }
            query = query
                .Take(specs.PageSize * (specs.PageIndex))
                .Skip(specs.PageSize*(specs.PageIndex-1));

            var data = await query.ToListAsync();
            List<VoucherToReturnDTO> returnVouchers = new List<VoucherToReturnDTO>();
            foreach (var v in data)
            {
                returnVouchers.Add(_mapper.Map<VoucherToReturnDTO>(v));
            }
            Pagination<VoucherToReturnDTO> pagination = new Pagination<VoucherToReturnDTO>()
            {
                Count = await _voucherRepository.ListAllAsync().CountAsync(),
                PageIndex = specs.PageIndex,
                PageSize = specs.PageSize,
                Data = returnVouchers
            };
            return pagination;

        }
        public async Task AddVoucher(VoucherToAddDTO voucherToAdd)
        {
            Voucher voucher = _mapper.Map<Voucher>(voucherToAdd);
            await _voucherRepository.AddAsync(voucher);
        }
        public async Task DeleteVoucher(int voucherId)
        {
            await _voucherRepository.DeleteByIdAsync(voucherId);
        }
        public async Task UpdateVoucherUser(int voucherId, int voucherId2)
        {
            Voucher voucher = await _voucherRepository.ListAllAsync()
                .Include(v => v.Owner)
                .Include(v => v.Merchant)
                .FirstOrDefaultAsync(v => v.Id == voucherId);
            Voucher voucher2 = await _voucherRepository.ListAllAsync()
                .Include(v => v.Owner)
                .Include(v => v.Merchant)
                .FirstOrDefaultAsync(v => v.Id == voucherId2);
            await _bidRepository.RemoveByCondition(b => b.VoucherId == voucherId && b.VoucherBidId == voucherId2);
            int temp = voucher.UserId;
            voucher.UserId = voucher2.UserId;
            voucher2.UserId = temp;
            await _voucherRepository.UpdateVoucher(voucher);
            await _voucherRepository.UpdateVoucher(voucher2);
            Console.WriteLine("Send " + voucher.Owner.PhoneNumber + " VoucherNumber " + voucher.VoucherNumber + " voucher Price " + voucher.Price + " voucher Merchant " + voucher.Merchant.Name);
            Console.WriteLine("Send " + voucher2.Owner.PhoneNumber + " VoucherNumber " + voucher2.VoucherNumber + " voucher Price " + voucher2.Price + " voucher Merchant " + voucher2.Merchant.Name);

            await _voucherRepository.DeleteByIdAsync(voucherId);
            await _voucherRepository.DeleteByIdAsync(voucherId2);
            //send whatsapp with new voucher
        }

        public async Task AddBid(BidToAddDTO bidToAdd)
        {
            Voucher voucher = await _voucherRepository.GetByIdAsync(bidToAdd.VoucherId);
            Voucher voucher2 = await _voucherRepository.GetByIdAsync(bidToAdd.VoucherBidId);
            if (voucher.UserId == voucher2.UserId)
            {
                throw new Exception("Cannot bid with your own voucher");
            }
            if (await _bidRepository.ListAllAsync().AnyAsync(b => b.VoucherBidId == bidToAdd.VoucherBidId && b.VoucherId == bidToAdd.VoucherId))
            {
                return;
            }

            var bid = _mapper.Map<Bid>(bidToAdd);
            await _bidRepository.AddAsync(bid);
        }
        public async Task<List<Bid>> GetBidByVoucherId(int voucherId)
        {
            return await _bidRepository.ListAllAsync().Where(b => b.VoucherId == voucherId).ToListAsync();
        }
        public async Task<Bid> AcceptBid(int bidId)
        {
            var bid = await _bidRepository.GetByIdAsync(bidId);
            if (await _bidRepository.ListAllAsync().AnyAsync(b => bid.VoucherId == b.VoucherId && b.IsAccepted == true))
            {
                throw new Exception("Bid was accpeted before");
            }
            bid.IsAccepted = true;
            await _bidRepository.RemoveByCondition(b => b.VoucherId == bid.VoucherId);
            await _bidRepository.SaveChanges();
            return bid;
        }


        public async Task<Pagination<Merchant>> GetAllMerchants(MerchantSpecParams specs)
        {
            var query1 = _merchantRepository.ListAllAsync();
            if (!string.IsNullOrEmpty(specs.Search))
            {
                query1 = query1.Where(m => m.Name == specs.Search);
            }
           var query = query1.Take(specs.PageSize * (specs.PageIndex))
                .Skip(specs.PageSize * (specs.PageIndex - 1));

            
            var data = await query.ToListAsync();
            return new Pagination<Merchant>
            {
                Data = data,
                Count = await query1.CountAsync(),
                PageIndex = specs.PageIndex,
                PageSize = specs.PageSize
            };
        }
        public DecryptedData DecryptData(string data)
        {
            var dataString = AesCypherHelpers.Decrypt(data, "c8o5P2jFmO9oJp2jwv7kougqK8yuTv8WJLXApFcP1Xo=", "DSncouW2MffV+StRD0RZHg==");
            return JsonConvert.DeserializeObject<DecryptedData>(dataString);

        }


    }
}
