using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.EntityFrameWork.Model
{
    public  class CustomCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int  UserId { get; set; }

        [MaxLength(16), Required]
        public string Name { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public DateTime UpdateDateTime { get; set; } = DateTime.Now;

        public bool IsActived { get; set; } = true;
    }
}
