using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3.Model
{
	/// <summary>
	/// 实体相对状态
	/// </summary>
	[Flags]
	public enum EntityState
	{

		/// <summary>
		/// 对象存在，分离状态。
		/// </summary>
		Detached = 1,

		/// <summary>
		/// 该对象尚未修改。
		/// </summary>
		Unchanged = 2,

		/// <summary>
		/// 新增对象，但尚未保存。
		/// 保存后状态改变为Unchanged。
		/// </summary>
		Added = 4,

		/// <summary>
		/// 对象已从工作单元中删除。 保存更改后，对象状态更改为“Detached”。
		/// </summary>
		Deleted = 8,

		/// <summary>
		/// 对象上的一个属性已被修改，尚未调用保存。
		/// 保存更改后，对象状态更改为Unchanged。
		/// </summary>
		Modified = 16,
	}
}
