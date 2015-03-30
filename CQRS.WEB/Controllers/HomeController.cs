using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CQRS.ES;


namespace CQRS.WEB.Controllers
{
    public class HomeController : Controller
    {
        private Bus _bus;
        private RemoteFacade readFacade;

        public HomeController()
        {
            _bus = Service.Bus;
            readFacade = new RemoteFacade();
        }
        public ActionResult Index()
        {
            ViewData.Model = readFacade.GetItemList();

            return View();

        }

         [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
         public ActionResult Add(string name)
         {
             _bus.Send(new CreateItem(Guid.NewGuid(), name));

             return RedirectToAction("Index");
         }

       
        public ActionResult Delete(Guid id,int OriginalVersion)
        {
            _bus.Send(new DeleteItem(id, OriginalVersion));

            return RedirectToAction("Index");
        }


	}
}