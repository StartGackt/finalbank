using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Projectfinal.Model
{
    public class User
    {
   
        public int Id { get; set; }

        public string Username { get; set; }
        public string Family { get; set; }
        public string IdCard { get; internal set; }
        public string Phone { get; set; }
        public string Prefix { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string User1 { get; set; }
        public string User2 { get; set; }
        public string PhoneUser1 { get; set; }
        public string PhoneUser2 { get; set; }
        public virtual ICollection<MoneyTrans> MoneyTransactions { get; set; }
    }
}