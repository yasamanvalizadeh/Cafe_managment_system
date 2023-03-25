using CafeManagementSystem.Common.AttributesErrorMessageCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeManagementSystem.ViewModel
{
    public class EditItemViewModel
    {
        [Timestamp, HiddenInput]
        public byte[] RowVersion { get; set; }

        [HiddenInput]
        public int ItemId { get; set; }

        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [StringLength(50, ErrorMessage = ErrorMessageCommon.StringLengthErrorMessage)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = ErrorMessageCommon.PriceFormatErrorMessage)]
        [Range(0, 9999999999999999.99)]
        [Display(Name = "Item Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }  
    }
}
