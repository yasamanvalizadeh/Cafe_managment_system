using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.ViewModel.Error
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } 
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
