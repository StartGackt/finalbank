﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Projectfinal.Model
{
    public class dbcontext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite(@"Data Source = C:\Users\User\Documents\finalbank\finalprojectbankingDB.db");
           optionsBuilder.UseSqlite(@"Data Source = " + new PathConf().getDBPath());
            //optionsBuilder.UseSqlite(@"Data Source = E:\dotNet_Project\jame\finalprojectbankingDB.db");

            optionsBuilder.EnableSensitiveDataLogging();
            
        }

        public DbSet<AdminRegisterModel> AdminRegisters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MoneyTrans> MoneyTranss { get; set; }
        public DbSet<OrdLone> OrdLones { get; set; }
        public DbSet<EditOrdLone> EditOrdLones { get; set; }
        public DbSet<Emer> Emers { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }
        public DbSet<DivPeople> DivPeoples { get; set; }
    }


}
