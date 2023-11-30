using Framework.Application.Controllers;
using Framework.ServiceContract.Request;
using Microsoft.AspNetCore.Mvc;
using Stock.Core;
using Stock.Dto.Common;
using Stock.ServiceContract.Common;
using StockApi.Models.Item;

namespace StockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ApiBaseController
    {
        private readonly IItemService _itemService;
        private readonly ILocationService _locationService;
        private readonly IItemLocationService _itemLocationService;

        public ItemController(IConfiguration configuration, 
            IHostEnvironment hostEnvironment,
            IItemService itemService,
            ILocationService locationService,
            IItemLocationService itemLocationService) : base(configuration, hostEnvironment)
        {
            this._itemService = itemService;
            _locationService = locationService;
            _itemLocationService = itemLocationService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var response = await this._itemService.PagedSearchAsync(new PagedSearchRequest
            {
                PageIndex = pageIndex - 1,
                PageSize = pageSize,
                OrderByFieldName = "Name",
                SortOrder = CoreConstant.SortOrder.Ascending,
                Keyword = string.Empty,
                Filters = string.Empty,
            });

            var rowJsonData = new List<object>();
            foreach (var dto in response.DtoCollection)
            {
                rowJsonData.Add(this.PopulateItemResponse(dto));
            }

            return GetPagedSearchGridJson(pageIndex, pageSize, rowJsonData, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var readResponse = await this._itemService.ReadAsync(new GenericRequest<int> { Data = id });
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

            var dto = new ItemDto
            {
                Name = model.Name,
                SellingPrice = model.SellingPrice,
            };

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._itemService.InsertAsync(new GenericRequest<ItemDto> { Data = dto });

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

            var readResponse = await this._itemService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var dto = readResponse.Data;
            dto.Name = model.Name;
            dto.SellingPrice = model.SellingPrice;

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._itemService.UpdateAsync(new GenericRequest<ItemDto> { Data = dto });

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

            var readResponse = await this._itemService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var deleteResponse = await this._itemService.DeleteAsync(new GenericRequest<int> { Data = id });

            if (deleteResponse.IsError())
            {
                return this.GetErrorJson(deleteResponse);
            }

            return this.GetSuccessJson(deleteResponse, deleteResponse.Data);
        }

        [HttpGet("{id}/location")]
        public async Task<ActionResult> GetAllItemLocation([FromRoute] int id,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var filters = $"IdItem = {id}";
            var response = await this._itemLocationService.PagedSearchAsync(new PagedSearchRequest
            {
                PageIndex = pageIndex - 1,
                PageSize = pageSize,
                OrderByFieldName = "Id",
                SortOrder = CoreConstant.SortOrder.Ascending,
                Keyword = string.Empty,
                Filters = filters
            });

            var rowJsonData = new List<object>();
            foreach (var dto in response.DtoCollection)
            {
                rowJsonData.Add(this.PopulateItemLocationResponse(dto));
            }

            return GetPagedSearchGridJson(pageIndex, pageSize, rowJsonData, response);
        }

        [HttpPost("{id}/location")]
        public async Task<IActionResult> CreateSupplierItem([FromRoute] int id, [FromBody] CreateItemLocation model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }


            var readResponse = await this._itemService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var readLocationResponse = await this._locationService.ReadAsync(new GenericRequest<int> { Data = model.Id });
            if (readLocationResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var filters = $" IdItem = {id} and IdLocation = {model.Id}";
            var response = await this._itemLocationService.PagedSearchAsync(new PagedSearchRequest
            {
                PageIndex = 0,
                PageSize = 1,
                OrderByFieldName = "Id",
                SortOrder = CoreConstant.SortOrder.Ascending,
                Keyword = string.Empty,
                Filters = filters
            });

            if (response.DtoCollection.Count > 0)
            {
                return this.BadRequest(this.GetErrorJson("Data Sudah Ada"));
            }

            var dto = new ItemLocationDto
            {
                IdItem = id,
                IdLocation = model.Id,
                Stock = model.Stock,
            };

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._itemLocationService.InsertAsync(new GenericRequest<ItemLocationDto> { Data = dto });

            if (createResponse.IsError())
            {
                return this.GetErrorJson(createResponse);
            }

            return this.GetSuccessJson(createResponse, createResponse.Data);
        }

        [HttpPatch("{id}/location")]
        public async Task<IActionResult> EditSupplierItem([FromRoute] int id, [FromBody] EditItemLocation model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }


            var readResponse = await this._itemService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var readLocationResponse = await this._locationService.ReadAsync(new GenericRequest<int> { Data = model.Id });
            if (readLocationResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var filters = $" IdItem = {id} and IdLocation = {model.Id}";
            var response = await this._itemLocationService.PagedSearchAsync(new PagedSearchRequest
            {
                PageIndex = 0,
                PageSize = 1,
                OrderByFieldName = "Id",
                SortOrder = CoreConstant.SortOrder.Ascending,
                Keyword = string.Empty,
                Filters = filters
            });

            if (response.DtoCollection.Count == 0)
            {
                return this.BadRequest(this.GetErrorJson("Item Not Found"));
            }

            var dto = response.DtoCollection.First();

            dto.Stock = model.Stock;

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._itemLocationService.UpdateAsync(new GenericRequest<ItemLocationDto> { Data = dto });

            if (createResponse.IsError())
            {
                return this.GetErrorJson(createResponse);
            }

            return this.GetSuccessJson(createResponse, createResponse.Data);
        }

        private object PopulateItemResponse(ItemDto itemDto)
        {
            return new
            {
                itemDto.Id,
                itemDto.Name,
                itemDto.SellingPrice
            };
        }

        private object PopulateItemLocationResponse(ItemLocationDto itemLocationDto)
        {
            return new
            {
                itemLocationDto.Id,
                itemLocationDto.LocationName,
                itemLocationDto.ItemName,
                itemLocationDto.Stock
            };
        }
    }
}
