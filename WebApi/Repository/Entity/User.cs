using System;

namespace WebApi.Repository.Entity
{
	/// <summary>
	/// Пользователь.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Идентификатор записи.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public string UserDomainName { get; set; }
	}
}
