using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebPosterDomain.Entities;
using WebPosterDomain.Interfaces.Common;
using WebPosterInfrastructure.Configuration;
using WebPosterInfrastructure.Repository.Generic;

namespace WebPosterInfrastructure.Repository.Common
{
    public class UserReport : GenericRepo<User>, IUser
    {
        private readonly DbContextOptionsBuilder<Context> _OptionsBuilder;
        public UserReport()
        {

            _OptionsBuilder = new DbContextOptionsBuilder<Context>();
        }
        public string LogingCheck(string login, string pass)
        {
            string role = string.Empty;

            using (var obj = new Context(_OptionsBuilder.Options))
            {
                var param = new SqlParameter("@UserName", login);
                var param2 = new SqlParameter("@Password", pass);
                var paramOut = new SqlParameter("@RoleValid", System.Data.SqlDbType.NVarChar, 250, System.Data.ParameterDirection.Output, true, 0, 0, null, 0, role);
                obj.Database.ExecuteSqlRaw("[Admin].[Login]  @UserName, @Password, @RoleValid", param, param2, paramOut);
                obj.SaveChanges();
            }

            return role;
        }
    }
}
