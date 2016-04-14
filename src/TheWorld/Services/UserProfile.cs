using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;

namespace AspNetIdentity.Services
{
    public class UserProfile
    {
        public UserProfile(IServiceProvider services)
        {
            UserManager = services.GetRequiredService<UserManager<WorldUser>>();
        }

        public UserManager<WorldUser> UserManager { get; set; }

        public async Task<byte[]> Image(string username)
        {
            WorldUser result = await UserManager.FindByNameAsync(username);
            return result.Image;
        }
    }
}
