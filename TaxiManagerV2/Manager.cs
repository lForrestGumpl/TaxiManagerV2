﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
    public class Manager: ManagerSql
    {
        public int IdManager { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public string RedirectionCode { get; set; }
        public string Status { get; set; }

        public void CreateManager()
        {
            IdManager = GetNextId("manager_table");
            CreateNewManager(Fname, Sname, RedirectionCode, Status);
        }
        public void Delete()
        {
            DeleteManager(IdManager);
        }
        public void Update()
        {
            UpdateManager(Fname, Sname, RedirectionCode, Status, IdManager);
        }
    }
}
