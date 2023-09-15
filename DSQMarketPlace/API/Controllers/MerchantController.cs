using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MerchantController : ControllerBase
    {
        private readonly IGenericRepository<Merchant> _merchantRepository;
        private readonly IVoucherService _voucherService;

        public MerchantController(IGenericRepository<Merchant> merchantRepository, IVoucherService voucherService)
        {
            _merchantRepository = merchantRepository;
            _voucherService = voucherService;
        }
        [HttpGet("GetAllMerchants")]
        public async Task<Pagination<Merchant>> GetMerchantsAsync([FromQuery] MerchantSpecParams specs)
        {
            return await _voucherService.GetAllMerchants(specs);
        }
        [HttpPost("AddMerchant")]
        public async Task AddMerchant(string name, string picture)
        {
            await _merchantRepository.AddAsync(new Merchant() { Name = name, PictureUrl = picture });
        }

    }
}
