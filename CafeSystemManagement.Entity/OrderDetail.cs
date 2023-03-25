using CafeManagementSystem.Common.CommonEntityProps;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeSystemManagement.Entities
{
    public class OrderDetail  : CommonEntityProps
    {  
        public int OrderDetailId { get; set; }

        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }
        public int Qty { get; set; } 


        //foreign keys 
        public int OrderId { get; set; }
        public int ItemId { get; set; }

        //relationShip 
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}

