using Framework.Application.Controllers;
using Framework.Application.ModelBinder;
using Framework.Application.Models;
using Framework.ServiceContract.Request;
using Microsoft.AspNetCore.Mvc;
using Stock.Core;
using Stock.Dto.Common;
using Stock.ServiceContract.Common;
using StockApi.Models.Location;

namespace StockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ApiBaseController
    {
        private readonly ILocationService _locationService;

        public LocationController(IConfiguration configuration, 
            IHostEnvironment hostEnvironment,
            ILocationService _locationService) : base(configuration, hostEnvironment)
        {
            this._locationService = _locationService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var response = await this._locationService.PagedSearchAsync(new PagedSearchRequest
            {
                PageIndex = pageIndex - 1,
                PageSize = pageSize,
                OrderByFieldName = "Id",
                SortOrder = CoreConstant.SortOrder.Ascending,
                Keyword = string.Empty,
                Filters = string.Empty
            });

            var rowJsonData = new List<object>();
            foreach (var dto in response.DtoCollection)
            {
                rowJsonData.Add(this.PopulateLocationResponse(dto));
            }

            return GetPagedSearchGridJson(pageIndex, pageSize, rowJsonData, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var readResponse = await this._locationService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            return Ok(this.GetSuccessJson(readResponse, new
            {
                readResponse.Data
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var dto = new LocationDto
            {
                Name = model.Name,
            };

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._locationService.InsertAsync(new GenericRequest<LocationDto> { Data = dto });

            if (createResponse.IsError())
            {
                return this.GetErrorJson(createResponse);
            }

            return this.GetSuccessJson(createResponse, createResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] EditModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var readResponse = await this._locationService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var dto = readResponse.Data;
            dto.Name = model.Name;

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._locationService.UpdateAsync(new GenericRequest<LocationDto> { Data = dto });

            if (createResponse.IsError())
            {
                return this.GetErrorJson(createResponse);
            }

            return this.GetSuccessJson(createResponse, createResponse.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var readResponse = await this._locationService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var deleteResponse = await this._locationService.DeleteAsync(new GenericRequest<int> { Data = id });

            if (deleteResponse.IsError())
            {
                return this.GetErrorJson(deleteResponse);
            }

            return this.GetSuccessJson(deleteResponse, deleteResponse.Data);
        }

        private object PopulateLocationResponse(LocationDto locationDto)
        {
            return new
            {
                locationDto.Id,
                locationDto.Name
            };
        }
    }
}
