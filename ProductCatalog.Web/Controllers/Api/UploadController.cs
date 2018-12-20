using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ProductCatalog.BLL.Contracts.Contracts;

namespace ProductCatalog.Web.Controllers.Api {
    [RoutePrefix("api")]
    public class UploadController : ApiController {

        private readonly IEquipmentService _equipmentService;

        public UploadController(IEquipmentService equipmentService) {
            _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
        }

        [Route("upload")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostFormData() {
            // Check if the request contains multipart/form-data. 
            if (!Request.Content.IsMimeMultipartContent()) {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/App_Data");
            if (HttpContext.Current.Request.Files.Count > 0) {
                foreach (string file in HttpContext.Current.Request.Files) {
                    var postedFile = HttpContext.Current.Request.Files[file];
                    var filePath = $"{root}\\{postedFile.FileName}";
                    postedFile.SaveAs(filePath);
                    await _equipmentService.ImportFromFile(filePath);
                    RemoveFile(filePath);
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        private void RemoveFile(string filePath) {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }
    }
}