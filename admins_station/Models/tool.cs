using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admins_station.Models
{
    public class tool
    {
        public String get_current_time()//取得目前時間 
        {
            DateTime CurrTime = DateTime.Now;//取得目前時間  
            return CurrTime.ToString("yyyy/MM/dd");
        }
    }
}
