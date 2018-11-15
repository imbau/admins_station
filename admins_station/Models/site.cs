using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admins_station.Models
{
  
    public class site
    {
        public int seqno { get; set; }//場站在資料庫的id
        public String station_no { get; set; }//場站id'
        public String company_no { get; set; }//公司名稱
        public String station_name { get; set; }//站名
        public String station_full_name { get; set; }//案場全名
        public String station_ip { get; set; }//場站ip
        public String hq_url { get; set; }//'場站網址'  
        public String origin_url { get; set; }//'原始網址'     
        public String park_time { get; set; }//'停車時間'  
        public String member_attr_list { get; set; }//場站的會員定義
        public String period_list { get; set; }//'費率list'
        public String period_name { get; set; }//'   
        public String rent_rate_tx_no { get; set; }//'  
        public String price_rate_tx_price_plan_id { get; set; }//'  

    }
    
}
