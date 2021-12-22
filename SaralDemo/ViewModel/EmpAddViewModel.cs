using SaralDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaralDemo.ViewModel
{
    public class EmpAddViewModel
    {
        public Employee Emp { get; set; }
        public Documents_BankDetails bank { get; set; }

        public TechnicalDetail tech { get; set; }
    }
}