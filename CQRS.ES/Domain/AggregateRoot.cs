using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.ES.Domain
{
    public abstract class AggregateRoot //aggreggate wrap Domain objects in one aggreggate object and every reference outside agreggate need to go here. aggregate is basic element of reading and writing
    // also transactions cant cross edges of aggregate
    {
        private readonly List<Event> _changes = new List<Event>(); // list of events

        public abstract Guid id { get; }
        public int version { get; internal set; }

        public IEnumerable<Event> GetUncommittedChanges() //return all uncomited events
        {
            return _changes;
        }

        public void MarkChangesAsCommitted() //clear list when changes are commited
        {
            _changes.Clear(); //it clears list
        }

        private void ApplyChange(Event @event, bool isNew) //apply change event method with 2 parameters event and is it new
        {
            this.AsDynamic().Apply(@event);
            if (isNew) _changes.Add(@event);
        }

        protected void ApplyChange(Event @event) //calling same function with one parameter now but passing hardcoded second  as true
        {
            ApplyChange(@event, true);
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history)
            {
                ApplyChange(e, false);
            }
        }
    }
}
