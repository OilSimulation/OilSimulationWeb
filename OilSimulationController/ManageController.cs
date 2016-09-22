using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OilSimulationModel;
using EclipseUtils;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace OilSimulationController
{
    [HandleError]
    public class ManageController:Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ExamManage()
        {
            return View();
        }
    }
}
