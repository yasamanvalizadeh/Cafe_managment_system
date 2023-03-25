using CafeManagementSystem.Common.AttributesErrorMessageCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.ViewModel
{
    public class UpdateItemsPriceViewModel
    {
        [Required(ErrorMessage =ErrorMessageCommon.RequiredErrorMessage)]
        public int multiplier { get; set; }
    }
}
