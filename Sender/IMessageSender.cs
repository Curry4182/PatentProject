﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public interface IMessageSender
    {
        public Task<bool> SendEmailAsync(string phoneNumber, string text);
    }
}
