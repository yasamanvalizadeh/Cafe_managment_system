using CafeManagementSystem.Common.AttributesErrorMessageCommon; 
using CafeSystemManagement.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CafeManagementSystem.ViewModel
{
    public class AddItemViewModel 
    {
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [StringLength(50,ErrorMessage = ErrorMessageCommon.StringLengthErrorMessage, MinimumLength =4)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; } 
         
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$" , ErrorMessage = ErrorMessageCommon.PriceFormatErrorMessage)]
        [Range(0, 9999999999999999.99)]
        [Display(Name = "Item Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Item Categoty")]
        public int CategoryId { get; set; }

        [Display(Name = "Tenant Id")]
        [Required(ErrorMessage = ErrorMessageCommon.RequiredErrorMessage)]
        [Range(0, int.MaxValue, ErrorMessage = ErrorMessageCommon.TenantIdErrorMessage)]
        public int TenantId { get;set; }
    }
}