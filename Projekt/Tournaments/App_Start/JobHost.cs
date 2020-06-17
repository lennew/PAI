﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Tournaments.App_Start
{
    public class JobHost : IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;

        public JobHost()
        {
            HostingEnvironment.RegisterObject(this);
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _shuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }

        public void DoWork(Action work)
        {
            lock (_lock)
            {
                if (_shuttingDown)
                {
                    return;
                }
                work();
            }
        }
    }
}