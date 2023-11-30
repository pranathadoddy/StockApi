using Framework.Application.Controllers;
using Framework.Application.ModelBinder;
using Framework.Application.Models;
using Framework.ServiceContract.Request;
using Microsoft.AspNetCore.Mvc;
using Stock.Core;
using Stock.Dto.Common;
using Stock.ServiceContract.Common;
using StockApi.Models.Supplier;

namespace StockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ApiBaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierItemService _supplierItemService;
        private readonly IItemService _itemService;

        public SupplierController(IConfiguration configuration, 
            IHostEnvironment hostEnvironment,
            ISupplierService supplierService,
            ISupplierItemService supplierItemService,
            IItemService itemService) : base(configuration, hostEnvironment)
        {
            this._supplierService = supplierService;
            _supplierItemService = supplierItemService;
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var response = await this._supplierService.PagedSearchAsync(new PagedSearchRequest
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
                rowJsonData.Add(this.PopulateSupplierResponse(dto));
            }

            return GetPagedSearchGridJson(pageIndex, pageSize, rowJsonData, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var readResponse = await this._supplierService.ReadAsync(new GenericRequest<int> { Data = id });
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

            var dto = new SupplierDto
            {
               Name = model.Name,
            };

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._supplierService.InsertAsync(new GenericRequest<SupplierDto> { Data = dto });

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

            var readResponse = await this._supplierService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var dto = readResponse.Data;
            dto.Name = model.Name;

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._supplierService.UpdateAsync(new GenericRequest<SupplierDto> { Data = dto });

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

            var readResponse = await this._supplierService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var deleteResponse = await this._supplierService.DeleteAsync(new GenericRequest<int> { Data = id });

            if (deleteResponse.IsError())
            {
                return this.GetErrorJson(deleteResponse);
            }

            return this.GetSuccessJson(deleteResponse, deleteResponse.Data);
        }


        [HttpGet("{id}/item")]
        public async Task<ActionResult> GetAllSupplierItem([FromRoute] int id,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var filters =$"IdSupplier = {id}";
            var response = await this._supplierItemService.PagedSearchAsync(new PagedSearchRequest
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
                rowJsonData.Add(this.PopulateSupplierItemResponse(dto));
            }

            return GetPagedSearchGridJson(pageIndex, pageSize, rowJsonData, response);
        }

        [HttpPost("{id}/item")]
        public async Task<IActionResult> CreateSupplierItem([FromRoute] int id, [FromBody] CreateSupplierItemModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }


            var readResponse = await this._supplierService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var readItemResponse = await this._itemService.ReadAsync(new GenericRequest<int> { Data = model.Id });
            if (readItemResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var filters = $" IdItem = {model.Id} and IdSupplier = {id}";
            var response = await this._supplierItemService.PagedSearchAsync(new PagedSearchRequest
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

            var dto = new SupplierItemDto
            {
                IdSupplier = id,
                IdItem = model.Id,
                Price = model.Price,
            };

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._supplierItemService.InsertAsync(new GenericRequest<SupplierItemDto> { Data = dto });

            if (createResponse.IsError())
            {
                return this.GetErrorJson(createResponse);
            }

            return this.GetSuccessJson(createResponse, createResponse.Data);
        }

        [HttpPatch("{id}/item")]
        public async Task<IActionResult> EditSupplierItem([FromRoute] int id, [FromBody] EditSupplierItemModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }


            var readResponse = await this._supplierService.ReadAsync(new GenericRequest<int> { Data = id });
            if (readResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var readItemResponse = await this._itemService.ReadAsync(new GenericRequest<int> { Data = model.Id });
            if (readItemResponse.IsError())
            {
                return this.BadRequest(this.GetErrorJson(readResponse));
            }

            var filters = $" IdItem = {model.Id} and IdSupplier = {id}";
            var response = await this._supplierItemService.PagedSearchAsync(new PagedSearchRequest
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

            dto.Price = model.Price;

            this.PopulateAuditFieldsOnCreate(dto, "user");

            var createResponse = await this._supplierItemService.UpdateAsync(new GenericRequest<SupplierItemDto> { Data = dto });

            if (createResponse.IsError())
            {
                return this.GetErrorJson(createResponse);
            }

            return this.GetSuccessJson(createResponse, createResponse.Data);
        }

        private object PopulateSupplierResponse(SupplierDto supplierDto)
        {
            return new
            {
                supplierDto.Id,
                supplierDto.Name
            };
        }

        private object PopulateSupplierItemResponse(SupplierItemDto supplierItemDto)
        {
            return new
            {
                supplierItemDto.SupplierName,
                supplierItemDto.ItemName,
                supplierItemDto.Price
            };
        }

    }
}
