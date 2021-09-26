using System;
using System.Collections.Generic;

namespace NG.ServiceWorker
{
    public class MainDataRefListItem<ItemType>
    {
        public string ItemKey { get; set; }

        public ItemType Item { get; set; }
    }
}
