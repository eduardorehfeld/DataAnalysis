using System;
using System.Collections.Generic;
using System.Globalization;

namespace DataAnalysis.Domain.Entities
{
    public class Sale
    {
        public Sale(int id, string item, string salesmanName)
        {
            Id = id;
            Items = CreateSaleItems(item);
            SalesmanName = salesmanName;
        }

        public int Id { get; set; }
        public List<SaleItem> Items { get; set; }
        public string SalesmanName { get; set; }

        private List<SaleItem> CreateSaleItems(string itemsRegistry)
        {
            List<SaleItem> saleItems = new List<SaleItem>();
            string[] items = itemsRegistry[1..(itemsRegistry.Length - 1)].Split(",");
            foreach (var item in items)
            {
                string[] splittedItem = item.Split("-");
                var saleItem = new SaleItem(
                    Convert.ToInt32(splittedItem[0]), 
                    Convert.ToInt64(splittedItem[1]), 
                    Convert.ToDouble(splittedItem[2], new CultureInfo("en-US")));

                saleItems.Add(saleItem);
            }

            return saleItems;
        }
    }
}