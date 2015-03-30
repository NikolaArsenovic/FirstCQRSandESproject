using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.ES
{
    public class Event:Message 
    {
        //defining event type
        public int Version;
    }

    public class ItemCreated : Event //event when item is created
    {
        public Guid id;
        public string name;

        public ItemCreated(Guid ItemId, string ItemName) //event constructor
        {
            id = ItemId;
            name = ItemName;
        }
    }

    public class ItemDeleted : Event //event when item is deleted
    {
        public Guid id;

        public ItemDeleted(Guid ItemId) //event constructor
        {
            id = ItemId;
        }
    }

}
