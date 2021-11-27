using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace KygekTermDownload
{
    class Program
    {
        public static readonly string userAgent = "kygektermdownload";
        public static readonly string PREFIX = "[KygekTermDownload]: ";
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                SendHelpMessage();
            }
            else if (args[0].ToLower() == "help")
            {
                SendHelpMessage();
            }
            else if (args[0].ToLower() == "pm4")
            {
                if (args.Length >= 2)
                {
                    if (args[1].ToLower() == "help")
                    {
                        SendPM4HelpMessage();
                    }
                    else if (args[1].ToLower() == "windows")
                    {
                        var client = new WebClient();
                        Console.WriteLine(PREFIX + "Downloading PHP");
                        client.DownloadFile("https://jenkins.kygek.team/job/PMMP-4-PHP-Binary/lastSuccessfulBuild/artifact/Windows/PHP_Windows-x86_64.zip", "php.zip");
                        Console.WriteLine(PREFIX + "Downloading PM4");
                        client.DownloadFile("https://jenkins.kygek.team/job/PocketMine-MP-4/lastSuccessfulBuild/artifact/PocketMine-MP.phar", "pmmp.phar");
                        Console.WriteLine(PREFIX + "Downloading Command Prompt start script");
                        client.DownloadFile("https://jenkins.kygek.team/job/PocketMine-MP-4/lastSuccessfulBuild/artifact/start.cmd", "start.cmd");
                        Console.WriteLine(PREFIX + "Downloading PowerShell start script");
                        client.DownloadFile("https://jenkins.kygek.team/job/PocketMine-MP-4/lastSuccessfulBuild/artifact/start.ps1", "start.ps1");
                        Console.WriteLine(PREFIX + "Finished downloading!");
                    }
                    else if (args[1].ToLower() == "mac")
                    {
                        var client = new WebClient();
                        Console.WriteLine(PREFIX + "Downloading PHP");
                        client.DownloadFile("https://jenkins.kygek.team/job/PMMP-4-PHP-Binary/lastSuccessfulBuild/artifact/Mac/PHP_MacOS-x86_64.tar.gz", "php.tar.gz");
                        Console.WriteLine(PREFIX + "Downloading PM4");
                        client.DownloadFile("https://jenkins.kygek.team/job/PocketMine-MP-4/lastSuccessfulBuild/artifact/PocketMine-MP.phar", "pmmp.phar");
                        Console.WriteLine(PREFIX + "Downloading start script");
                        client.DownloadFile("https://jenkins.kygek.team/job/PocketMine-MP-4/lastSuccessfulBuild/artifact/start.cmd", "start.sh");
                        Console.WriteLine(PREFIX + "Finished downloading!");
                    }
                    else if (args[1].ToLower() == "linux")
                    {
                        var client = new WebClient();
                        Console.WriteLine(PREFIX + "Downloading PHP");
                        client.DownloadFile("https://jenkins.kygek.team/job/PMMP-4-PHP-Binary/lastSuccessfulBuild/artifact/Linux/PHP_Linux-x86_64.tar.gz", "php.tar.gz");
                        Console.WriteLine(PREFIX + "Downloading PM4");
                        client.DownloadFile("https://jenkins.kygek.team/job/PocketMine-MP-4/lastSuccessfulBuild/artifact/PocketMine-MP.phar", "pmmp.phar");
                        Console.WriteLine(PREFIX + "Downloading start script");
                        client.DownloadFile("https://jenkins.kygek.team/job/PocketMine-MP-4/lastSuccessfulBuild/artifact/start.cmd", "start.sh");
                        Console.WriteLine(PREFIX + "Finished downloading!");
                    }
                    else
                    {
                        SendPM4HelpMessage();
                    }
                }
                else
                {
                    SendPM4HelpMessage();
                }
            }
            else if (args[0].ToLower() == "list")
            {
                SendListOfPlugins();
            }
            else if (args[0].ToLower() == "download")
            {
                if (args.Length >= 2)
                {
                    WebClient client = new WebClient();
                    Stream data = client.OpenRead("https://api.kygek.team");
                    StreamReader reader = new StreamReader(data);
                    string dataString = reader.ReadToEnd();
                    data.Close();
                    reader.Close();
                    KygekPlugin[] plugins = JsonConvert.DeserializeObject<KygekPlugin[]>(dataString);
                    bool compatible = true;
                    KygekPlugin match = null;
                    bool exists = true;
                    if (args.Length >= 3)
                    {
                        if (args[1] == "pmmp")
                        {
                            foreach (KygekPlugin plugin in plugins)
                            {
                                if (plugin.name.ToLower() == args[2].ToLower())
                                {
                                    match = plugin;
                                }
                            }

                            if (match == null)
                            {
                                exists = false;
                            }

                            if (exists)
                            {
                                if (match.downloads.poggit == null)
                                {
                                    compatible = false;
                                }

                                if (compatible)
                                {
                                    Stream data2 = client.OpenRead("https://poggit.pmmp.io/releases.json?name=" + match.name);
                                    StreamReader reader2 = new StreamReader(data2);
                                    string dataString2 = reader2.ReadToEnd();
                                    data2.Close();
                                    reader2.Close();
                                    PoggitArtifact[] artifacts = JsonConvert.DeserializeObject<PoggitArtifact[]>(dataString2);
                                    PoggitArtifact artifact = artifacts[0];
                                    Console.WriteLine("Downloading " + match.name);
                                    client.DownloadFile(artifact.artifact_url, match.name + ".phar");
                                }
                                else
                                {
                                    Console.WriteLine("This plugin isn't compatible with the server type you selected.");
                                }
                            }
                            else
                            {
                                Console.WriteLine(PREFIX + "That plugin doesn't exist!");
                            }
                        }
                        else if (args[1] == "nukkit")
                        {
                            foreach (KygekPlugin plugin in plugins)
                            {
                                if (plugin.name.ToLower() == args[2].ToLower())
                                {
                                    match = plugin;
                                }
                            }

                            if (match == null)
                            {
                                exists = false;
                            }

                            if (exists)
                            {
                                if (match.downloads.nukkit == null)
                                {
                                    compatible = false;
                                }

                                if (compatible)
                                {
                                    Console.WriteLine(PREFIX + "Downloading " + match.name);
                                    client.DownloadFile(match.downloads.nukkit + "/download", match.name + ".jar");
                                }
                                else
                                {
                                    Console.WriteLine("This plugin isn't compatible with the server type you selected.");
                                }
                            }
                            else
                            {
                                Console.WriteLine(PREFIX + "That plugin doesn't exist!");
                            }
                        }
                        else if (args[1] == "spigot")
                        {
                            foreach (KygekPlugin plugin in plugins)
                            {
                                if (plugin.name.ToLower() == args[2].ToLower())
                                {
                                    match = plugin;
                                }
                            }

                            if (match == null)
                            {
                                exists = false;
                            }

                            if (exists)
                            {
                                if (match.downloads.spigot == null)
                                {
                                    compatible = false;
                                }

                                if (compatible)
                                {

                                    Stream data2 = client.OpenRead("https://api.spiget.org/v2/search/resources/" + match.name + "?field=name");
                                    StreamReader reader2 = new StreamReader(data2);
                                    string dataString2 = reader2.ReadToEnd();
                                    data2.Close();
                                    reader2.Close();
                                    SpigetID[] ids = JsonConvert.DeserializeObject<SpigetID[]>(dataString2);
                                    SpigetID id = ids[0];
                                    Console.WriteLine("Downloading " + match.name);
                                    client.DownloadFile("https://api.spiget.org/v2/resources/" + id.id + "/download", match.name + ".jar");
                                }
                                else
                                {
                                    Console.WriteLine("This plugin isn't compatible with the server type you selected.");
                                }
                            }
                            else
                            {
                                Console.WriteLine(PREFIX + "That plugin doesn't exist!");
                            }
                        } else
                        {
                            Console.WriteLine(PREFIX + "Please specify a server to use!");
                            Console.WriteLine(PREFIX + "Valid options: pmmp, nukkit, spigot");
                        }
                    }
                    else
                    {
                        if (args[1].ToLower() == "pmmp" | args[1].ToLower() == "nukkit" | args[1].ToLower() == "spigot") {
                            Console.WriteLine(PREFIX + "Please specify a plugin to download!");
                        }
                        else
                        {
                            Console.WriteLine(PREFIX + "Two errors found:");
                            Console.WriteLine("    Please specify a server to use!");
                            Console.WriteLine("        Valid options: pmmp, nukkit, spigot");
                            Console.WriteLine("    Please specify a plugin to download!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please specify a server to use!");
                    Console.WriteLine("Valid options: pmmp, nukkit, spigot");
                }
            }
            else if (args[0].ToLower() == "easygamemode")
            {
                WebClient client = new WebClient();
                Console.WriteLine("Downloading KygekEasyGamemode");
                client.DownloadFile("https://kygek.team/plugins/KygekEasyGamemode_v1.1.1.phar", "KygekEasyGamemode.phar");
            }
            else
            {
                Console.WriteLine(PREFIX + "Command not found");
                SendHelpMessage();
            }
            return 0;
        }
        private static void SendHelpMessage()
        {
            Console.WriteLine(PREFIX + "help         | Read this message.");
            Console.WriteLine(PREFIX + "pm4          | Download the PocketMine-MP 4 binary as well as the PHP binary for your system.");
            Console.WriteLine(PREFIX + "list         | Get a list of all KygekTeam plugins.");
            Console.WriteLine(PREFIX + "download     | Downloads a plugin from Poggit, Nukkit resources, or Spigot resources.");
            Console.WriteLine(PREFIX + "easygamemode | Downloads KygekEasyGamemode for PocketMine-MP, as you cannot download it from Poggit.");
        }
        private static void SendPM4HelpMessage()
        {
            Console.WriteLine(PREFIX + "Usage: ktd pm4 <windows/mac/linux>");
            Console.WriteLine(PREFIX + "This command will be slightly different on Mac or Linux.");
        }
        private static void SendListOfPlugins()
        {
            WebClient client = new WebClient();
            Stream data = client.OpenRead("https://api.kygek.team");
            StreamReader reader = new StreamReader(data);
            string dataString = reader.ReadToEnd();
            data.Close();
            reader.Close();
            KygekPlugin[] plugins = JsonConvert.DeserializeObject<KygekPlugin[]>(dataString);
            foreach (KygekPlugin plugin in plugins)
            {
                Console.WriteLine(PREFIX + plugin.name + " by " + plugin.author);
                Console.WriteLine("    " + plugin.description);
                bool poggitExists = true;
                bool nukkitExists = true;
                bool spigotExists = true;
                if (plugin.downloads.poggit == null)
                {
                    poggitExists = false;
                }
                if (plugin.downloads.nukkit == null)
                {
                    nukkitExists = false;
                }
                if (plugin.downloads.spigot == null)
                {
                    spigotExists = false;
                }
                if (poggitExists)
                {
                    Console.WriteLine("    Poggit: " + plugin.downloads.poggit);
                }
                if (nukkitExists)
                {
                    Console.WriteLine("    Nukkit: " + plugin.downloads.nukkit);
                }
                if (spigotExists)
                {
                    Console.WriteLine("    Spigot: " + plugin.downloads.spigot);
                }
            }
        }
    }
}
