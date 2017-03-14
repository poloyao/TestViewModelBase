using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using WpfApplication3.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DevExpress.Mvvm.POCO;

namespace WpfApplication3.ViewModels
{
	[POCOViewModel]
	public class MainViewModel : MyModelBase<Employee> //:INotifyPropertyChanged
	{
		//public event PropertyChangedEventHandler PropertyChanged;

		//private void FirePropertyChanged(string propertyName)
		//{
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		//}

		//public void RaisePropertyChanged(string property)
		//{

		//}
		//[Command(CanExecuteMethodName = "CanSave")]
		//public void Save()
		//{
		 
		//}


		//public bool CanSave()
		//{
		//	//return !string.IsNullOrEmpty(this.emp.Name);
		//	return this.emp.entityState == EntityState.Modified;
		//}

		//private void OnEmpChanged()
		//{
		//	//TempData = this;

		//	//throw new NotImplementedException();
		//}

		public MainViewModel()
		{
			//this.RaiseCanExecuteChanged(x => x.Save());
			//this.emp = new Employee();
			//emp.entityState = EntityState.Added;
			//base.State = EntityState.Added;
			base.Entity = new Employee();
		}

		
	}


	[POCOViewModel]
	public class MyModelBase<T> : ViewModelBase where T :class
	{

		protected T Entity //{ get; set; }
		{
			get
			{
				return GetProperty(() => Entity);
			}
			set
			{
				SetProperty(() => Entity, value);
				if (TempEntity == null)
					TempEntity = Entity;
			}
		}

		protected T TempEntity { get; set; }


		protected EntityState State { get; set; }


		EntityState GetState()
		{
			//return this.State;
			if (DataConverter_StringWithGuid(Entity, TempEntity))
			{
				return EntityState.Detached;
			}
			return EntityState.Modified;

		}

		static bool DataConverter_StringWithGuid<T1, T2>(T1 t1,  T2 t2) where T1 : class where T2 : class
		{

			System.Reflection.PropertyInfo[] propertiesT1 = t1.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

			System.Reflection.PropertyInfo[] propertiesT2 = t2.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);


			foreach (System.Reflection.PropertyInfo itemT2 in propertiesT2)
			{
				foreach (System.Reflection.PropertyInfo itemT1 in propertiesT1)
				{
					if (itemT1.Name == itemT2.Name)
					{
						var temp1 = itemT1.GetValue(t1);
						var temp2 = itemT2.GetValue(t2);
						if(temp1 !=null  && temp2 != null)
						//if (itemT1.GetValue(t1) != itemT2.GetValue(t2))
						//if(!itemT1.GetValue(t1,null).Equals(itemT2.GetValue(t2,null)))
						if (itemT1.GetValue(t1).ToString() != itemT2.GetValue(t2).ToString())
						{
							return false;
						}
					}
				}
			}

			return true;
		}


		protected MyModelBase()
		{
			UpdateCommands();
		}

		protected void OnPropertyChanged()
		{

		}


		protected virtual void UpdateCommands()
		{
			this.RaiseCanExecuteChanged(x => x.Save());
			this.RaiseCanExecuteChanged(x => x.Reset());
		}

		[Command(CanExecuteMethodName = "CanSave")]
		protected void Save()
		{

		}

		protected bool CanSave()
		{
			return NeedSave() && !HasValidationErrors();
		}

		[Command(CanExecuteMethodName = "CanReset")]
		protected void Reset()
		{
			this.Entity = this.TempEntity;
		}


		protected bool CanReset()
		{
			return this.TempEntity != null && NeedSave();
		}

		bool NeedSave()
		{
			if (this.Entity == null)
				return false;
			//return true;
			EntityState entitySatate = GetState();
			return true;
			return entitySatate == EntityState.Added || entitySatate == EntityState.Modified;
		}

		protected virtual bool HasValidationErrors()
		{
			IDataErrorInfo dataErrorInfo = Entity as IDataErrorInfo;
			return dataErrorInfo != null && IDataErrorInfoHelper.HasErrors(dataErrorInfo);
		}





	}
}