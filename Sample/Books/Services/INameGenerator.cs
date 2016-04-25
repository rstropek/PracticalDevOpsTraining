using System.Threading.Tasks;

namespace Books.Services
{
    /// <summary>
    /// Defines the interface for name generators.
    /// </summary>
    /// <remarks>
    /// Is an interface to enable mocking in unit tests.
    /// </remarks>
    public interface INameGenerator
    {
        Task<string> GenerateRandomBookTitleAsync();
    }
}
