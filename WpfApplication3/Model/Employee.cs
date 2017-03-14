using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3.Model
{
	public class Employee:DatabaseObject
	{

		private string name;

		public bool Sex { get; set; }

		public decimal Amount { get; set; }

		public string Phone
		{
			get
			{
				return phone;
			}

			set
			{
				phone = value;
				OnPropertyChanged();
			}
		}

		public string Name
		{
			get
			{
				return name;
			}

			set
			{
				name = value;
				OnPropertyChanged();
			}
		}


		public EntityState entityState { get; set; }
		private Employee TempData { get; set; }

		private void OnPropertyChanged([CallerMemberName] string propertyName = "onne passed")
		{
			this.entityState = EntityState.Modified;
			//TempData = this;

			//throw new NotImplementedException();
		}
		//private void OnPropertyChanged([CallerFilePath] string propertyName = "onne passed")
		//{
		//	throw new NotImplementedException();
		//}

		//private void OnPropertyChanged([CallerFilePath] string propertyName = "onne passed")
		//{
		//	throw new NotImplementedException();
		//}


		private string phone;


		public void RaiseCanExecuteChanged()
		{

		}
	}
}
