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
    public class EquipmentController : ApiController {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService) {
            _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
        }

        [Route("equipments")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetEquipments(int limit = 20, int offset = 0) {
            var equipment = await _equipmentService.GetAllAsync(limit, offset);
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ICollection<EquipmentResponseModel>>(equipment));
        }

        [Route("equipments/{equipmentId}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetEquipment(Guid equipmentId) {
            var equipment = await _equipmentService.GetByIdAsync(equipmentId);
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<EquipmentResponseModel>(equipment));
        }

        [Route("equipments")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddEquipment(EquipmentResponseModel equipment) {
            await _equipmentService.AddAsync(Mapper.Map<EquipmentDto>(equipment));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("equipments/{equipmentId}")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateEquipment(Guid equipmentId, [FromBody]EquipmentResponseModel equipment) {
            await _equipmentService.UpdateAsync(equipmentId, Mapper.Map<EquipmentDto>(equipment));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("equipments/{equipmentId}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveEquipment(Guid equipmentId) {
            await _equipmentService.RemoveAsync(equipmentId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}