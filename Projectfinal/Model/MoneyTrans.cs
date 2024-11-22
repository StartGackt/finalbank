using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projectfinal.Model
{
    public class MoneyTrans
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Family { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public decimal MoneyOld { get; set; }
        public decimal MoneyLast { get; set; }
        public decimal MoneyTotal { get; set; }
        public DateTime TimeMoney { get; set; }
    }
}
