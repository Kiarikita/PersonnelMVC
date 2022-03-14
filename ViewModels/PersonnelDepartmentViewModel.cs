using PersonnelMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelMVC.ViewModels
{
    public class PersonnelDepartmentViewModel
    {
        public IEnumerable<Department> Departments { get; set; }
        public Personnel Personnel { get; set; }
    }
}