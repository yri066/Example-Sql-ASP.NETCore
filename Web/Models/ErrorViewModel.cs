using System;

namespace Web.Models
{
	/// <summary>
	/// Ошибка.
	/// </summary>
	public class ErrorViewModel
	{
		/// <summary>
		/// Код запроса.
		/// </summary>
		public string RequestId { get; set; }

		/// <summary>
		/// Показать код запроса
		/// </summary>
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
