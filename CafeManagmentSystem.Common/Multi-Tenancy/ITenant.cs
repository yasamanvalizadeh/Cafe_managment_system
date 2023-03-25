using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.Common.Multi_Tenancy
{
    public interface ITenant
    {
        public int TenantId { get; set; }
    }
}
