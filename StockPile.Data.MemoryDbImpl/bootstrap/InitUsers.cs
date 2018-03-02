using StockPile.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace StockPile.Data.MemoryDbImpl
{
    internal class InitUsers
    {
        public static Dictionary<Guid, User> GetUsers(string path)
        {
            Dictionary<Guid, User> users = new Dictionary<Guid, User>();

            using (var reader = File.OpenText(path + "users.csv"))
            {
                using (var csv = new CsvHelper.CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        User usr = new User()
                        {
                            Id = csv.GetField<Guid>(0),
                            Name = csv.GetField(1),
                            Email = csv.GetField(2)
                        };
                        users.Add(usr.Id, usr);
                    }
                }
            }

            return users;
        }
    }
}
