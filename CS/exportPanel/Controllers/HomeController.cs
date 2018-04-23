using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace exportPanel.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult GridViewPartial() {
            return PartialView(GetData());
        }
        public ActionResult Export() {
            var dataStream = Session["stream"] as MemoryStream;
            string disposition = "attachment";
            var response = HttpContext.Response;
            response.Clear();
            response.Buffer = false;
            response.AppendHeader("Content-Type", string.Format("application/{0}", "pdf"));
            response.AppendHeader("Content-Transfer-Encoding", "binary");
            response.AppendHeader("Content-Disposition", string.Format("{0}; filename={1}.{2}", disposition, HttpUtility.UrlEncode("grid").Replace("+", "%20"), "pdf"));
            response.BinaryWrite(dataStream.ToArray());
            response.End();
            return null;
        }
        public ActionResult CallbackExport() {
            Thread.Sleep(5000);
            MemoryStream stream = new MemoryStream();
            GridViewExtension.WritePdf(GetSettings(), GetData(), stream);
            Session["stream"] = stream;
            return null;
        }

        public static object GetData() {
            return Enumerable.Range(0, 10).Select(i => new { ID = i, Text = "Text " + i});
        }

        public static GridViewSettings GetSettings() {
            var settings = new GridViewSettings();
            settings.Name = "gridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };
            return settings;
        }
    }
}