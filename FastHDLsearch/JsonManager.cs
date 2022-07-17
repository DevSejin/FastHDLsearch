using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastHDLsearch
{
    public class ConfigFile
    {
        public string path { get; set; } = "";
        public bool searchwrapping { get; set; }
    }

    public class JsonManager
    {
        public ConfigFile configfile = new ConfigFile();

        public void Load()
        {
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(@".\config.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                configfile = (ConfigFile)serializer.Deserialize(file, typeof(ConfigFile));
            }
        }

        public void Save()
        {
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(@".\config.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, configfile);
            }
        }
    }
}
