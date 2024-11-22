using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projectfinal.Model
{
    public class UserPayment
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Family { get; set; }
        public string Fullname { get; set; }
        public string NumberLone { get; set; }
        public string LoneCategory { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal Interest { get; set; }
        public decimal RemainingBalance { get; set; }  // ใช้ชื่อนี้แทน MoneyLoneTotal
        public DateTime PaymentDate { get; set; }

    }
}
