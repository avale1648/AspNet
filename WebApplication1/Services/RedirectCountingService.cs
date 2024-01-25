//Данный класс основан на работе моего одногруппника, Смахтина Алексея, ПВ113:
//https://github.com/AlexSmakhtin/WebAppAspNetCore/blob/master/MySuperShop.ApiGateway/Middleware/TrafficCounterMiddleware.cs
using System.Collections.Concurrent;
namespace WebApplication1.Services
{
	public class RedirectCountingService
	{
		//Потокобезопасный словарь для хранения пути и количества переходов на страницу:
		private readonly ConcurrentDictionary<string, int> _pathCountPairs = new ConcurrentDictionary<string, int>();
		//метод для добавления/изменения элемента в словаре:
		public void AddOrUpdate(PathString path)
		{
			//если path имеет значение null, выдать исключение:
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}
			//добавление/изменение элемента в словаре:
			// - добавление: путь, количество переходов - 1;
			// - измененение: количество переходов - количество переходов + 1.
			_pathCountPairs.AddOrUpdate(path.ToString(), 1, (path, count) => count + 1);
		}
		//метод для получения словаря:
		public Dictionary<string, int> GetPathCountPairs()
		{
			return _pathCountPairs.ToDictionary(kvp => kvp.Key, kvp => kvp.Value, _pathCountPairs.Comparer);
		}
	}
}