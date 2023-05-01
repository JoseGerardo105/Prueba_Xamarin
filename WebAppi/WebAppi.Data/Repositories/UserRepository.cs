using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppi.Model;

namespace WebAppi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLConfiguration _configuration;

        public UserRepository(MySQLConfiguration configuration)
        {
            _configuration = configuration; 
        }

        protected MySqlConnection dbConnection ()
        { 
            return new MySqlConnection(_configuration.ConnectionString);
        }
       

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var db = dbConnection();
            var sql = @"SELECT id, name, lastname, phone, email FROM user";

            return await db.QueryAsync<User>(sql, new { });
        }

        public async Task<User> GetDetails(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT id, name, lastname, phone, email FROM users WHERE id = @Id";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<bool> InsertUser(User user)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO users(name, lastname, phone, email) 
                        VALUES(@Name, @Lastname, @Phone, @Email) ";

            var result = await db.ExecuteAsync(sql, new { 
                user.Name, user.Lastname, user.Phone, user.Email            
            });

            return result > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var db = dbConnection();
            var sql = @"UPDATE users
                        SET name=@Name,
                            lastname=@Lastname,
                            phone=@Phone,
                            email=@Email
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new
            {
                user.Name,
                user.Lastname,
                user.Phone,
                user.Email
            });

            return result > 0;
        }
        public async Task<bool> DeleteUser(User user)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM users WHERE id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = user.Id });

            return result > 0;
        }
    }
}
