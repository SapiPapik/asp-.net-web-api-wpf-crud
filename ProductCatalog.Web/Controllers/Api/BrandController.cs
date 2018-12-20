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
    public class BrandController : ApiController {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService) {
            _brandService = brandService ?? throw new ArgumentNullException(nameof(brandService));
        }

        [Route("brands")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetBrands(int limit = 20, int offset = 0) {
            var brands = await _brandService.GetAllAsync(limit, offset);
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ICollection<BrandResponseModel>>(brands));
        }

        [Route("brands/{brandId}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetBrand(Guid brandId) {
            var brand = await _brandService.GetByIdAsync(brandId);
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<BrandResponseModel>(brand));
        }

        [Route("brands")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddBrand(BrandResponseModel brand) {
            await _brandService.AddAsync(Mapper.Map<BrandDto>(brand));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("brands/{brandId}")]
        [HttpPut]
        public async Task<HttpResponseMessage> EditBrand(Guid brandId, [FromBody]BrandResponseModel brand) {
            await _brandService.UpdateAsync(brandId, Mapper.Map<BrandDto>(brand));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("brands/{brandId}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteBrand(Guid brandId) {
            await _brandService.RemoveAsync(brandId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}