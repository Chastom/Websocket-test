using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Iterators
{
    public class StrategyAggregate : Aggregate
    {
        private static ArrayList _items = new ArrayList();

        public override Iterator CreateIterator()
        {
            return new StrategyIterator(this);
        }   

        // Gets item count
        public int Count
        {
            get { return _items.Count; }
        }

        // Indexer
        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }
}
