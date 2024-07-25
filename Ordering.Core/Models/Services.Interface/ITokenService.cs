using Microsoft.AspNetCore.Identity;
using Ordering.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Models.Services.Interface
{
    public interface ITokenService
    {

        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> _userManager);    
    }
}
