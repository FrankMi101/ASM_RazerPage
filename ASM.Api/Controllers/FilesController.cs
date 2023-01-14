using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASM.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _extent;
        private readonly IOptions<AppConfigAppSettings> _appSettings;
        private readonly IOptions<AppConfigReportSettings> _reportSettings;
        public FilesController(FileExtensionContentTypeProvider extent, IOptions<AppConfigAppSettings> appSettings, IOptions<AppConfigReportSettings> reportSettings)
        {
            _extent = extent ?? throw new System.ArgumentNullException(nameof(extent));
            _appSettings = appSettings;
            _reportSettings = reportSettings;
        }

        // GET: api/<FilesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FilesController>/5
        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
             return GetFileFromLocal(fileId);
          
        }

        // POST api/<FilesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FilesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FilesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private ActionResult GetFileFromLocal(string fileId) {
            var pathTofile = "Source/LongTermOccasionalTeachersList.pdf";
            string saveName = "LongTermOccasionalTeachersList.pdf";

            if (!System.IO.File.Exists(pathTofile))
            {
                return NotFound();
            }

            if (!_extent.TryGetContentType(pathTofile, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            // ************** working in local file ********************************
            var bytes = System.IO.File.ReadAllBytes(pathTofile);
            return File(bytes, contentType, Path.GetFileName(saveName));

            //***************************************************************
        }
      
    }
}
