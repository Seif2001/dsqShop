using AutoMapper;
using Infrastructure.DTOs;
using Infrastructure.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IVoucherRepository _voucherRepository;


        public UserService(IUserRepository userRepository, IVoucherRepository voucherRepository)
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserToAddDTO, User>();
                cfg.CreateMap<Voucher, VoucherToReturnDTO>()
                .ForMember(d => d.MerchantName, o => o.MapFrom(s => s.Merchant.Name));
                cfg.CreateMap<Voucher, UserVoucherToReturnDTO>()
                .ForMember(d => d.MerchantName, o => o.MapFrom(s => s.Merchant.Name));
                cfg.CreateMap<BidToAddDTO, Bid>();
            }));
            _userRepository = userRepository;
            _voucherRepository = voucherRepository;
        }

        public async Task<int> AddUser(UserToAddDTO userToAdd)
        {
            if (await _userRepository.IsUserThereAsync(userToAdd.PhoneNumber))
            {
                User user = await _userRepository.ListAllAsync().FirstOrDefaultAsync(u => u.PhoneNumber == userToAdd.PhoneNumber);

                return user.Id;
            }
            else
            {
                User user = _mapper.Map<User>(userToAdd);
                await _userRepository.AddAsync(user);
                return user.Id;
            }
        }            
        public async Task<bool> IsUserThereAsync(string phoneNumber)
        {
            return await _userRepository.IsUserThereAsync(phoneNumber);
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.ListAllAsync().ToListAsync();
        }
        public async Task<List<UserVoucherToReturnDTO>> GetCurrentVouchers(int userId)
        {
            var vouchers = await _userRepository.GetCurrentVouchers(userId);
            var returnVouchers = new List<UserVoucherToReturnDTO>();
            foreach (var v in vouchers)
            {
                var userReturnVoucher = _mapper.Map<UserVoucherToReturnDTO>(v);
                foreach(var bid in v.VoucherBids)
                {
                    var userVoucherDTO = await _voucherRepository.ListAllAsync()
                        .Include(v => v.Merchant)
                        .FirstOrDefaultAsync(voucher => voucher.Id == bid.VoucherBidId);
                    var userVoucher = _mapper.Map<VoucherToReturnDTO>(userVoucherDTO);
                    if (userVoucher != null)
                    {
                        userReturnVoucher.BiddedVouchers.Add(userVoucher);
                    }
                }
                returnVouchers.Add(userReturnVoucher);
            }
            return returnVouchers;
        }

        public Task<User> GetUserByNumber(string phoneNumber)
        {
            return _userRepository.ListAllAsync().FirstOrDefaultAsync(p=>p.PhoneNumber == phoneNumber);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
    }

}
