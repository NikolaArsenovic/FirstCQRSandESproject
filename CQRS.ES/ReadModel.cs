using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.ES
{
    public class ItemListDto
    {
        public Guid id;
        public string name;
        public int version;
  
        public ItemListDto(Guid ID, string Name,int Version)
        {
            id = ID;
            name = Name;
            version = Version;
        }
    }

    //denormalizer for read database
    public class ItemListView : Handles<ItemCreated>, Handles<ItemDeleted>
    {
        public void Handle(ItemCreated message)
        {
            Database.list.Add(new ItemListDto(message.id, message.name,0));
        }

        public void Handle(ItemDeleted message)
        {
            Database.list.RemoveAll(x => x.id == message.id);
        }
    }

    public interface IReadModelFacade
    {
        IEnumerable<ItemListDto> GetItemList();
    }

    public class RemoteFacade : IReadModelFacade
    {
        public IEnumerable<ItemListDto> GetItemList()
        {
            return Database.list;
        }
    }
    public class Database
    {
        public static List<ItemListDto> list = new List<ItemListDto>();
    }
}
