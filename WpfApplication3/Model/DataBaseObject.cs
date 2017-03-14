using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WpfApplication3.Common;

namespace WpfApplication3.Model
{
	public abstract class DatabaseObject : IDataErrorInfo
	{
		[Key]
		public long Id
		{
			get;
			set;
		}

		string IDataErrorInfo.Error
		{
			get
			{
				return null;
			}
		}

		string IDataErrorInfo.this[string columnName]
		{
			get
			{
				return IDataErrorInfoHelper.GetErrorText(this, columnName);
			}
		}
	}
}
