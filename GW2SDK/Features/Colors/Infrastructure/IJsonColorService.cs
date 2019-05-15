using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Features.Colors.Infrastructure
{
    public interface IJsonColorService
    {
        Task<string> GetAllColors();

        Task<string> GetColorIds();

        Task<string> GetColorById(int colorId);

        Task<string> GetColorsById(IReadOnlyList<int> colorIds);

        Task<string> GetColorsPage(int page, int? pageSize);
    }
}