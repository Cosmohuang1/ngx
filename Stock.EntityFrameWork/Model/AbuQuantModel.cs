using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.EntityFrameWork.Model
{
    public class AbuQuantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public int Rank { get; set; }
        public int Score { get; set; }
        public int FW { get; set; }
        public int KC { get; set; }
        public int BS { get; set; }
        public int MC { get; set; }
        public int MP { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
