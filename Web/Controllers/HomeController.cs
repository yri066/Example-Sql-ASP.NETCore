using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Web.Models;
using WebApi.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Web.Controllers
{
	/// <summary>
	/// Реализует получение данных последней сделки пользователя
	/// </summary>
	public class HomeController : Controller
	{
		/// <summary>
		/// Конфигурация.
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Конструктор с параметрами.
		/// </summary>
		/// <param name="configuration">Конфигурация.</param>
		public HomeController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// Стартовая страница выбора метода получения данных.
		/// </summary>
		/// <returns>Стартовая страница.</returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Страница запроса через c#.
		/// </summary>
		/// <returns>Страница со сделкой.</returns>
		public async Task<IActionResult> TradeChsarp()
		{
			var trade = new WebApi.Model.Trade();

			try
			{
				trade = await GetTradeFromUrl(_configuration.GetValue<string>("TradeUrl"));
			}
			catch (Exception)
			{
				trade = null;
			}

			return View(trade);
		}

		/// <summary>
		/// Страница запроса через JavaScript.
		/// </summary>
		/// <returns>Страница со сделкой.</returns>
		public IActionResult TradeJavaScript()
		{
			return View();
		}

		/// <summary>
		/// Страница вывода ошибок.
		/// </summary>
		/// <returns> Возвращает страницу Error. </returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		/// <summary>
		/// Получение трейда по адресу.
		/// </summary>
		/// <param name="url">URL адрес.</param>
		/// <returns>Трейд.</returns>
		private static async Task<Trade> GetTradeFromUrl(string url)
		{
			if(string.IsNullOrEmpty(url))
			{
				throw new ArgumentNullException(nameof(url));
			}

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
