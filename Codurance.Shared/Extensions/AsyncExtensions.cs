using System.Threading.Tasks;

namespace Codurance.Shared.Extensions
{
	public static class AsyncExtensions
	{
		public static Task<T> AsTask<T>(this T result)
		{
			return Task.FromResult(result);
		}
	}
}