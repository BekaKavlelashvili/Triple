using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Threading.Tasks;
using Triple.Application.Shared;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;
using MediatR;

namespace Triple.API.Controllers
{
    public class BaseController : Controller
    {
        private IMediator _mediator;

        public BaseController()
        {
        }

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected (int page, int pageSize) GetPaging()
        {
            this.HttpContext.Request.Query.TryGetValue("page", out StringValues pageIndex);
            this.HttpContext.Request.Query.TryGetValue("pageSize", out StringValues pageSize);

            return (int.Parse(pageIndex.ToString()), int.Parse(pageSize.ToString()));
        }
        protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        {
            return await _mediator.Send(query);
        }

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null) return NotFound();
            return Ok(data);
        }

        protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
        {
            return await _mediator.Send(command);
        }

        protected void FillPaging(SortAndPagingQuery query)
        {
            var paging = GetPaging();
            query.Page = paging.page;
            query.PageSize = paging.pageSize;
        }

        protected IActionResult ToHttpResponse(Result result)
        {
            return result.Status switch
            {
                ResultStatus.Succeeded => Ok(result),
                ResultStatus.Failed => BadRequest(result),
                ResultStatus.NotFound => NotFound(result),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected IActionResult ToHttpResponse<T>(Result<T> result)
        {
            return result.Status switch
            {
                ResultStatus.Succeeded => Ok(result.Object),
                ResultStatus.Failed => BadRequest(result),
                ResultStatus.NotFound => NotFound(result),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected IActionResult ExcelResponse(XSSFWorkbook wb, string fileName)
        {
            using (var stream = new MemoryStream())
            {
                stream.Position = 0;
                wb.Write(stream);
                var excelContentType = "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.Clear();
                Response.ContentType = excelContentType;
                wb.Close();

                return File(stream.ToArray(), excelContentType, $"{fileName}.xlsx");
            }
        }

        protected string GetUserIdentityId()
        {
            return GetUserIdentity()?.UserId;
        }

        [NonAction]
        public UserIdentity GetUserIdentity()
        {
            if (User == null/* || !User.Identity.IsAuthenticated*/)
                return UserIdentity.Empty();

            return UserIdentity.From(User);
        }
    }
}