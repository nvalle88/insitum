using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using insitum.Utiles;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace insitum.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {
        // GET: Reportes
        [Authorize(Roles = "Cliente")]
        public ActionResult ReporteClientes(string id)
        {


            var userWithClaims = (ClaimsPrincipal)User;
            var idUsuario = userWithClaims.Claims.First(c => c.Type == Constantes.IdUsuario).Value;

            ReportViewer rptViewer = new ReportViewer();

            // ProcessingMode will be Either Remote or Local  
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(100);
            rptViewer.Height = Unit.Percentage(100);
            rptViewer.AsyncRendering = true;


            IReportServerCredentials irsc = new CustomReportCredentials(Constantes.UsuarioReport, Constantes.ContrasenaReporte);

            rptViewer.ServerReport.ReportServerCredentials = irsc;

            rptViewer.ServerReport.ReportServerUrl = new Uri(Constantes.ServerReportUrl);

            rptViewer.ServerReport.ReportPath = Constantes.ReporteClientesPath;

            ViewBag.ReportViewer = rptViewer;
            return View();
        }

        [Authorize(Roles = "Trabajador")]
        public ActionResult ReporteGestion()
        {
            ReportViewer rptViewer = new ReportViewer();

            // ProcessingMode will be Either Remote or Local  
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(100);
            rptViewer.Height = Unit.Percentage(100);
            rptViewer.AsyncRendering = true;


            IReportServerCredentials irsc = new CustomReportCredentials(Constantes.UsuarioReport, Constantes.ContrasenaReporte);

            rptViewer.ServerReport.ReportServerCredentials = irsc;

            rptViewer.ServerReport.ReportServerUrl = new Uri(Constantes.ServerReportUrl);

            rptViewer.ServerReport.ReportPath = Constantes.ReporteGestionPath;

            ViewBag.ReportViewer = rptViewer;
            return View();
        }
      
    }
}