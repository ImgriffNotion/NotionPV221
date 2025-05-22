using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.DAL.Repositories;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NotionBack.Services.ConverterService.TypeList
{
    public class ListConverter(IConvertService<ListContentDTO, ListContent> content_converter,
        IUnitOfWork unitOfWork) : IConvertService<ListDTO, DAL.Models.pageContents.List>
    {
        private readonly IConvertService<ListContentDTO, ListContent> _convertService = content_converter;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<DAL.Models.pageContents.List> FromDTO(ListDTO model)
        {
            if (model == null)
                return new List();

            var newList = new DAL.Models.pageContents.List()
            {
                Title = model.Title,
                ParentPageId = null,
                BoardId = null,
                Contents = new List<ListContent>()
            };

            if (model.InternalContent != null && model.InternalContent.Count != 0)
            {
                int index = 0;
                foreach (var content in model.InternalContent)
                {
                    content.Index = index++;
                    newList.Contents.Add(await _convertService.FromDTO(content));
                }
            }

            return newList;
        }

        public async Task<DAL.Models.pageContents.List> FromDTO(List domain, ListDTO dto)
        {
            if (domain == null || dto == null)
                return new List();

            domain.Title = dto.Title;

            if (dto.InternalContent != null && dto.InternalContent.Count != 0)
            {
                var tmpBuffer = new List<ListContent>();
                int index = 0;
                foreach (var dtoContent in dto.InternalContent)
                {
                    dtoContent.Index = index++;
                    if (dtoContent.Id != null)
                    {
                        var domainContent = domain.Contents.Where(obj => obj.Id == dtoContent.Id).FirstOrDefault();
                        if (domainContent != null)
                        {
                            await _convertService.FromDTO(domainContent, dtoContent);
                        }
                        else
                        {
                            try
                            {
                                domainContent = await _unitOfWork.ListContents.Get((Guid)dtoContent.Id);
                                domainContent.ListId = dto.Id;
                                await _convertService.FromDTO(domainContent, dtoContent);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                    else
                    {
                        tmpBuffer.Add(await _convertService.FromDTO(dtoContent));
                    }
                }

                foreach (var content in tmpBuffer)
                {
                    domain.Contents.Add(content);
                }
            }


            return domain;
        }

        public async Task<ListDTO> ToDTO(DAL.Models.pageContents.List model)
        {
            if (model == null)
                return new ListDTO();

            var newList = new ListDTO()
            {
                Id = model.Id,
                ParentPageId = model.ParentPageId,
                Title = model.Title,
                CreatedAt = model.CreatedAt,
                DeleteDt = model.DeleteDt,
                BoardId = model.BoardId,
                InternalContent = new List<ListContentDTO>()
            };

            if (model.Contents != null && model.Contents.Count != 0)
            {
                foreach (var content in model.Contents)
                {
                    newList.InternalContent.Add(await _convertService.ToDTO(content));
                }
            }

            newList.InternalContent = newList.InternalContent.OrderBy(c => c.Index).ToList();

            return newList;
        }
    }
}
