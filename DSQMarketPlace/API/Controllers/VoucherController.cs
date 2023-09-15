using Infrastructure.DTOs;
using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        private readonly IUserService _userService;

        public VoucherController(IVoucherService voucherService, IUserService userService)
        {
            _userService = userService;
            _voucherService = voucherService;
        }
        [Authorize]
        [HttpGet("GetAllVouchers")]
        public async Task<Pagination<VoucherToReturnDTO>> GetAllVouchers([FromQuery] ProductSpecParams specParams)
        {
            return await _voucherService.GetVoucherPagination(specParams);
        }
        [HttpPost("CreateVoucher")]
        public async Task CreateVoucher(VoucherToAddDTO voucherToAdd)
        {
            await _voucherService.AddVoucher(voucherToAdd);
        }
        [HttpPost("MakeBid")]
        public async Task MakeBid(BidToAddDTO bidToAdd)
        {
            await _voucherService.AddBid(bidToAdd);
        }
        [HttpGet("AcceptBid")]
        public async Task AcceptBid(int voucherId, int voucherBidId)
        {
            await _voucherService.UpdateVoucherUser(voucherId, voucherBidId);
        }
        

        [HttpPost("addUserVoucher")]
        public async Task AddUserVoucher(string data)
        {
            DecryptedData dataToAdd = _voucherService.DecryptData(data);
            int id = await _userService.AddUser(new UserToAddDTO()
            {
                PhoneNumber = dataToAdd.UserPhoneNumber
            });
            await _voucherService.AddVoucher(new VoucherToAddDTO()
            {
                MerchantId = dataToAdd.MerchantId,
                Price = dataToAdd.Price,
                VoucherNumber = dataToAdd.VoucherNumber,
                UserId = id
            });
        }
    }
}
