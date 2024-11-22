using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projectfinal.Model
{
    public class AdminRegisterModel// Corrected spelling here
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Idcard { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Time { get; set; }
        public string Position { get; set; }
    }
}

