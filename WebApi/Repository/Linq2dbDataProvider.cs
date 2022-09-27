using Microsoft.Extensions.Configuration;
using LinqToDB;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApi.Repository.Entity;
using WebApi.Model;
using System;

namespace WebApi.Repository
{
	/// <summary>
	/// Реализует запросы через Linq2db.
	/// </summary>
	public class Linq2dbDataProvider
	{
		/// <summary>
		/// Значение конфигурации.
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Конструктор с параметрами.
		/// </summary>
		/// <param name="configuration">Конфигурация.</param>
		public Linq2dbDataProvider(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// Получить последнюю сделку пользователя.
		/// </summary>
		/// <param name="name">Имя пользователя.</param>
		/// <returns>Сделка.</returns>
		public Trade GetLastTrade(string name)
		{
			if(string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			using (var context = new DbConnection(_configuration))
			{
				var trades = new List<Data>();
				try
				{
					trades = (from user in context.User
								  join data in context.Data
									  on user.Id equals data.UserId
								  where user.UserDomainName == name
								  select data)
							.Take(1000)
							.ToList();
				}
				catch
				{
					return null;
				}
				

				var result = new List<Trade>();

				foreach(var trade in trades)
				{
					result.Add(JsonConvert.DeserializeObject<Trade>(trade.Entity));
				}

				return result.Where(result => result.Created <= DateTime.UtcNow)
								.OrderByDescending(data => data.Created).FirstOrDefault();
			}
		}
	}
}
