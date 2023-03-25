using Microsoft.Azure.Management.ResourceManager.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;


namespace CafeManagmentSystem.Common.Multi_Tenancy
{
    public class Tenant : ITenant
    {
        public int TenantId { get; set; }
        public Tenant(int tenantId)
            => TenantId = tenantId;
    }
}
