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

namespace NotionBack.Controllers
{

    [ApiController]
    [Route("imgriff/pages")]
    public class PageController(IUnitOfWork unitOfWork,
        IConvertService<PageDTO, Page> pageConvertService,
        IConvertService<PageTypeDTO, TypePage> pagetypeConvertService,
        IPageTypeService pageTypeService,
        ISlugerService slugerService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<PageDTO, Page> _pageConvertService = pageConvertService;
        private readonly IConvertService<PageTypeDTO, TypePage> _pagetypeConvertService = pagetypeConvertService;
        private readonly IPageTypeService _pageTypeService = pageTypeService;
        private readonly ISlugerService _slugerService = slugerService;

        [HttpGet]
        public async Task<IActionResult> Get(String slug)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetAll",
                uri = $"/imgriff/pages?slug={slug}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var page = await _unitOfWork.Pages.GetPageBySlug(slug);
                page.Type = await _unitOfWork.PageTypes.Get((Guid)page.TypeId);
                await GetContent(page);

                var _response = new RestResponse<Object>(200, _pageConvertService.ToDTO(page), meta);
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


            try
            {
                var listOfPages = (await this._unitOfWork.Pages.GetAll()).ToList();
                var pages = new List<PageDTO>();
                foreach (var page in listOfPages)
                {
                    page.Type = await _unitOfWork.PageTypes.Get((Guid)page.TypeId);
                    pages.Add(_pageConvertService.ToDTO(page));
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


            try
            {
                var pageType = await _unitOfWork.PageTypes.GetTypePageByCode(_pageTypeService.GetCodeOfPageType(page.Type));
                var newPage = _pageConvertService.FromDTO(page);
                newPage.Type = pageType;
                newPage.Slug = await _slugerService.GenerateUniqueSlug(newPage.Title);
                await _unitOfWork.Pages.Create(newPage);

                await _unitOfWork.Save();

                var updatedPage = await _unitOfWork.Pages.GetPageBySlug(newPage.Slug);

                var _response = new RestResponse<Object>(200, _pageConvertService.ToDTO(updatedPage), meta);
                return Ok(_response);
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

            return Ok();
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

                var _response = new RestResponse<Object>(200, _pageConvertService.ToDTO(pages), meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(200, ex.Message, meta);
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
                var _response = new RestResponse<Object>(200, ex.Message, meta);
                return Ok(_response);
            } 
        }


        /// There's enormous kostyl | YOU MUST MAKE IT CORRECT !!!!

        private async Task<Page> GetContent(Page page)
        {
            switch ((PageType)page.Type.TypeCode)
            {
                case PageType.Empty:
                    {
                        await _unitOfWork.JustPageContents.GetAll();
                        break;
                    }
                case PageType.Board:
                    {
                        var tmp = await _unitOfWork.Boards.GetAll();
                        break;
                    }
                case PageType.List:
                    {
                        await _unitOfWork.Lists.GetAll();
                        break;
                    }
                case PageType.Calendar:
                    {
                        await _unitOfWork.Calendars.GetAll();
                        break;
                    }
                case PageType.Gallery:
                    {
                        await _unitOfWork.Galleries.GetAll();
                        break;
                    }
                case PageType.Table:
                    {
                        await _unitOfWork.Tables.GetAll();
                        break;
                    }
            };
            return page;
        }
    }
}
