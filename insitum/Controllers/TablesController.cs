#region Using

using System.Web.Mvc;

#endregion

namespace SmartAdminMvc.Controllers
{
     
    public class TablesController : Controller
    {
        // GET: tables/normal
        public ActionResult Normal()
        {
            return View();
        }

        // GET: tables/data-tables
        public ActionResult DataTables()
        {
            return View();
        }

        // GET: tables/jq-grid
        public ActionResult JQGrid()
        {
            return View();
        }
    }
}