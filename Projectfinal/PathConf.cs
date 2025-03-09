using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projectfinal
{
    class PathConf
    {
        //แก้ path ตรงนี้

        private const string V1 = @"C:\Users\krisa\source\repos\finalbank\";
        private const string V2 = @"E:\dotNet_Project\jame\";

        private string Path = V1; // or V2, depending on which path you want to use

        public string getPath() => this.Path;

        public string getFontsPath() => this.Path + @"Projectfinal\Fonts\Kanit-Bold.ttf";

        public string getPDFPath() => this.Path + @"Projectfinal\Filepdf";

        public string getDBPath() => this.Path + @"finalprojectbankingDB.db";
    }
}
