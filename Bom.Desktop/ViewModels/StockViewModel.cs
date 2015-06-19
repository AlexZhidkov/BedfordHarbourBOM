using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.Support;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StockViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public StockViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            EditStockCommand = new DelegateCommand<StockItemData>(OnEditStockCommand);
            EditPartCommand = new DelegateCommand<int>(OnEditPartCommand);
            DeleteStockCommand = new DelegateCommand<int>(OnDeleteStockCommand);
            AddStockCommand = new DelegateCommand<object>(OnAddStockCommand);

        }

        readonly IServiceFactory _serviceFactory;

        EditStockViewModel _currentStockViewModel;
        EditPartViewModel _currentPartViewModel;

        public DelegateCommand<StockItemData> EditStockCommand { get; private set; }
        public DelegateCommand<int> EditPartCommand { get; private set; }
        public DelegateCommand<int> DeleteStockCommand { get; private set; }
        public DelegateCommand<object> AddStockCommand { get; private set; }

        public override string ViewTitle
        {
            get { return "Stock"; }
        }
        public event CancelEventHandler ConfirmDelete;
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;
        public event EventHandler<EditStockViewModel> OpenEditStockWindow;
        public event EventHandler<EditPartViewModel> OpenEditPartWindow;

        public EditStockViewModel CurrentStockViewModel
        {
            get { return _currentStockViewModel; }
            set
            {
                if (_currentStockViewModel != value)
                {
                    _currentStockViewModel = value;
                    OnPropertyChanged(() => CurrentStockViewModel, false);
                }
            }
        }

        public EditPartViewModel CurrentPartViewModel
        {
            get { return _currentPartViewModel; }
            set
            {
                if (_currentPartViewModel != value)
                {
                    _currentPartViewModel = value;
                    OnPropertyChanged(() => CurrentPartViewModel, false);
                }
            }
        }

        ObservableCollection<StockItemData> _stocks;

        public ObservableCollection<StockItemData> Stocks
        {
            get { return _stocks; }
            set
            {
                if (_stocks != value)
                {
                    _stocks = value;
                    OnPropertyChanged(() => Stocks, false);
                }
            }
        }

        protected override void OnViewLoaded()
        {
            _stocks = new ObservableCollection<StockItemData>();

            WithClient(_serviceFactory.CreateClient<IStockService>(), stockClient =>
            {
                StockItemData[] stocks = stockClient.GetAllStockItems();
                if (stocks != null)
                {
                    foreach (StockItemData stock in stocks)
                        _stocks.Add(stock);
                }
            });
        }

        void OnEditStockCommand(StockItemData stockItem)
        {
            if (stockItem.StockId > 0)
            {
                CurrentStockViewModel = new EditStockViewModel(_serviceFactory, stockItem);
                CurrentStockViewModel.StockUpdated += CurrentStockViewModel_StockUpdated;
                CurrentStockViewModel.CancelEditStock += CurrentStockViewModel_CancelEvent;
            }

            if (OpenEditStockWindow != null) OpenEditStockWindow(this, CurrentStockViewModel);
        }

        void OnEditPartCommand(int partId)
        {
            if (partId > 0)
            {
                //ToDo CurrentPartViewModel = new EditPartViewModel(_serviceFactory, part);
                CurrentPartViewModel.PartUpdated += CurrentPartViewModel_PartUpdated;
                CurrentPartViewModel.CancelEditPart += CurrentPartViewModel_CancelEvent;
            }

            if (OpenEditPartWindow != null) OpenEditPartWindow(this, CurrentPartViewModel);
        }

        void OnAddStockCommand(object arg)
        {
            CurrentStockViewModel = new EditStockViewModel(_serviceFactory, new StockItemData());
            CurrentStockViewModel.StockUpdated += CurrentStockViewModel_StockUpdated;
            CurrentStockViewModel.CancelEditStock += CurrentStockViewModel_CancelEvent;

            if (OpenEditStockWindow != null) OpenEditStockWindow(this, CurrentStockViewModel);
        }

        void CurrentStockViewModel_StockUpdated(object sender, StockEventArgs e)
        {
            if (!e.IsNew)
            {
                StockItemData stock = _stocks.Single(item => item.StockId == e.Stock.Id);
                if (stock != null)
                {
                    stock.Count = e.Stock.Count;
                    stock.CountDate = e.Stock.CountDate;
                    stock.PartId = e.Stock.PartId;
                    stock.Cost = e.Stock.Cost;
                    stock.Notes = e.Stock.Notes;
                }
            }
            else
            {
                _stocks.Add(new StockItemData
                {
                    StockId = e.Stock.Id,
                    PartId = e.Stock.PartId,
                    PartDescription = e.PartDescription,
                    Count = e.Stock.Count,
                    CountDate = e.Stock.CountDate,
                    Cost = e.Stock.Cost,
                    Notes = e.Stock.Notes
                });
            }

            CurrentStockViewModel = null;
        }

        void CurrentStockViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentStockViewModel = null;
        }

        void CurrentPartViewModel_PartUpdated(object sender, PartEventArgs e)
        {
            if (!e.IsNew)
            {
                foreach (var stock in _stocks.Where(item => item.PartId == e.Part.Id))
                {
                    stock.PartDescription = e.Part.Description;
                }
            }
            CurrentPartViewModel = null;
        }

        void CurrentPartViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentPartViewModel = null;
        }

        void OnDeleteStockCommand(int stockId)
        {
            CancelEventArgs args = new CancelEventArgs();
            if (ConfirmDelete != null)
                ConfirmDelete(this, args);

            if (!args.Cancel)
            {
                WithClient(_serviceFactory.CreateClient<IStockService>(), suplierClient =>
                {
                    suplierClient.DeleteStock(stockId);
                    _stocks.Remove(_stocks.Single(i => i.StockId == stockId));
                });
            }
        }

    }
}
