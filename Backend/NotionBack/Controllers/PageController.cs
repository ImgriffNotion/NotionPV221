using Microsoft.AspNetCore.Mvc;
using NotionBack.Models.Enums;
using NotionBack.REST;
using NotionBack.DAL.Interfaces;
using NotionBack.Services.ConverterService;
using NotionBack.DAL.Repositories;
using NotionBack.Models.ModelsDTO;
using NotionBack.DAL.Models;
using NotionBack.Services.PageTypesService;
using System;
using NotionBack.Services.SlugService;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using NotionBack.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using NotionBack.Services.PageContent;

namespace NotionBack.Controllers
{

    [ApiController]
    [Route("imgriff/pages")]
    public class PageController(IUnitOfWork unitOfWork,
        IConvertService<PageDTO, Page> pageConvertService,
        IConvertService<PageTypeDTO, TypePage> pagetypeConvertService,
        IPageTypeService pageTypeService,
        ISlugerService slugerService,
        IPageContentService pageContentService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<PageDTO, Page> _pageConvertService = pageConvertService;
        private readonly IConvertService<PageTypeDTO, TypePage> _pagetypeConvertService = pagetypeConvertService;
        private readonly IPageTypeService _pageTypeService = pageTypeService;
        private readonly ISlugerService _slugerService = slugerService;
        private readonly IPageContentService _pageContentService = pageContentService;

        [HttpGet]
        public async Task<IActionResult> Get(String slug)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/pages?slug={slug}",
                locale = "en-US",
                serverTime = DateTime.UtcNow,
            };

            try
            {
                var page = await _unitOfWork.Pages.GetPageBySlug(slug);
                if (page.TypeId != null)
                    page.Type = await _unitOfWork.PageTypes.Get((Guid)page.TypeId);
                await _pageContentService.GetContent(page);


                var _response = new RestResponse<PageDTO>(200, await _pageConvertService.ToDTO(page), meta);
                return Ok(_response);
            }
            catch (NullReferenceException ex)
            {
                var _response = new RestResponse<Object>(404, ex.Message, meta);
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                var _response = new RestResponse<Object>(404, ex.Message, meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetAll",
                uri = "/imgriff/pages/get-all",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var userId = HttpContext.Items["userId"];
            if (userId == null)
            {
                return userUnauthorized(meta);
            }

            try
            {
                var listOfPages = (await this._unitOfWork.Pages.GetAll((Guid)userId)).ToList();
                var pages = new List<PageDTO>();
                foreach (var page in listOfPages)
                {
                    if (page.TypeId != null)
                    {
                        page.Type = await _unitOfWork.PageTypes.Get((Guid)page.TypeId);
                        pages.Add(await _pageConvertService.ToDTO(page));
                    }
                }

                var _response = new RestResponse<Object>(200, pages, meta);
                return Ok(_response);
            }
            catch (NullReferenceException ex)
            {
                var _response = new RestResponse<Object>(404, ex.Message, meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PageDTO page)
        {
            var meta = new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/pages",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };


            if (!ModelState.IsValid)
            {
                var _response = new RestResponse<Object>(400, ModelState, meta);
                return Ok(_response);
            }

            var userId = HttpContext.Items["userId"];
            if (userId == null)
            {
                return userUnauthorized(meta);
            }

            try
            {
                if (page.Type != null)
                {
                    var pageType = await _unitOfWork.PageTypes.GetTypePageByCode(_pageTypeService.GetCodeOfPageType(page.Type));
                    var newPage = await _pageConvertService.FromDTO(page);
                    newPage.Type = pageType;
                    newPage.OwnerId = (Guid)userId;
                    newPage.Slug = await _slugerService.GenerateUniqueSlug(newPage.Title ?? "");
                    await _unitOfWork.Pages.Create(newPage);

                    await _unitOfWork.Save();

                    var updatedPage = await _unitOfWork.Pages.GetPageBySlug(newPage.Slug);

                    var _response = new RestResponse<Object>(200, await _pageConvertService.ToDTO(updatedPage), meta);
                    return Ok(_response);
                }
                else
                {
                    var _response = new RestResponse<Object>(400, page, meta);
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PageDTO page)
        {
            var meta = new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = $"/imgriff/pages",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };


            if (!ModelState.IsValid)
            {
                var _response = new RestResponse<Object>(400, ModelState, meta);
                return Ok(_response);
            }

            try
            {
                if (page.Slug != null && page.Type != null)
                {
                    var pageForUpdate = await _unitOfWork.Pages.GetPageBySlug(page.Slug);
                    pageForUpdate.Type = await _unitOfWork.PageTypes.GetTypePageByCode(_pageTypeService.GetCodeOfPageType(page.Type));
                    await _pageContentService.GetContent(pageForUpdate);
                    await _pageConvertService.FromDTO(pageForUpdate, page);
                    _unitOfWork.Pages.Update(pageForUpdate);
                    await _unitOfWork.Save();

                    var _response = new RestResponse<Object>(200, await _pageConvertService.ToDTO(pageForUpdate), meta);
                    return Ok(_response);
                }
                else
                {
                    var _response = new RestResponse<Object>(400, page, meta);
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }

        }

        [HttpPut("restore")]
        public async Task<IActionResult> Restore(String slug)
        {
            var meta = new RestMetaData()
            {
                method = "PUT",
                name = "Restore",
                uri = $"/imgriff/pages/restore?slug={slug}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };


            try
            {
                var pages = await _unitOfWork.Pages.GetPageBySlug(slug);
                pages.DeleteDt = null;
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, await _pageConvertService.ToDTO(pages), meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(String slug)
        {
            var meta = new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = $"/imgriff/pages?slug={slug}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };


            try
            {
                var pages = await _unitOfWork.Pages.GetPageBySlug(slug);
                await _unitOfWork.Pages.Delete(pages.Id);
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, await _pageConvertService.ToDTO(pages), meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpDelete("delete-permanently")]
        public async Task<IActionResult> DeletePermanently(String slug)
        {
            var meta = new RestMetaData()
            {
                method = "DELETE",
                name = "DeletePermanently",
                uri = $"/imgriff/pages/delete-permanently?slug={slug}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };


            try
            {
                var pages = await _unitOfWork.Pages.GetPageBySlug(slug);
                await _unitOfWork.Pages.DeletePagePermanently(pages);
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, "Deleted success", meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpDelete("delete-permanently-admin")]
        public async Task<IActionResult> DeletePermanentlyAdmub()
        {
            var meta = new RestMetaData()
            {
                method = "DELETE",
                name = "DeletePermanently",
                uri = $"/imgriff/pages/delete-permanently-admin",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };


            try
            {
                var pages = await _unitOfWork.Pages.GetAll();
                foreach (var page in pages)
                {

                    await _unitOfWork.Pages.DeletePagePermanently(page);

                }
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, "Deleted success", meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
        }

        private IActionResult userUnauthorized(RestMetaData meta)
        {
            var response = new RestResponse<String>(401, "User must be authorized", meta);
            return Ok(response);
        }

    }
}
