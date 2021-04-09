using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.EntityFrameWork.Model
{
    public class Board
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CagegoryId { get; set; }

        public string Name { get; set; }

        public BrokerSource BrokerSource { get; set; }

        public BoardCategory BoardCategory { get; set; }
       
    }

    public enum BrokerSource
    {
        EASTMONEY,
        TENCENT
    }

    public enum BoardCategory
    {
        Region,
        Industry,
        Concept
    }
}
