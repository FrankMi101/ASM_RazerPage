using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

namespace ASM.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class ReportController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _extent;
        private readonly IOptions<AppConfigAppSettings> _appSettings;
        private readonly IOptions<AppConfigReportSettings> _reportSettings;
        public ReportController(FileExtensionContentTypeProvider extent, IOptions<AppConfigAppSettings> appSettings, IOptions<AppConfigReportSettings> reportSettings)
        {
            _extent = extent ?? throw new System.ArgumentNullException(nameof(extent));
            _appSettings = appSettings;
            _reportSettings = reportSettings;
        }

        // GET api/<FilesController>L/00003862/812390342
        [HttpGet("{source}/{Id}")]
        public ActionResult GetReport(string source,string Id)
        {
            if (source =="L")
              return GetSQLReportFromLocal(Id);
            else
              return GetSQLReportFromServer(Id);

        }

        // GET api/<FilesController>L/00003862/812390342
        [HttpPost("{source}")]
        public ActionResult GetReport(string source, [FromBody] List<ReportParameter> para)
        {
            if (source == "L")
                return GetSQLReportFromLocal(para);
            else
                return GetSQLReportFromServer(para);

        }
        private ActionResult GetSQLReportFromLocal(string Id)
        {
            var pathTofile = "Source/AnnualLearningPlan.pdf";
            string saveName = "AnnualLearningPlan.pdf";
            if (!_extent.TryGetContentType(pathTofile, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var parameter = new List<ReportParameter>
                {
                    ReportBuilder.GetParameter(1, "Operate", "Appraiser"),
                    ReportBuilder.GetParameter(2, "UserID", "mif"),
                    ReportBuilder.GetParameter(3, "SchoolYear", "20192020"),
                    ReportBuilder.GetParameter(4, "SchoolCode", "0529"),
                    ReportBuilder.GetParameter(5, "EmployeeID",Id),
                    ReportBuilder.GetParameter(6, "SessionID", "Appraisal 1"),
                    ReportBuilder.GetParameter(7, "Category", "TPA")
                };

            // ************** working on local SQL service reports ********************
            string reportName = "/EPA_Reports/AnnualLearningPlan";
            var bytes = ReportBuilder.GetReport(reportName, parameter, _reportSettings.Value);
            return File(bytes, contentType, Path.GetFileName(saveName));

        }
        private ActionResult GetSQLReportFromLocal(  List<ReportParameter> parameter)
        {
            var pathTofile = "Source/AnnualLearningPlan.pdf";
            var Id = ReportBuilder.GetValueFromList("EmployeeID", parameter);
            string saveName = "AnnualLearningPlan" + Id + ".pdf";
            if (!_extent.TryGetContentType(pathTofile, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            /* Client side Json data
                        [
                          {
                            "Name": "Operate",
                            "Value": "Appraiser"
                          },
                          {
                             "Name": "UserID",
                            "Value": "mif"
                          },
                          {
                            "Name": "SchooYear",
                            "Value": "20192020"
                          },
                          {
                            "Name": "SchoolCode",
                            "Value": "0529"
                          },
                          {
                            "Name": "EmployeeID",
                            "Value": "00003862"
                          },
                          {
                            "Name": "SessionID",
                            "Value": "Appraisal 1"
                          },
                          {
                            "Name": "Category",
                            "Value": "TPA"
                          }
                        ]
            */

            // ************** working on local SQL service reports ********************
            string reportName = "/EPA_Reports/AnnualLearningPlan";
            var bytes = ReportBuilder.GetReport(reportName, parameter, _reportSettings.Value);
            return File(bytes, contentType, Path.GetFileName(saveName));

        }
        private ActionResult GetSQLReportFromServer(string Id)
        {
            var pathTofile = "IEP_Report7.pdf";
            string saveName = "IEP_Report7.pdf";

            if (!_extent.TryGetContentType(pathTofile, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var parameter = new List<ReportParameter>
                {
                    ReportBuilder.GetParameter(1, "PersonID", Id),
                    ReportBuilder.GetParameter(2, "SchoolYear", "20222023"),
                    ReportBuilder.GetParameter(3, "SchoolCode", "0205"),
                    ReportBuilder.GetParameter(4, "Term", "0")
                };

            // ************** working on local SQL service reports ********************
            string reportName = "/SES_Reports/IEP/Production/IEP_Report7";
            var bytes = ReportBuilder.GetReport(reportName, parameter, _reportSettings.Value);
            return File(bytes, contentType, Path.GetFileName(saveName));

        }
        private ActionResult GetSQLReportFromServer(List<ReportParameter> parameter)
        {
            var pathTofile = "IEP_Report7.pdf";
            var pId = ReportBuilder.GetValueFromList("PersonID", parameter);
            string saveName = "IEP_Report7_" + pId + ".pdf";

            if (!_extent.TryGetContentType(pathTofile, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            //var parameter = new List<ReportParameter>
            //    {
            //        ReportBuilder.GetParameter(1, "PersonID", Id),
            //        ReportBuilder.GetParameter(2, "SchoolYear", "20222023"),
            //        ReportBuilder.GetParameter(3, "SchoolCode", "0205"),
            //        ReportBuilder.GetParameter(4, "Term", "0")
            //    };
            /* Client side Json data
              [
                  {
                    "Name": "PersonID",
                    "Value": "812390342"
                  },
                  { "Name": "SchoolYear",
                    "Value": "20222023"
                  },
                  {
                    "Name": "SchoolCode",
                    "Value": "0205"
                  },
                  {
                    "Name": "Term",
                    "Value": "0"
                  }
               ]
             */

            // ************** working on local SQL service reports ********************
            string reportName = "/SES_Reports/IEP/Production/IEP_Report7";
            var bytes = ReportBuilder.GetReport(reportName, parameter, _reportSettings.Value);
            return File(bytes, contentType, Path.GetFileName(saveName));

        }

    }
}
