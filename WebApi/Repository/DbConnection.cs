using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Configuration;
using WebApi.Repository.Entity;

namespace WebApi.Repository
{
	/// <summary>
	/// Контекст подключения к базе для Linq2db.
	/// </summary>
	public class DbConnection : DataConnection
	{
		/// <summary>
		/// Конструктор контекста подключения.
		/// </summary>
		/// <param name="configuration">Конфигурация.</param>
		public DbConnection(IConfiguration configuration) : base(ProviderName.SqlServer, configuration.GetConnectionString("DefaultConnection"))
		{ }

		/// <summary>
		/// Данные для пользователя.
		/// </summary>
		public ITable<Data> Data => this.GetTable<Data>();

		/// <summary>
		/// Пользователь.
		/// </summary>
		public ITable<User> User => this.GetTable<User>();
	}
}
