using StockPile.Model.Inventory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace StockPile.Data.MemoryDbImpl.bootstrap
{
    internal class InitInventory
    {
        public static Dictionary<Guid, Product> GetProducts(string path)
        {
            Dictionary<Guid, Product> products = new Dictionary<Guid, Product>();

            using (var reader = File.OpenText(path + "products.csv"))
            {
                using (var csv = new CsvHelper.CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        Product p = new Product()
                        {
                            Id = csv.GetField<Guid>(0),
                            Name = csv.GetField(1),
                            Description = csv.GetField(2),
                            ImageUrl = csv.GetField(3),
                            Brand = csv.GetField<Guid>(4),
                            Category = csv.GetField(5),
                            Created = DateTime.Now
                        };

                        Stock s = new Stock()
                        {
                            Quantity = csv.GetField<int>(6),
                            Cost = csv.GetField<double>(7),
                            Price = csv.GetField<double>(8)
                        };

                        p.Stock = s;

                        products.Add(p.Id, p);
                    }
                }
            }

            return products;
        }

        public static Dictionary<Guid, Brand> GetBrands(string path)
        {
            Dictionary<Guid, Brand> brands = new Dictionary<Guid, Brand>();
            using (var reader = File.OpenText(path + "brands.csv"))
            {
                using (var csv = new CsvHelper.CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        Brand b = new Brand()
                        {
                            Id = csv.GetField<Guid>(0),
                            Name = csv.GetField(1),
                            Description = csv.GetField(2)
                        };

                        brands.Add(b.Id, b);
                    }
                }
            }

            return brands;
        }

        public static List<string> GetCategories(string path)
        {
            List<string> categories = new List<string>();
            
            using (var reader = File.OpenText(path + "categories.csv"))
            {
                using (var csv = new CsvHelper.CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        categories.Add(csv.GetField(0));
                    }
                }
            }

            return categories;
        }
    }
}
