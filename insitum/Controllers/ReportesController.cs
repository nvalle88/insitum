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
using System.Web.Mvc;
using insitum.Models;
using insitum.Models.Negocio;
using insitum.Utiles;
using insitum.Utiles.Template;
using insitum.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace insitum.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {

        private ApplicationUserManager _userManager;

        public ReportesController()
        {
        }

        public ReportesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Reportes
        [Authorize(Roles = "Cliente")]
        public async Task<ActionResult> ReporteClientes(string id)
        {



            try
            {
                var idUsuario = User.Identity.GetUserId();
                var cliente = await UserManager.FindByIdAsync(idUsuario);

                var parameters = new List<ReportParameter>();

                //Add parameter
                parameters.Add(new ReportParameter("NIP", id.ToString(), false));
                parameters.Add(new ReportParameter("Identificacion", cliente.Identificacion.ToString(), false));

                ReportViewer rptViewer = new ReportViewer();

                // ProcessingMode will be Either Remote or Local  
                rptViewer.ProcessingMode = ProcessingMode.Remote;
                rptViewer.SizeToReportContent = true;
                rptViewer.ZoomMode = ZoomMode.PageWidth;
                rptViewer.Width = Unit.Percentage(100);
                rptViewer.Height = Unit.Percentage(100);
                rptViewer.AsyncRendering = true;
                rptViewer.Visible = true;
                
                IReportServerCredentials irsc = new CustomReportCredentials(Constantes.UsuarioReport, Constantes.ContrasenaReporte);

                rptViewer.ServerReport.ReportServerCredentials = irsc;

                rptViewer.ServerReport.ReportServerUrl = new Uri(Constantes.ServerReportUrl);

                rptViewer.ServerReport.ReportPath = Constantes.ReporteClientesPath;

                rptViewer.ServerReport.SetParameters(parameters);

                rptViewer.ServerReport.Refresh();

                ViewBag.ReportViewer = rptViewer;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
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