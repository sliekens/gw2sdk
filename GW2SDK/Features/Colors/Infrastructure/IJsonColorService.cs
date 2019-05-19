using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Features.Colors.Infrastructure
{
    public interface IJsonColorService
    {
        Task<(string Json, Dictionary<string, string> MetaData)> GetColorIds();

        Task<(string Json, Dictionary<string, string> MetaData)> GetColorById(int colorId);

        Task<(string Json, Dictionary<string, string> MetaData)> GetColorsById(IReadOnlyList<int> colorIds);

        Task<(string Json, Dictionary<string, string> MetaData)> GetAllColors();

        Task<(string Json, Dictionary<string, string> MetaData)> GetColorsPage(int page, int? pageSize);
    }
}