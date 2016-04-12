using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace TheWorld.Models
{
    public class WorldUser : IdentityUser
    {
        //public DateTime FirstWebPost { get; set; }
       // public bool Administrator { get; set; }
       public byte[] Image { get; set; }
        
    }
}