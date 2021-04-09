using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.EntityFrameWork.Model
{

    public class StockComment
    {
        /// <summary>
        /// [{"TDate":"2021-03-23T00:00:00","Code":"600600","Name":"青岛啤酒","New":79.8,"ChangePercent":1.26,"PERation":49.45,"TurnoverRate":0.89,"ZLCB":79.4987631003039,"JGCYD":0.2515756,"JGCYDType":"中度控盘","ZLCB20R":78.4962915978762,"ZLCB60R":89.9464426533132,"FLOWINXL":31421386.0,"FLOWOUTXL":26599353.0,"FLOWINL":93937478.0,"FLOWOUTL":88977140.0,"ZLJLR":9782371.0,"Market":"2","TotalScore":69.57,"RankingUp":283.0,"Ranking":832.0,"Focus":83.2}]
        /// </summary>

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        private DateTime tdate;
        public DateTime TDate
        {
            get
            {
                return new DateTime(tdate.Year, tdate.Month, tdate.Day);
            }
            set { tdate = value; }
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ChangePercent { get; set; }
        public string PERation { get; set; }
        public string TurnoverRate { get; set; }
        /// <summary>
        /// 主力成本
        /// </summary>
        public string ZLCB { get; set; }
        public string ZLCB20R { get; set; }
        public string ZLCB60R { get; set; }
        /// <summary>
        /// 机构参与度 /100
        /// </summary>
        public string JGCYD { get; set; }
        public string JGCYDType { get; set; }
        public string FLOWINXL { get; set; }
        public string FLOWOUTXL { get; set; }
        public string FLOWINL { get; set; }
        public string FLOWOUTL { get; set; }
        /// <summary>
        /// 主力净流入
        /// </summary>
        public string ZLJLR { get; set; }
        public string Market { get; set; }
        public string TotalScore { get; set; }
        public string RankingUp { get; set; }
        public string Ranking { get; set; }
        public string Focus { get; set; }
    }
}
