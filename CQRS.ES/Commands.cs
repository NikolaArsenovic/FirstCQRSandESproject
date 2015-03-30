using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.ES
{
    public class Command:Message
    {
        //defining command type
    }

    public class CreateItem:Command //command to create item
    {
        public Guid id;
        public string name;

        public CreateItem(Guid ItemId,string ItemName) //command constructor
        {
            id = ItemId;
            name = ItemName;
        }
    }

    public class DeleteItem:Command //command for deleting item
    {
        public Guid id;
        public int originalVersion;
        
        public DeleteItem(Guid ItemId,int OriginalVersion) //command constructor
        {
            id = ItemId;
            originalVersion = OriginalVersion;
            
        }
    }
}
