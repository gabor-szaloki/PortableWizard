﻿using PortableWizard.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PortableWizard.Model
{
    class Application
    {
        public string Name { get; set; }
		public BitmapImage Icon { get; set; }

        public bool IsDesktopShortcut { get; set; }
        public bool IsStartMenuShortcut { get; set; }
        public bool IsPinnedToStart { get; set; }
        public bool IsPinnedToTaskbar { get; set; }
        public bool IsStartup { get; set; }

        //public bool WillProcessed { get; set; }

        public List<string> SupportedFileExtensions { get; set; }
        public List<string> HandledFileExtensions { get; set; }        

        private FileInfo ConfigFile;

        public Application() { }

        public Application(FileInfo configFile)
        {
            ConfigFile = configFile;

            IniFile iniFile = new IniFile(ConfigFile.FullName);
            Name = iniFile.IniReadValue("Details", "Name");
            IsDesktopShortcut = false;
            IsStartMenuShortcut = false;
            IsPinnedToStart = false;
            IsPinnedToTaskbar = false;
            //WillProcessed = false;
            IsStartup = false;

            SupportedFileExtensions = new List<string>();
            string association = iniFile.IniReadValue("Associations", "FileTypes");
            if (association != "")
            {
                SupportedFileExtensions.AddRange(association.Split(','));
            }

            FileInfo iconPath = new FileInfo(ConfigFile.Directory.FullName + @"\appicon_32.png");
            if (iconPath.Exists)
            {
                Icon = new BitmapImage(new Uri(ConfigFile.Directory.FullName + @"\appicon_32.png"));
            }
        }

    }
}
