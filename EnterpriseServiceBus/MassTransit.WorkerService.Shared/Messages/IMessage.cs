﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransit.WorkerService.Shared.Messages
{
    public interface IMessage
    {
        public string Text { get; set; }
    }
}
