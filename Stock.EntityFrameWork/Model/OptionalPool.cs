using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.EntityFrameWork.Model
{
    public  class OptionalPool
    {        
        public int CategoryId { get; set; }

        [MaxLength(6)]
        public string Code { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
