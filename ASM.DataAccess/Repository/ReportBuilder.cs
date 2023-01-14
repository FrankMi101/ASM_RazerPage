using ASM.Models;
//using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
namespace ASM.DataAccess.Repository
{

    public class ReportBuilder
    {

        public ReportBuilder()
        {
        }

        public static void setParameterArray(List<ReportParameter> _ParaArray, int X, string _Name, string _Value)
        {
            try
            {
                _ParaArray[X].Name = _Name;
                _ParaArray[X].Value = _Value;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }


        public static Byte[] GetReport(string _reportName, List<ReportParameter> _reportParameter, AppConfigReportSettings reportsSettings)
        {
            try
            {
                string accessUser = reportsSettings.ReportUser;
                string accessRWSPW = reportsSettings.ReportPW;
                string accessDomain = reportsSettings.Domain;
                string reportingUri = reportsSettings.ReportServer;
                string format = reportsSettings.ReportFormat;

                string report = _reportName;

                ServerReport RS = new ServerReport();

                RS.ReportServerCredentials.NetworkCredentials = new System.Net.NetworkCredential(accessUser, accessRWSPW, accessDomain);
                RS.ReportServerUrl = new Uri(reportingUri); // new Uri("http://localhost/ReportServer");
                RS.ReportPath = report;

                var rptParameters = new List<Microsoft.Reporting.NETCore.ReportParameter>();

                foreach (var para in _reportParameter)
                {
                    rptParameters.Add(new Microsoft.Reporting.NETCore.ReportParameter(para.Name, para.Value));
                }

                RS.SetParameters(rptParameters);
                byte[] pdf = RS.Render("PDF");

                return pdf;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return new Byte[0];
            }
        }

        //public static Byte[] MultiplePDF(string[] mySelectIDArray, string reportName, string schoolyear, string schoolcode, string sessionID)
        //{
        //    Document doc = new Document();
        //    MemoryStream msOutput = new MemoryStream();
        //    //           PdfCopy pCopy;
        //    PdfCopy pCopy = new PdfSmartCopy(doc, msOutput);
        //    doc.Open();

        //    for (int j = 0; j < mySelectIDArray.Length; j++)
        //    {

        //        string userID = HttpContext.Current.User.Identity.Name;
        //        string employeeID = mySelectIDArray[j].ToString();
        //        if (employeeID != "")
        //        {
        //            try
        //            {
        //                Byte[] myPDF;
        //                myPDF = GetOneReport(reportName, userID, schoolyear, schoolcode, sessionID, employeeID);
        //                MemoryStream stream1 = new MemoryStream(myPDF);
        //            iTextSharp.text.pdf.PdfReader pdfFile1 = new PdfReader(stream1.ToArray());
        //                for (int i = 1; i <= pdfFile1.NumberOfPages; i++)
        //                {
        //                    pCopy.AddPage(pCopy.GetImportedPage(pdfFile1, i));
        //                }
        //                pdfFile1.Close();
        //            }
        //            catch { }
        //        }

        //    }
        //    try
        //    {
        //        pCopy.Close();
        //        doc.Close();
        //    }
        //    catch { }

        //    return msOutput.ToArray();
        //}


        private static Byte[] GetOneReport(string reportName, string userID, string schoolyear, string schoolcode, string sessionID, string employeeID, AppConfigReportSettings reportSetting)
        {
            var reportParameters = new List<ReportParameter>();
            setParameterArray(reportParameters, 0, "Operate", "Report");
            setParameterArray(reportParameters, 1, "UserID", userID);
            setParameterArray(reportParameters, 2, "SchoolYear", schoolyear);
            setParameterArray(reportParameters, 3, "SchoolCode", schoolcode);
            setParameterArray(reportParameters, 4, "EmployeeID", employeeID);
            setParameterArray(reportParameters, 5, "SessionID", sessionID);
            setParameterArray(reportParameters, 6, "Category", "EPA");
            return GetReport(reportName, reportParameters, reportSetting);

        }

        public static string GetValueFromList(string name, List<ReportParameter> para)
        {
            try
            {
                string val = (from item in para
                              where item.Name == name
                              select item.Value).FirstOrDefault();

                return val;
            }
            catch (Exception)
            {

                return "";
            }


        }
        public static string reportFormat(string pFormat)
        {
            string rValue = "";
            switch (pFormat)
            {
                case "PDF1":
                    rValue = "&rs:Command=Render&rs:Format=PDF&rs:ClearSession=true&rc:Toolbar=false";
                    break;
                case "PDF":
                    rValue = "&rs:Command=Render&rs:Format=PDF&rs:ClearSession=true&rc:Toolbar=false&rc:LinkTarget=_blank";
                    break;
                case "PDFV":
                    rValue = "&rs:Command=Render&rs:Format=PDF&rs:ClearSession=true&rc:Toolbar=true&rc:LinkTarget=_blank";
                    break;
                case "CSV":
                    rValue = "&rs:Command=Render&rs:Format=CSV&rc:Toolbar=false&rc:LinkTarget=_blank";
                    break;
                case "EXCEL":
                    rValue = "&rs:Command=Render&rs:Format=EXCEL&rc:Toolbar=false&rc:LinkTarget=_blank";
                    break;
                case "IMAGE":
                    rValue = "&rs:Command=Render&rs:Format=IMAGE&rc:Toolbar=false&rc:LinkTarget=_blank";
                    break;
                case "HTML":
                    rValue = "&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=false&rc:LinkTarget=_blank";
                    break;
                case "HTMLV":
                    rValue = "&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=true&rc:LinkTarget=_blank";
                    break;
                case "XML":
                    rValue = "&rs:Command=Render&rs:Format=XML&rc:Toolbar=false&rc:LinkTarget=_blank";
                    break;
                default:

                    rValue = "&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=false&rc:LinkTarget=_blank";

                    break;
            }
            return rValue;

        }
        public static string getReportContentType(string _reportFormat)
        {

            string rValue = "";
            switch (_reportFormat)
            {
                case "PDF":
                    rValue = "application/pdf";
                    break;
                case "CSV":
                    rValue = "application/csv";
                    break;
                case "EXCEL":
                    rValue = "application / vnd.ms - excel";
                    break;
                case "IMAGE":
                    rValue = "image/tiff";
                    break;
                case "HTML":
                    rValue = "application/html";
                    break;
                case "XML":
                    rValue = "application/xml";
                    break;
                default:

                    rValue = "application/pdf";

                    break;
            }
            return rValue;

        }

        public static ReportParameter GetParameter(Int16 Seq, string pName, string pValue)
        {
            return new ReportParameter() { Name = pName, Value = pValue };
        }

        public static string getFileExtension(string _reportFormat)
        {

            string rValue = "";
            switch (_reportFormat)
            {
                case "PDF":
                    rValue = ".pdf";
                    break;
                case "CSV":
                    rValue = ".csv";
                    break;
                case "EXCEL":
                    rValue = ".xls";
                    break;
                case "IMAGE":
                    rValue = ".gif";
                    break;
                case "HTML":
                    rValue = ".html";
                    break;
                case "XML":
                    rValue = ".xml";
                    break;
                default:
                    rValue = ".pdf";

                    break;
            }
            return rValue;

        }
    }
    public   class ReportParameter :NameValue
    {
        //public string ParaName { get; set; }
        //public string ParaValue { get; set; }
    }

}