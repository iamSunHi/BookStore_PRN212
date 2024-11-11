using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
	public class OrderHeaderVM : INotifyPropertyChanged
	{
		private string _name = null!;
		private string? _phoneNumber;
		private string? _streetAddress;
		private string? _city;
		private string? _state;
		private string? _postalCode;
		private decimal _orderTotal;
		private ProductVM _productVM;

		public OrderHeaderVM()
		{
			_productVM = new ProductVM();
		}
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		public string? PhoneNumber
		{
			get => _phoneNumber;
			set
			{
				_phoneNumber = value;
				OnPropertyChanged();
			}
		}

		public string? StreetAddress
		{
			get => _streetAddress;
			set
			{
				_streetAddress = value;
				OnPropertyChanged();
			}
		}

		public string? City
		{
			get => _city;
			set
			{
				_city = value;
				OnPropertyChanged();
			}
		}

		public string? State
		{
			get => _state;
			set
			{
				_state = value;
				OnPropertyChanged();
			}
		}

		public string? PostalCode
		{
			get => _postalCode;
			set
			{
				_postalCode = value;
				OnPropertyChanged();
			}
		}

		public ProductVM ProductVM
		{
			get => _productVM;
			set
			{
				_productVM = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(OrderTotal));
			}
		}

		public double OrderTotal => ProductVM.Quantity * ProductVM.Product.Price;


		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

}

