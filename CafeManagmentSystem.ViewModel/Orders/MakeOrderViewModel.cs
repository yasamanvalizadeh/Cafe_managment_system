using CafeManagementSystem.Common.AttributesErrorMessageCommon; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CafeManagementSystem.ViewModel.Orders
{
    public class MakeOrderViewModel
    {
        [HiddenInput]
        public int ItemId { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [RegularExpression("^(?=.{8,20}$)(?![.])(?!.*[.]{2})[a-zA-Z0-9._]+(?<![.])$", ErrorMessage = ErrorMessageCommon.UserNameFormatErrorMessage)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [StringLength(20, ErrorMessage = ErrorMessageCommon.StringLengthErrorMessage, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string password { get; set; }

         
        [Display(Name = "Qty")]
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        public int qty { get; set; }

        [Display(Name = "Order Status")]
        public string? OrderStatuses { get; set; }

        [Display(Name = "Items")]
        public List<CheckboxViewModel> items { get; set; }
 
    }
}
