using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.EntityFrameWork.Model
{
    public class StockEntity
    {
        [Key]
        [MaxLength(6), Required]
        public string Code { get; set; }

        [MaxLength(16), Required]
        public string Name { get; set; }

        public int Type { get; set; }
    }
}
