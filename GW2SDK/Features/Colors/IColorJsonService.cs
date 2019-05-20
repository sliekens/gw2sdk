using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Features.Colors
{
    public interface IColorJsonService
    {
        Task<HttpResponseMessage> GetColorIds();

        Task<HttpResponseMessage> GetColorById(int colorId);

        Task<HttpResponseMessage> GetColorsById(IReadOnlyList<int> colorIds);

        Task<HttpResponseMessage> GetAllColors();

        Task<HttpResponseMessage> GetColorsPage(int page, int? pageSize);
    }
}