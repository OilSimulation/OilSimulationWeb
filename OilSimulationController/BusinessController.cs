using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using OilSimulationModel;
using EclipseUtils;

namespace OilSimulationController
{
    public class BusinessController
    {
        public ActionResult IsLocal()
        {
            EclipseModel gridModelChangeAfter = EclipseParser.ParseEgrid("D:\\WorkSpace\\VS2010\\MvcApplication2\\MvcApplication2\\DataModule\\Modle2D\\4水驱油机理仿真实训\\MODEL2D_E100.EGRID");
            Pillar[] p = gridModelChangeAfter.GetSliceK(0);
            //var res = new ConfigurableJsonResult();
            //res.Data = p;
            var res = new JsonResult();
            return res;
        }
    }
}
