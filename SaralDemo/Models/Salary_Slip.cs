//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SaralDemo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Salary_Slip
    {
        public int SalaryID { get; set; }

        [Display(Name = "Employee Name")]
        public int EmployeeID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter Date")]
        public Nullable<System.DateTime> Date { get; set; }

        [Display(Name = "Basic Pay")]
        [RegularExpression(@"^[+-]?([0-9]+\.?[0-9]*|\.[0-9]+)$", ErrorMessage = "Please Enter Number Value")]
        [Required(ErrorMessage = "Please enter Basic Pay")]
        public Nullable<double> BasicPay { get; set; }

        [Display(Name = "Travel Allowance")]
        [Required(ErrorMessage = "Please enter Travel Allowance")]
        public Nullable<double> TravelAllowance { get; set; }

        [Display(Name = "Esic Charges")]
        [Required(ErrorMessage = "Please enter Esic Charges")]
        public Nullable<double> EsicCharges { get; set; }

        [Display(Name = "Pf Charges")]
        [Required(ErrorMessage = "Please enter Pf Charges")]
        public Nullable<double> PfCharges { get; set; }

        [Display(Name = "Medical Allowance")]
        [Required(ErrorMessage = "Please enter Medical Allowance")]
        public Nullable<double> MedicalAllowance { get; set; }

        [Display(Name = "Tax 4%")]
        public Nullable<double> Tax { get; set; }

        [Display(Name = "Leave Deduction")]
        [RegularExpression(@"^[+-]?([0-9]+\.?[0-9]*|\.[0-9]+)$", ErrorMessage = "Please Enter Number Value")]
        public Nullable<double> LeaveDeduction { get; set; }

        [Display(Name = "Total Amount")]
        public Nullable<decimal> TotalAmount { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
