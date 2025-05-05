using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.REST;

namespace NotionBack.Controllers
{
    [Route("imgriff/meta")]
    [ApiController]
    public class MetaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = "/imgriff/meta",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var metaInfo = new Dictionary<String, List<RestMetaData>>();
            metaInfo.Add("EmptyController", new List<RestMetaData>() {
             new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/emptypage?id={new Guid()}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },

                new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/emptypage",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/emptypage",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/emptypage",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }});
            metaInfo.Add("TableController", new List<RestMetaData>()
            {
                new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/tables?id={new Guid()}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/tables",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/tables",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/tables",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }
            });
            metaInfo.Add("BoardController", new List<RestMetaData>()
            {
                new RestMetaData()
            {
                method = "GET",
                name = "GetAll",
                uri = $"/imgriff/boards?id={new Guid()}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/boards",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/boards",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/boards",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }
            });
            metaInfo.Add("CalendarController", new List<RestMetaData>()
            {
                new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/calendars?id={new Guid()}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/calendars",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }, new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/calendars",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/calendars",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }
            });
            metaInfo.Add("EmptyControllre", new List<RestMetaData>()
            {
                new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/emptypage?id={new Guid()}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/emptypage",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/emptypage",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/emptypage",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }
            });
            metaInfo.Add("GalleryController", new List<RestMetaData>()
                {
                new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/galleries?id={new Guid()}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/galleries",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/galleries",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/galleries",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }
            });
            metaInfo.Add("ListController", new List<RestMetaData>()
            {
                new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/lists?id={new Guid()}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/lists",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/lists",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/lists",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }
            });
            metaInfo.Add("PageController", new List<RestMetaData>()
            {
                new RestMetaData()
            {
                method = "GET",
                name = "GetAll",
                uri = $"/imgriff/pages?slug=page_slug",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/pages/get-all",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/pages",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/pages",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/pages?slug=page_slug",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "DELETE",
                name = "DeletePermanently",
                uri = "/imgriff/page/delete-permanently?slug=page_slug",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }
            });
            metaInfo.Add("UserController", new List<RestMetaData>()
            {
                new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/person?id={new Guid()}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/person",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/person",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/person",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }
            });
            metaInfo.Add("AuthController", new List<RestMetaData>()
            {
                new RestMetaData()
            {
               method = "GET",
                name = "GetOtp",
                uri = $"/imgriff/auth/get-otp?email=user_email",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },new RestMetaData()
            {
               method = "GET",
                name = "Login",
                uri = $"/imgriff/auth/login",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },new RestMetaData()
            {
                method = "GET",
                name = "GoogleResponse",
                uri = "/imgriff/auth/google-response",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "GET",
                name = "GetByEmail",
                uri = $"/imgriff/auth/user-by-email?email=user_email",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/auth",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            },
                 new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/auth",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            }
            });

            var _response = new RestResponse<object>(200, metaInfo, meta);
            return Ok(_response);
        }
    }
}
