using System;
using System.Collections.Generic;
using System.Text;

namespace KygekTermDownload
{
    class PluginDownloads
    {
        public string poggit, nukkit, spigot;

        public PluginDownloads(string poggit = null, string nukkit = null, string spigot = null)
        {
            this.poggit = poggit;
            this.nukkit = nukkit;
            this.spigot = spigot;
        }
    }
}
