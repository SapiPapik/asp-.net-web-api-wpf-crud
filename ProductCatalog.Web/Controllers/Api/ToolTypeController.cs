using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using ProductCatalog.BLL.Contracts.Contracts;
using ProductCatalog.BLL.Contracts.Dtos;
using ProductCatalog.Web.Filters;
using ProductCatalog.Web.Models;

namespace ProductCatalog.Web.Controllers.Api {
    [RoutePrefix("api")]
    [ApiExceptionFilter]
    public class ToolTypeController : ApiController {
        private readonly IToolTypeService _toolTypeService;

        public ToolTypeController(IToolTypeService toolTypeService) {
            _toolTypeService = toolTypeService ?? throw new ArgumentNullException(nameof(toolTypeService));
        }

        [Route("toolTypes")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetToolTypes(int limit = 20, int offset = 0) {
            var toolTypes = await _toolTypeService.GetAllAsync(limit, offset);
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ICollection<ToolTypeResponseModel>>(toolTypes));
        }

        [Route("toolTypes/{toolTypeId}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetToolType(Guid toolTypeId) {
            var toolType = await _toolTypeService.GetByIdAsync(toolTypeId);
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ToolTypeResponseModel>(toolType));
        }

        [Route("toolTypes")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddToolType([FromBody] ToolTypeResponseModel toolType) {
            await _toolTypeService.AddAsync(Mapper.Map<ToolTypeDto>(toolType));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("toolTypes/{toolTypeId}")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateToolType(Guid toolTypeId, [FromBody] ToolTypeResponseModel toolType) {
            await _toolTypeService.UpdateAsync(toolTypeId, Mapper.Map<ToolTypeDto>(toolType));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("toolTypes/{toolTypeId}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteToolType(Guid toolTypeId) {
            await _toolTypeService.RemoveAsync(toolTypeId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}