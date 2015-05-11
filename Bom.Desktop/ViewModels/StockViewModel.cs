using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bom.Desktop.ViewModels
{
    public class StockViewModel : BaseViewModel
    {
        private IEnumerable<StockItem> _stockItems = null;

        public StockViewModel()
        {
            _stockItems = getStock();
        }

        public IEnumerable<StockItem> StockItems { get { return _stockItems; } }

        private IEnumerable<StockItem> getStock()
        {
            return new List<StockItem>
            {
                new StockItem
                {
                    Id = 1,
                    Description = "Description 1",
                    DateOfCount = DateTime.Now,
                    Count = 15,
                    Cost = 900,
                    TotalCost = 900 * 15
                },
                new StockItem
                {
                    Id = 2,
                    Description = "Test Description 2",
                    DateOfCount = new DateTime(2012, 4 ,8),
                    Count = 9,
                    Cost = 500,
                    TotalCost = 500 * 9
                },
                new StockItem
                {
                    Id = 3,
                    Description = "Item number three",
                    DateOfCount = new DateTime(2015, 7 ,3),
                    Count = 25,
                    Cost = 200,
                    TotalCost = 200 * 25
                }
            };
        }
    }
}
