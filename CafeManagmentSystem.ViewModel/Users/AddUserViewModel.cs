using CafeManagementSystem.Common.AttributesErrorMessageCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CafeManagementSystem.ViewModel.Users
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [Display(Name ="First Name")]
        [StringLength(50,ErrorMessage =ErrorMessageCommon.StringLengthErrorMessage,MinimumLength =3)]
        public string Fname { get; set; }

        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = ErrorMessageCommon.StringLengthErrorMessage, MinimumLength = 3)]
        public string Lname { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [RegularExpression("^(?=.{8,20}$)(?![.])(?!.*[.]{2})[a-zA-Z0-9._]+(?<![.])$" , ErrorMessage =ErrorMessageCommon.UserNameFormatErrorMessage)]
        public string UserName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)] 
        [MaxLength(11, ErrorMessage = ErrorMessageCommon.MaxLengthErrorMessage)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(09[0-9]{2})([0-9]{3})([0-9]{4})$", ErrorMessage = ErrorMessageCommon.PhoneNumberFormatErrorMessage)]
        public string phoneNumber { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [StringLength(20, ErrorMessage = ErrorMessageCommon.StringLengthErrorMessage, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string password { get; set; }


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [Compare(nameof(password), ErrorMessage = ErrorMessageCommon.ConfirmPasswordErrorMessagee)]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        [Display(Name = "Tenant Id")]
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [Range(0, int.MaxValue, ErrorMessage = ErrorMessageCommon.TenantIdErrorMessage)]
        public int TenantId { get; set; }
    }
}
