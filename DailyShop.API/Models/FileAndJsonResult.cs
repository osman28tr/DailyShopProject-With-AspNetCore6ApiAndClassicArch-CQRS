using DailyShop.Business.Features.Products.Dtos;

namespace DailyShop.API.Models
{
    public class FileAndJsonResult
    {
        public Stream? FileStream { get; set; }
        public List<GetListProductDto>? GetListProductDto { get; set; }
    }
}
