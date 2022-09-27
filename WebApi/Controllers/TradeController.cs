using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Repository;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	/// <summary>
	/// Контроллер для получения сделок.
	/// </summary>
	[Authorize]
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class TradeController : Controller
	{
		/// <summary>
		/// Реализует запросы к бд через Linq2db.
		/// </summary>
		private Linq2dbDataProvider _context { get; }

		/// <summary>
		/// Конструктор с параметрами.
		/// </summary>
		/// <param name="context">Экземпляр класса для запросов к бд через Linq2db.</param>
		public TradeController(Linq2dbDataProvider context)
		{
			_context = context;
		}

		/// <summary>
		/// Получить последнюю сделку пользователя.
		/// </summary>
		/// <returns>Сделка.</returns>
		[EnableCors]
		[HttpGet]
		public JsonResult GetLastTrade()
		{
			return Json(_context.GetLastTrade(User.Identity.Name));
		}
	}
}
