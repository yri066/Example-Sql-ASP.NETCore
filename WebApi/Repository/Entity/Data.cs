using System;

namespace WebApi.Repository.Entity
{
	/// <summary>
	/// Данные для пользователя.
	/// </summary>
	public class Data
	{
		/// <summary>
		/// Идентификатор записи.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// JSON Сделка.
		/// </summary>
		public string Entity { get; set; }
	}
}
