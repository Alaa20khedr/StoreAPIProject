using Store.Data.Entities.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.TokenServices
{
    public interface ITokenServices
    {
        string GenerateToken(AppUser appUser);
    }
}
