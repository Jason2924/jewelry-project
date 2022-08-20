//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_Sem_3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class InquiryMst
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Can not empty")]
        [RegularExpression(@"^(([a-zA-Z])+([ ]{0,1})?)+$", ErrorMessage = "Can not have any special characters or numbers")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Can not empty")]
        [RegularExpression(@"^(([a-zA-Z0-9])+([ ,-]*)?)+$", ErrorMessage = "Can not have any special characters or spaces")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Can not empty")]
        [RegularExpression(@"^(([a-zA-Z])+([ ]{0,1})?)+$", ErrorMessage = "Can not have any special characters or numbers")]
        public string City { get; set; }

        [Required(ErrorMessage = "Can not empty")]
        [RegularExpression(@"^(([a-zA-Z])+([ ]{0,1})?)+$", ErrorMessage = "Can not have any special characters or numbers")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Can not empty")]
        [Phone(ErrorMessage = "Please input valid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Can not empty")]
        [EmailAddress(ErrorMessage = "Please input valid email address")]
        public string Email { get; set; }
        public string Content { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
