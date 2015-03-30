using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.ES.Domain
{
    public class Item:AggregateRoot
    {
        private Guid _id;
        private string name;
        private void Apply(ItemCreated e)
        {
            _id = e.id;
            name = e.name;
        }

        public override Guid id
        {
            get { return _id; }
        }

        public void Delete()
        {
            ApplyChange(new ItemDeleted(_id));
        }

        public Item()
        {
            //nothing
        }
        public Item(Guid itemId, string itemName)
        {
            ApplyChange(new ItemCreated(itemId, itemName));
        }

    }
}
