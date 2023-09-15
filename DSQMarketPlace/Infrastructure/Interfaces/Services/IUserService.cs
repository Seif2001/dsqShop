
using Infrastructure.DTOs;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Services
{
    public interface IUserService
    {
        Task<int> AddUser(UserToAddDTO userToAdd);
        Task<List<User>> GetAllUsers();
        Task<List<UserVoucherToReturnDTO>> GetCurrentVouchers(int userId);
        Task<User> GetUserByNumber(string phoneNumber);
        Task<User> GetUserById(int id);
    }
}
