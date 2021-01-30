using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectDiffFinder
{
	/// <summary>
	/// From https://janaks.com.np/compare-two-objects-in-csharp/
	/// https://gist.github.com/janaks09/56c3f6d01a8f3791ee4bd0f2c3f2af50#file-objectcomparer-cs
	/// </summary>
	public static class ObjectComparer
    {
		public static string GetChangedValues(object oldObject, object newObject)
		{
			var oType = oldObject.GetType();

			var sb = new StringBuilder();
			foreach (var oProperty in oType.GetProperties())
			{
				if (oProperty.Name.ToLower().Equals("lastupdated"))
					continue;

				var oOldValue = oProperty.GetValue(oldObject, null);
				var oNewValue = oProperty.GetValue(newObject, null);
				// this will handle the scenario where either value is null

				if (Equals(oOldValue, oNewValue)) continue;
				// Handle the display values when the underlying value is null

				var sOldValue = oOldValue == null ? "null" : oOldValue.ToString();
				var sNewValue = oNewValue == null ? "null" : oNewValue.ToString();
				sb.Append($"{oProperty.Name}: {sOldValue} –> {sNewValue}");
				sb.AppendLine();
			}

			return sb.ToString();
		}
	}
}
