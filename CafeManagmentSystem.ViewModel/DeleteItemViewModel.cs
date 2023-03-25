using CafeManagementSystem.Common.AttributesErrorMessageCommon;
using CafeSystemManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CafeManagmentSystem.ViewModel
{
    public class DeleteItemViewModel
    {
        [HiddenInput]
        public int ItemId { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Category")]
        public string CategoryName { get; set; }  

        [Display(Name = "Item Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Timestamp , HiddenInput]
        public byte[] RowVersion { get; set; }
    }
}
