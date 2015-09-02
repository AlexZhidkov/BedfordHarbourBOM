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

namespace Bom.Desktop.ViewModels
{
    public class Person
    {
        //List<Person> _children = new List<Person>();
        public IList<Person> Children { get; set; }

        public string Name { get; set; }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductTreeViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public ProductTreeViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            var rootPerson = new Person
            {
                Name = "Root Person",
                Children = new List<Person>
                {
                    new Person {Name = "Child1",
                        Children = new List<Person>()
                    },
                    new Person {Name = "Child2",
                        Children = new List<Person>()
                    }
                }

            };

            _rootPerson = new PersonViewModel(rootPerson);

            _firstGeneration = new ReadOnlyCollection<PersonViewModel>(
                new PersonViewModel[]
                {
                    _rootPerson
                });


            //EditProductTreeCommand = new DelegateCommand<Part>(OnEditProductTreeCommand);
            //EditPartCommand = new DelegateCommand<int>(OnEditPartCommand);
            //DeleteProductTreeCommand = new DelegateCommand<int>(OnDeleteProductTreeCommand);
            //AddProductTreeCommand = new DelegateCommand<object>(OnAddProductTreeCommand);

        }
        /*
                public ProductTreeViewModel(Person rootPerson)
                {
                    _rootPerson = new PersonViewModel(rootPerson);

                    _firstGeneration = new ReadOnlyCollection<PersonViewModel>(
                        new PersonViewModel[]
                        {
                            _rootPerson
                        });

                    //_searchCommand = new SearchFamilyTreeCommand(this);
                }
        */
        readonly IServiceFactory _serviceFactory;

        PersonViewModel _rootPerson;
        ReadOnlyCollection<PersonViewModel> _firstGeneration;

        /*
                        EditStockViewModel _currentStockViewModel;
                        EditPartViewModel _currentPartViewModel;

                        public DelegateCommand<Part> EditProductTreeCommand { get; private set; }
                        public DelegateCommand<int> EditPartCommand { get; private set; }
                        public DelegateCommand<int> DeleteProductTreeCommand { get; private set; }
                        public DelegateCommand<object> AddProductTreeCommand { get; private set; }
        */
        public override string ViewTitle
        {
            get { return "ProductTree"; }
        }
        public ReadOnlyCollection<PersonViewModel> FirstGeneration
        {
            get { return _firstGeneration; }
        }

        /*
                public event CancelEventHandler ConfirmDelete;
                public event EventHandler<ErrorMessageEventArgs> ErrorOccured;
                public event EventHandler<EditStockViewModel> OpenEditProductTreeWindow;
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

                ObservableCollection<Part> _stocks;

                public ObservableCollection<Part> ProductTrees
                {
                    get { return _stocks; }
                    set
                    {
                        if (_stocks != value)
                        {
                            _stocks = value;
                            OnPropertyChanged(() => ProductTrees, false);
                        }
                    }
                }

                protected override void OnViewLoaded()
                {
                    _stocks = new ObservableCollection<Part>();

                    WithClient(_serviceFactory.CreateClient<IPartService>(), stockClient =>
                    {
                        Part[] stocks = stockClient.GetAllParts();
                        if (stocks != null)
                        {
                            foreach (Part stock in stocks)
                                _stocks.Add(stock);
                        }
                    });
                }

                void OnEditProductTreeCommand(Part stockItem)
                {
                    if (stockItem.Id > 0)
                    {
                        CurrentStockViewModel = new EditStockViewModel(_serviceFactory, stockItem);
                        CurrentStockViewModel.StockUpdated += CurrentStockViewModel_StockUpdated;
                        CurrentStockViewModel.CancelEditStock += CurrentStockViewModel_CancelEvent;
                    }

                    if (OpenEditProductTreeWindow != null) OpenEditProductTreeWindow(this, CurrentStockViewModel);
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

                void OnAddProductTreeCommand(object arg)
                {
                    CurrentPartViewModel = new EditPartViewModel(_serviceFactory, new Part());
                    CurrentPartViewModel.PartUpdated += CurrentPartViewModel_PartUpdated;
                    CurrentPartViewModel.CancelEditPart += CurrentPartViewModel_CancelEvent;

                    if (OpenEditPartWindow != null) OpenEditPartWindow(this, CurrentPartViewModel);
                }

                void CurrentStockViewModel_StockUpdated(object sender, StockEventArgs e)
                {
                    if (!e.IsNew)
                    {
                        Part stock = _stocks.Single(item => item.Id == e.Stock.Id);
                        if (stock != null)
                        {
                            stock.Type = e.Stock.Type;
                            stock.Number = e.Stock.Number;
                            stock.Description = e.Stock.Description;
                            stock.IsOwnMake = e.Stock.IsOwnMake;
                            stock.Length = e.Stock.Length;
                            stock.OwnCost = e.Stock.OwnCost;
                            stock.ComponentsCost = e.Stock.ComponentsCost;
                            stock.Count = e.Stock.Count;
                            stock.CountDate = e.Stock.CountDate;
                            stock.OnOrder = e.Stock.OnOrder;
                            stock.Notes = e.Stock.Notes;
                        }
                    }
                    else
                    {
                        _stocks.Add(new Part(e.Stock));
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
                        Part stock = _stocks.Single(item => item.Id == e.Part.Id);
                        if (stock != null)
                        {
                            stock.Type = e.Part.Type;
                            stock.Number = e.Part.Number;
                            stock.Description = e.Part.Description;
                            stock.IsOwnMake = e.Part.IsOwnMake;
                            stock.Length = e.Part.Length;
                            stock.OwnCost = e.Part.OwnCost;
                            stock.ComponentsCost = e.Part.ComponentsCost;
                            stock.Count = e.Part.Count;
                            stock.CountDate = e.Part.CountDate;
                            stock.OnOrder = e.Part.OnOrder;
                            stock.Notes = e.Part.Notes;
                        }
                    }
                    else
                    {
                        _stocks.Add(new Part(e.Part));
                    }

                    CurrentPartViewModel = null;
                }

                void CurrentPartViewModel_CancelEvent(object sender, EventArgs e)
                {
                    CurrentPartViewModel = null;
                }

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
