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
<<<<<<< HEAD
        private const string V = @"C:\Users\krisa\source\repos\finalbank\";
=======
        private const string V = @"E:\dotNet_Project\jame\";
>>>>>>> b6f7b4b2d76c8ae6c477e528565d98de940adf30

        private string Path = V;

        public string getPath() => this.Path;

        public string getFontsPath() => this.Path + @"Projectfinal\Fonts\Kanit-Bold.ttf";

        public string getPDFPath() => this.Path + @"Projectfinal\Filepdf";

        public string getDBPath() => this.Path + @"finalprojectbankingDB.db";

    }
}
