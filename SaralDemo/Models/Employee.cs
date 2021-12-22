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

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Documents_BankDetails = new HashSet<Documents_BankDetails>();
            this.Leave_Request = new HashSet<Leave_Request>();
            this.LeaveCounts = new HashSet<LeaveCount>();
            this.Roles = new HashSet<Role>();
            this.Salary_Slip = new HashSet<Salary_Slip>();
            this.TechnicalDetails = new HashSet<TechnicalDetail>();
        }

        public int EmployeeID { get; set; }


        [Display(Name = "Full Name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s]+$", ErrorMessage = "Use Only Characters")]
        [Required(ErrorMessage = "Please enter Full Name")]
        public string FullName { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is notvalid")]
        [Required(ErrorMessage = "Please enter Email Id")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Password")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select a Gender")]
        public string Gender { get; set; }

        [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Number Should Start From 7/8/9")]
        [Required(ErrorMessage = "Please enter Contact No")]
        public string Contact { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter Date Of Birth")]
        public Nullable<System.DateTime> DOB { get; set; }

        [RegularExpression(@"^[A|B|AB|O][\+|\-]", ErrorMessage = "Please Insert Valid Blood Group")]
        [Required(ErrorMessage = "Please enter Blood Group")]
        public string BloodGroup { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter Date OF Joining")]
        public Nullable<System.DateTime> DOJ { get; set; }

        [Required(ErrorMessage = "Please enter Marital Status")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "Please enter Father Name")]
        [Display(Name = "Father Name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s]+$", ErrorMessage = "Use Only Characters ")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Please enter Mother Name")]
        [Display(Name = "Mother Name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s]+$", ErrorMessage = "Use Only Characters ")]
        public string MotherName { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Current Address")]
        [Required(ErrorMessage = "Please enter Current Address")]
        public string CurrentAddress { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Permanent Address")]
        [Required(ErrorMessage = "Please enter Permanent Address")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Please enter Designation")]
        public string Designation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documents_BankDetails> Documents_BankDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leave_Request> Leave_Request { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveCount> LeaveCounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salary_Slip> Salary_Slip { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TechnicalDetail> TechnicalDetails { get; set; }
    }
}