using Microsoft.Extensions.Configuration;
using Payment.Solution.DataAccessLayer.Interfaces;
using Payment.Solution.RuntimeModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payment.Solution.DataAccessLayer.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object Create(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Get(string where = null)
        {
            var userList = new List<User> { };

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("usp_SEL_User", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Filter", where);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userList.Add(new User
                        {
                            IdentityNumber = Convert.ToString(reader[nameof(User.IdentityNumber)]),
                            Password = Convert.ToString(reader[nameof(User.Password)]),
                            UserType = Convert.ToByte(reader[nameof(User.UserType)])
                        });
                    }
                }
            }

            return userList;
        }

        public void Update(string where, string set)
        {
            throw new NotImplementedException();
        }
    }
}
