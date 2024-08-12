using ApiServfyNotifications.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.Application.Services.Contracts
{
    public interface IUserService
    {
        Task<User> GetUserAsync(long id);
    }
}
