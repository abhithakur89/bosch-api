using bosch_api.Helper;
using BoschApi.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bosch_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<CameraController> _logger;
        private ApplicationDbContext Context { get; set; }

        private enum ResponseCodes
        {
            [Display(Name = "Successful")]
            Successful = 1200,
            [Display(Name = "Error")]
            SystemError = 1201,
        }

        public CameraController(IConfiguration configuration, ApplicationDbContext applicationDbContext, ILogger<CameraController> logger)
        {
            Configuration = configuration;
            Context = applicationDbContext;
            _logger = logger;
        }

        /// <summary>
        /// GetAllSites API. Returns all sites info.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/getallcameras
        ///     {     
        ///         "siteid":"1"
        ///     }
        /// 
        /// Sample response:
        /// 
        ///     {
        ///         "respcode": 1200,
        ///         "description": "Successful",
        ///         "cameras": [
        ///             {
        ///                 "cameraId": 1,
        ///                 "cameraName": "Camera at Entrance",
        ///                 "cameraIP": "192.168.1.191",
        ///                 "model": "Bosch",
        ///                 "additionalDetail": "",
        ///                 "GateId": 1
        ///             },
        ///             {
        ///                 ...
        ///             }
        ///             ...
        ///         ]
        ///     }
        ///     
        /// Response codes:
        ///     1200 = "Successful"
        ///     1201 = "Error"
        /// </remarks>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Route("getallcameras")]
        public ActionResult GetAllCameras([FromBody]JObject jsiteId)
        {
            try
            {
                _logger.LogInformation("GetAllCameras() called from: " + HttpContext.Connection.RemoteIpAddress.ToString());

                var received = new { SiteId = string.Empty };

                received = JsonConvert.DeserializeAnonymousType(jsiteId.ToString(Formatting.None), received);

                _logger.LogInformation($"Paramerters: {received.SiteId}");

                if (!int.TryParse(received.SiteId, out int nSiteId)) throw new Exception("Invalid Site Id");

                var cameras = Context.Cameras
                    .Where(x => x.Gate.SiteId == nSiteId)
                    .Select(x => new
                    {
                        x.CameraId,
                        x.CameraName,
                        x.CameraIP,
                        x.Model,
                        x.AdditionalDetail,
                        x.GateId
                    });

                return new JsonResult(new
                {
                    respcode = ResponseCodes.Successful,
                    description = ResponseCodes.Successful.DisplayName(),
                    cameras
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"Generic exception handler invoked. {e.Message}: {e.StackTrace}");

                return new JsonResult(new
                {
                    respcode = ResponseCodes.SystemError,
                    description = ResponseCodes.SystemError.DisplayName(),
                    Error = e.Message
                });
            }
        }
    }
}
