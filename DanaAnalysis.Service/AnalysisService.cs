using DataAnalysis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DanaAnalysis.Service
{
    public class AnalysisService
    {
        private List<Sale> _sales = new List<Sale>();
        private List<Client> _clients = new List<Client>();
        private List<Salesman> _salesmen = new List<Salesman>();

        public void RegisterRows(string[] rows)
        {
            foreach (var row in rows)
            {
                Register(row);
            }
        }

        public string DataAnalysisResult() => $"{_clients.Count}|{_salesmen.Count}|{MostExpensiveSaleId()}|{WorstSalesman()}";

        private void Register(string row)
        {
            string[] split = Regex.Split(row, "ç", RegexOptions.None);

            switch (split[0])
            {
                case "001":
                    _salesmen.Add(new Salesman(split[1],split[2], Convert.ToDouble(split[3], new CultureInfo("en-US"))));
                    break;
                case "002":
                    _clients.Add(new Client(split[1], split[2], split[3]));
                    break;
                case "003":
                    _sales.Add(new Sale(Convert.ToInt32(split[1]), split[2], split[3]));
                    break;
                default:
                    throw new InvalidOperationException("Registro inválido");
            }
        }

        private long MostExpensiveSaleId()
        {
            int mostExpensiveSaleId = 0;
            double mostExpensivePrice = 0;

            foreach (var sale in _sales)
            {
                double purchaseTotal = TotalSale(sale);
                if (mostExpensivePrice < purchaseTotal)
                {
                    mostExpensiveSaleId = sale.Id;
                    mostExpensivePrice = purchaseTotal;
                }
            }

            return mostExpensiveSaleId;
        }

        private string WorstSalesman()
        {
            double worstSaleValue = 0;
            string salesmanName = "";
            foreach (var sale in _sales)
            {
                var totalSale = TotalSale(sale);
                if (worstSaleValue == 0 || worstSaleValue > totalSale)
                {
                    worstSaleValue = totalSale;
                    salesmanName = sale.SalesmanName;
                }
            }

            return salesmanName;
        }

        private double TotalSale(Sale sale)
        {
            double purchaseTotal = 0;
            foreach (var item in sale.Items)
            {
                purchaseTotal += item.Price * item.Quantity;
            }

            return purchaseTotal;
        }
    }
}
