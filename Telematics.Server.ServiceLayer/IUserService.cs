using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telematics.Server.Data.Json;

namespace Telematics.Server.ServiceLayer
{
    public interface IUserService
    {

        IEnumerable<User> GetUsers();

    }
}
