using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CQRS.ES;
using CQRS.ES.Domain;


namespace CQRS.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var bus = new Bus();

            var storage = new EventStore(bus);
            var repository = new Repository<Item>(storage);
            var commands = new ItemCommandHandlers(repository);

            bus.RegisterHandler<CreateItem>(commands.Handle);
            bus.RegisterHandler<DeleteItem>(commands.Handle);

            var list = new ItemListView();
            bus.RegisterHandler<ItemCreated>(list.Handle);
            bus.RegisterHandler<ItemDeleted>(list.Handle);

            Service.Bus = bus;



        }
    }
}
