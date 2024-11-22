using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projectfinal.Model
{
   public class Emer
    {
        public int Id { get; set; } // Assuming there is an Id field as primary key
        public string Username { get; set; }
        public string Family { get; set; }
        public string Fullname { get; set; }
        public string Username1 { get; set; }
        public string Fullname1 { get; set; }
        public string Phone1 { get; set; }
        public string Username2 { get; set; }
        public string Fullname2 { get; set; }
        public string Phone2 { get; set; }
        public string Username3 { get; set; }
        public string Fullname3 { get; set; }
        public string Phone3 { get; set; }
        public double MoneyOld { get; set; }
        public double LoneMoney { get; set; }
        public double TotalMoneyLone { get; set; }
        public string NumberLone { get; set; } 
        public DateTime TimeLone { get; set; }
        public string Interrate { get; internal set; }

    }
}
