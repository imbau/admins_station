using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace admins_station.Models
{
    public class calculate_rents_amt
    {  
        tool Tool = new tool();
      //  MemberContext MemberData = new MemberContext(ConnectionString);
        //退租租金計算
        public Dictionary<string, string>  calculate_stop_rents_amt(Dictionary<string, string>  parms, MySqlConnection connection)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            //初始化會員物件
            MemberContext memberobj = new MemberContext();
            //取得會員資料
            List<MemberItem> memberdata = memberobj.GetMember("member_no", parms["member_no"],1, connection);

            //取得租金費率
            Rate StationRate = get_price_plan(Int32.Parse(memberdata[0].station_no), 1,connection);
          
            return result;
	}
        //首期租金
        public Dictionary<string, string> initial_deposit_rent(int station_no,String demonth_start_date,int member_attr,int period, MySqlConnection connection)
        {
            Rate StationRate = get_price_plan(station_no,1,connection);         
            String Startdate = null, Enddate=null;
            String demonth_end_date = null;
            int period_month_bias;
            int[] Rent = new int[2];
            int demonth_days = 0;
            int demonth_amt = 0;
            Double amt1 = 0;
            int amonth_days=0;
            int amonth_amt = 0;
            int amonth_months = 0;
            Double amt2 = 0;
            Dictionary<string, string> amt_arr = new Dictionary<string, string>();
           
            // 當傳入會員開始日為當月第一天時，視為一個足月，否則算不足月            
            if (DateTime.Parse(demonth_start_date).Day==1)//判斷起始日是否當月第一天，第一天就算足月
            {
                //如果足月就不算不足月租金
                //不足月起始日跟結束為同一天
                Startdate = demonth_start_date;                                                  
                demonth_end_date = demonth_start_date;                                           
            }
            else
            {
                // 不足月起租日為下個月一號
                Startdate = new DateTime(DateTime.Parse(demonth_start_date).AddMonths(1).Year, DateTime.Parse(demonth_start_date).AddMonths(1).Month, 1).AddDays(0).ToString("yyyy/MM/dd");
                //不足月結束日為起租日當月月底
                demonth_end_date = new DateTime(DateTime.Parse(demonth_start_date).AddMonths(1).Year, DateTime.Parse(demonth_start_date).AddMonths(1).Month, 1).AddDays(-1).ToString("yyyy/MM/dd");
            }

            //繳期直接算
            period_month_bias = (period < 1) ? 1 : period;
            //足月月底，結束看繳期去加
            Enddate = new DateTime(DateTime.Parse(Startdate).AddMonths(period_month_bias).Year, DateTime.Parse(Startdate).AddMonths(period_month_bias).Month, 1).AddDays(-1).ToString("yyyy/MM/dd");
       
          


            if (member_attr == 250)
            {
                Rent[0] = 0;   // 不足月 	(VIP)
                Rent[1] = 0;  // 足月		(VIP)
            } 
            else
             {
                // 不足月計算			 
                // 當傳入不足月開始日為當月第一天時，視為一個足月
                // 不足月天數，不足月用月繳金額去算
                demonth_days = (new TimeSpan(DateTime.Parse(demonth_end_date).Ticks - DateTime.Parse(demonth_start_date).Ticks).Days)+1;
                //月繳租金費率
                demonth_amt = StationRate.price_plan[1 + "_" + member_attr];
                
                //不足月租金
                amt1 = Math.Round((Double)((demonth_amt/ DateTime.DaysInMonth(DateTime.Parse(demonth_end_date).Year, DateTime.Parse(demonth_end_date).Month))* demonth_days), 0, MidpointRounding.AwayFromZero);
                amt1 = (amt1 > demonth_amt) ? demonth_amt: amt1;
             
                // 足月計算			  
                amonth_days = new TimeSpan(DateTime.Parse(Enddate).Ticks - DateTime.Parse(Startdate).Ticks).Days+1;
                //繳期租金費率
                amonth_amt = StationRate.price_plan[period+ "_" + member_attr];
			
			
			    // 第三版: 繳期直接算
			    amonth_months = period;
			    amt2 = Math.Round((Double)(amonth_amt * amonth_months / period),0, MidpointRounding.AwayFromZero); 
			    amt2 = (amt2 > amonth_amt) ? amonth_amt: amt2;


            }

            // 回傳參數
            amt_arr.Add("rents_deposit", StationRate.price_plan[member_attr + "_" + 0].ToString());// 押金
            amt_arr.Add("demonth_start_date", demonth_start_date);// 不足月起租日
            amt_arr.Add("demonth_end_date", demonth_end_date);// 不足月結束日
            amt_arr.Add("start_date", Startdate);// 足月起租日
            amt_arr.Add("end_date", Enddate);// 足月結束日
            amt_arr.Add("rents_amt1", amt1.ToString());// 不足月：租金
            amt_arr.Add("demonth_amt", demonth_amt.ToString());// 不足月：繳期費率
            amt_arr.Add("demonth_days", demonth_days.ToString());// 不足月：天數
            amt_arr.Add("demonth_days_total", DateTime.DaysInMonth(DateTime.Parse(demonth_end_date).Year, DateTime.Parse(demonth_end_date).Month).ToString());//  不足月：總天數
            amt_arr.Add("rents_amt2", amt2.ToString());// 足月：租金
            amt_arr.Add("amonth_amt", amonth_amt.ToString());// 足月：繳期費率
            amt_arr.Add("amonth_days", amonth_days.ToString());// 足月：天數          
            amt_arr.Add("amonth_months", amonth_months.ToString());// 足月：月數
            amt_arr.Add("amonth_months_total", period.ToString());// 足月：總月數
            return amt_arr;
        }

        

        //取得目前廠站費率物件
        public Rate get_price_plan(int station_no, int tx_type, MySqlConnection connection)
        {
            Rate Rateitem = null;           
            MySqlCommand cmd = null;
            cmd = new MySqlCommand("SELECT * FROM master_db.tx_price_plan WHERE " +
                "start_time <='" + Tool.get_current_time() + "' and " +
                "valid_time >'" + Tool.get_current_time() + "' and " +
                "station_no='" + station_no + "' and " +
                "tx_type='" + tx_type + "'", connection);
               
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Rateitem = new Rate()
                        {
                        tx_price_plan_id = Convert.ToInt32(reader["tx_price_plan_id"]),
                        tx_type = Convert.ToInt32(reader["tx_type"]),
                        station_no = Convert.ToInt32(reader["station_no"]),
                        remarks = reader["remarks"].ToString(),
                        price_plan = JsonConvert.DeserializeObject<Dictionary<string, int>>(reader["price_plan"].ToString().Replace("\"", "")),
                        start_time = reader["start_time"].ToString(),
                        valid_time = reader["valid_time"].ToString(),
                        create_time = reader["create_time"].ToString()
                        };
                       
                }
            }
            return Rateitem;
                        
        }
    }
}
