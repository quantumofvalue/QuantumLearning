﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroTDD_Module4_Library
{
    public interface EventSubscriber
    {
        void Listen(object sender, EventArgs eventArguments);
    }
}
