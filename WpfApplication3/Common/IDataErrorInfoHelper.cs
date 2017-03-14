using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace WpfApplication3.Common
{
	public static class IDataErrorInfoHelper
	{
		public static string GetErrorText(object owner, string propertyName)
		{
			string[] array = propertyName.Split(new char[]
			{
				'.'
			});
			if (array.Length > 1)
			{
				return IDataErrorInfoHelper.GetErrorText(owner, array);
			}
			PropertyInfo property = owner.GetType().GetProperty(propertyName);
			if (property == null)
			{
				return null;
			}
			object propertyValue = property.GetValue(owner, null);
			ValidationContext validationContext = new ValidationContext(owner, null, null)
			{
				MemberName = propertyName
			};
			string[] value = (from x in property.GetCustomAttributes(false).OfType<ValidationAttribute>()
							  select x.GetValidationResult(propertyValue, validationContext) into x
							  where x != null
							  select x.ErrorMessage into x
							  where !string.IsNullOrEmpty(x)
							  select x).ToArray<string>();
			return string.Join(" ", value);
		}

		private static string GetErrorText(object owner, string[] path)
		{
			string columnName = string.Join(".", path.Skip(1));
			string name = path[0];
			PropertyInfo property = owner.GetType().GetProperty(name);
			if (property == null)
			{
				return null;
			}
			object value = property.GetValue(owner, null);
			IDataErrorInfo dataErrorInfo = value as IDataErrorInfo;
			if (dataErrorInfo != null)
			{
				return dataErrorInfo[columnName];
			}
			return string.Empty;
		}
	}
}
