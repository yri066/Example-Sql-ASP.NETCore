using Newtonsoft.Json;
using WebApi.Model;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ShowLastTrade();
			Console.ReadLine();
		}

		/// <summary>
		/// Вывод сделки в консоль.
		/// </summary>
		private static async Task ShowLastTrade()
		{
			var trade = new Trade();
			try
			{
				trade = await GetTradeFromUrl(ConfigurationManager.ConnectionStrings["LastTradeUrl"].ToString());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				trade = null;
			}

			if (trade != null)
			{
				Console.WriteLine($"Сумма трейда {trade.Amount}\nДата трейда: {trade.Created.ToLocalTime()}");
			}
			else
			{
				Console.WriteLine("Не удалось получить последний трейд.");
			}
		}

		/// <summary>
		/// Получение трейда по адресу.
		/// </summary>
		/// <param name="url">URL адрес.</param>
		/// <returns>Сделка.</returns>
		private static async Task<Trade> GetTradeFromUrl(string url)
		{
			var request = WebRequest.Create(url);
			request.UseDefaultCredentials = true;
			var response = await request.GetResponseAsync();
			using (var stream = response.GetResponseStream())
			{
				using (var reader = new StreamReader(stream))
				{
					try
					{
						return JsonConvert.DeserializeObject<Trade>(reader.ReadToEnd());
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						return null;
					}
				}
			}
		}
	}
}
