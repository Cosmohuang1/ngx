using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.EntityFrameWork.Model
{
    public class BoardToStocks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        public string CagegoryId { get; set; }

        public string StockCode { get; set; }
    }
}
