using System;

namespace WebApi.Model
{
	/// <summary>
	/// Сделка.
	/// </summary>
	public class Trade
	{
		/// <summary>
		/// Сумма.
		/// </summary>
		public decimal Amount { get; set; }

		/// <summary>
		/// Создано.
		/// </summary>
		public DateTime Created { get; set; }
	}
}
