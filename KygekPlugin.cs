using System;
using System.Collections.Generic;
using System.Text;

namespace KygekTermDownload
{
    class KygekPlugin
    {
        public string name, description, author;
        public PluginDownloads downloads;

        public KygekPlugin(string name = null, string description = null, string author = null, PluginDownloads downloads = null)
        {
            this.name = name;
            this.description = description;
            this.author = author;
            this.downloads = downloads;
        }
    }
}
