using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;
using NotionBack.Services.ConverterService;
using static System.Net.Mime.MediaTypeNames;

namespace NotionBack.Middleware.PageTypes
{
    public class PageTypeCreatoreMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext, IUnitOfWork _unitOfWork, IConvertService<PageTypeDTO, TypePage> _convertService)
        {
            foreach (var type in Enum.GetValues(typeof(PageType)))
            {
                try
                {
                    var isType = await _unitOfWork.PageTypes.GetTypePageByCode((int)type);
                }
                catch (Exception)
                {
                    var newType = new PageTypeDTO()
                    {
                        Name = type.ToString(),
                        TypeCode = (int)type
                    };
                    await _unitOfWork.PageTypes.Create(await _convertService.FromDTO(newType));
                }
            }
            await _unitOfWork.Save();

            await _next(httpContext);
            return;
        }
    }
}
