using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admins_station.Models
{
    public class Rate
    {
        public int tx_price_plan_id { get; set; }//租金費率id
        public int tx_type { get; set; }//租金型態
        public int station_no { get; set; }//場站編號
        public String remarks { get; set; }//租金費率說明     
        public Dictionary<string, int> price_plan { get; set; }//租金費率
        public String start_time { get; set; }//開始時間
        public String valid_time { get; set; }//有效期限
        public String create_time { get; set; }//開始時間

    }
}
