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
using System.Collections.Generic;
using Core.Common.Extensions;
using Bom.Data.Contracts.DTOs;

namespace Bom.Desktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductTreeViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public ProductTreeViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            EditStockCommand = new DelegateCommand<int>(OnEditStockCommand);
            EditPartCommand = new DelegateCommand<int>(OnEditPartCommand);
            //DeleteProductTreeCommand = new DelegateCommand<int>(OnDeleteProductTreeCommand);
            //AddProductTreeCommand = new DelegateCommand<object>(OnAddProductTreeCommand);

        }

        readonly IServiceFactory _serviceFactory;

        EditPartViewModel _currentPartViewModel;
        public DelegateCommand<int> EditPartCommand { get; private set; }
        EditStockViewModel _currentStockViewModel;

        public DelegateCommand<int> EditStockCommand { get; private set; }
        /*
                        public DelegateCommand<int> DeleteProductTreeCommand { get; private set; }
                        public DelegateCommand<object> AddProductTreeCommand { get; private set; }
        */
        public override string ViewTitle
        {
            get { return "Product Tree"; }
        }

        public event EventHandler<EditPartViewModel> OpenEditPartWindow;
        /*
                public event CancelEventHandler ConfirmDelete;
*/
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;
        public event EventHandler<EditStockViewModel> OpenEditStockWindow;

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

        ObservableCollection<HierarchyNode<ProductTree>> _productTree;

        public ObservableCollection<HierarchyNode<ProductTree>> ProductTree
        {
            get { return _productTree; }
            set
            {
                if (_productTree != value)
                {
                    _productTree = value;
                    OnPropertyChanged(() => ProductTree, false);
                }
            }
        }
        protected override void OnViewLoaded()
        {
            _productTree = new ObservableCollection<HierarchyNode<ProductTree>>();

            WithClient(_serviceFactory.CreateClient<IPartService>(), partClient =>
            {
                _productTree.Add(partClient.GetProductTree());
            });
        }
        void OnEditStockCommand(int partId)
        {
            if (partId > 0)
            {
                CurrentStockViewModel = new EditStockViewModel(_serviceFactory, partId);
                CurrentStockViewModel.StockUpdated += CurrentStockViewModel_StockUpdated;
                CurrentStockViewModel.CancelEditStock += CurrentStockViewModel_CancelEvent;
            }

            if (OpenEditStockWindow != null) OpenEditStockWindow(this, CurrentStockViewModel);
        }
        void OnEditPartCommand(int partId)
        {
            if (partId > 0)
            {
                CurrentPartViewModel = new EditPartViewModel(_serviceFactory, partId);
                CurrentPartViewModel.PartUpdated += CurrentPartViewModel_PartUpdated;
                CurrentPartViewModel.CancelEditPart += CurrentPartViewModel_CancelEvent;
            }

            if (OpenEditPartWindow != null) OpenEditPartWindow(this, CurrentPartViewModel);
        }

        /*
                        void OnAddProductTreeCommand(object arg)
                        {
                            CurrentPartViewModel = new EditPartViewModel(_serviceFactory, new Part());
                            CurrentPartViewModel.PartUpdated += CurrentPartViewModel_PartUpdated;
                            CurrentPartViewModel.CancelEditPart += CurrentPartViewModel_CancelEvent;

                            if (OpenEditPartWindow != null) OpenEditPartWindow(this, CurrentPartViewModel);
                        }
*/
        void CurrentStockViewModel_StockUpdated(object sender, StockEventArgs e)
        {
            if (!e.IsNew)
            {
                //ToDo This doesn't update UI
                UpdateProductTree(_productTree, e.Stock);
            }
            else
            {
                //_stocks.Add(new Part(e.Stock));
            }

            CurrentStockViewModel = null;
        }

        private void UpdateProductTree(IEnumerable<HierarchyNode<ProductTree>> node, Part part)
        {
            foreach (var p in node)
            {
                if (p.Entity.Id == part.Id)
                {
                    p.Entity.PartDescription = part.Description;
                    p.Entity.Count = part.Count;
                    p.Entity.CountDate = part.CountDate;
                    p.Entity.OnOrder = part.OnOrder;
                    p.Entity.Capability = part.Capability;
                    p.Entity.Demand = part.Demand;
                    p.Entity.Notes = part.Notes;
                }
                if (p.ChildNodes.Count() > 0)
                {
                    UpdateProductTree(p.ChildNodes, part);
                }
            }
        }

        void CurrentStockViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentStockViewModel = null;
        }

        void CurrentPartViewModel_PartUpdated(object sender, PartEventArgs e)
        {
            if (!e.IsNew)
            {
                //Part stock = _stocks.Single(item => item.Id == e.Part.Id);
                //if (stock != null)
                //{
                //    stock.Type = e.Part.Type;
                //    stock.Number = e.Part.Number;
                //    stock.Description = e.Part.Description;
                //    stock.IsOwnMake = e.Part.IsOwnMake;
                //    stock.Length = e.Part.Length;
                //    stock.OwnCost = e.Part.OwnCost;
                //    stock.ComponentsCost = e.Part.ComponentsCost;
                //    stock.Count = e.Part.Count;
                //    stock.CountDate = e.Part.CountDate;
                //    stock.OnOrder = e.Part.OnOrder;
                //    stock.Notes = e.Part.Notes;
                //}
            }
            else
            {
                //_stocks.Add(new Part(e.Part));
            }

            CurrentPartViewModel = null;
        }

        void CurrentPartViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentPartViewModel = null;
        }
        /*
        void OnDeleteProductTreeCommand(int stockId)
        {
            CancelEventArgs args = new CancelEventArgs();
            if (ConfirmDelete != null)
                ConfirmDelete(this, args);

            if (!args.Cancel)
            {
                WithClient(_serviceFactory.CreateClient<IPartService>(), partClient =>
                {
                    partClient.DeletePart(stockId);
                    _stocks.Remove(_stocks.Single(i => i.Id == stockId));
                });
            }
        }
*/
    }
}
