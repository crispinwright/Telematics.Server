using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
//using Telematics.Server.Data.DataModels;
using Telematics.Server.Data.DataModels;
using User = Telematics.Server.Data.Json.User;

namespace Telematics.Server.ServiceLayer
{
	public interface IUserService
	{
		IEnumerable<User> GetUsers();
	}

	public class UserService : IUserService
	{
		public IEnumerable<User> GetUsers()
		{
			using (var context = new geoEntities())
			{
				return context.Users.ToJson();
			}
		}
	}

	public static class JsonTransforms
	{
		public static IEnumerable<User> ToJson(this DbSet<Data.DataModels.User> databaseUsers)
		{
			return databaseUsers.ToArray().Select(x => x.ToJson());
		}

		public static User ToJson(this Data.DataModels.User databaseUser)
		{
			return new User()
			       {
				       UserID = databaseUser.ID,
			       };
		}
	}
}