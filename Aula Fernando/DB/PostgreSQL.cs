using Npgsql;
using System;
using System.Collections.Generic;

namespace Aula_Fernando.DB
{
    public class PostgreSQL : IDatabase
    {

        private readonly string connectionString = @"Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=WPFDB";

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }

        public List<User> LoadDBList()
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    List<User> list = new List<User>();
                    string query = $@"SELECT * FROM users";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    con.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User()
                        {
                            //(0) é a id, (1) é o created_at
                            CPF = reader.GetString(2),
                            Name = reader.GetString(3),
                            Email = reader.GetString(4),
                            Password = reader.GetString(5)
                        };
                        list.Add(user);
                    }
                    con.Close();
                    return list;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DB_AddUser(User user)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    string query = $@"INSERT INTO users (cpf, name, email, password) VALUES   ('{user.CPF}','{user.Name}','{user.Email}','{user.Password}')";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public void DB_EditUser(User user)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {

                    string query = $@"UPDATE users SET name='{user.Name}',
                                           email='{user.Email}',
                                           password='{user.Password}'
                                           WHERE cpf='{user.CPF}'";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DB_RemoveUser(User user)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    string query = $@"DELETE FROM users WHERE cpf='{user.CPF}'";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
