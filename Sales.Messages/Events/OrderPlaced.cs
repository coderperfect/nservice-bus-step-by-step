﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Messages.Events
{
    public class OrderPlaced : IEvent
    {
        public string OrderId { get; set; }
    }
}
