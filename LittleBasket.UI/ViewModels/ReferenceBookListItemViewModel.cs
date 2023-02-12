using LittleBasket.Domain.Base;
using LittleBasket.Domain.Interfaces.Commands;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.ViewModels
{
    public class ReferenceBookListItemViewModel : ViewModelBase
    {
        public Product Product { get; set; }
        public Guid ProductId => Product.Id;
        public string ProductName => Product.Name;

        public bool IsVisible
        {
            get
            {
                return Product.IsVisible;
            }
            set
            {
                Product.IsVisible = value;
                OnPropertyChanged(nameof(IsVisible));
                UpdateProductCommand.Execute(Product);
            }
        }

        public IUpdateCommand<Product> UpdateProductCommand { get; }

        public ReferenceBookListItemViewModel(Product product, IUpdateCommand<Product> updateProductCommand)
        {
            UpdateProductCommand = updateProductCommand;
            Product = product;
        }
    }
}
