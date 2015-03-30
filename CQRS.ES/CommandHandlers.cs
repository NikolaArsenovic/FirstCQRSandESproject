using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.ES.Domain;

namespace CQRS.ES
{
    public class ItemCommandHandlers
    {
        private readonly IRepository<Item> _repository;

        public ItemCommandHandlers(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateItem message)
        {
            var item=new Item(message.id,message.name);
            _repository.Save(item,-1);
        }

        public void Handle(DeleteItem message)
        {
            var item = _repository.GetById(message.id);
           item.Delete();
           _repository.Save(item, message.originalVersion);
        }
    }
}
