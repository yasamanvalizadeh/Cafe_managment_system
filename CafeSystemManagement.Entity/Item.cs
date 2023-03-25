using CafeManagementSystem.Common.CommonEntityProps;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeSystemManagement.Entities
{
    public class Item : CommonEntityProps
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        [Column(TypeName = "Money")]
        public decimal UnitPrice { get; set; } 
        [Timestamp]
        public byte[] RowVersion { get; set; } 
        public int TenantId { get; set; }
        public int CategoryId { get; set; }
        //relationship
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual Category Category { get; set; }
    }
}