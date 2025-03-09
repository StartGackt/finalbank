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
        private const string V = @"C:\Users\krisa\source\repos\finalbank\";

        private string Path = V;

        public string getPath() => this.Path;

        public string getFontsPath() => this.Path + @"Projectfinal\Fonts\Kanit-Bold.ttf";

        public string getPDFPath() => this.Path + @"Projectfinal\Filepdf";

        public string getDBPath() => this.Path + @"finalprojectbankingDB.db";

    }
}
