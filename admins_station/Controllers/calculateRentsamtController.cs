using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using admins_station.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace admins_station.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class calculateRentsamtController : ControllerBase
    {
        private readonly MemberContext _context;
        private MySqlConnection connection;
        private IOptions<EnvironmentVariable> MysqlSettings;
        public calculateRentsamtController(IOptions<EnvironmentVariable> settings)
        {
            this.MysqlSettings = settings;
            connection = new MySqlConnection(MysqlSettings.Value.MysqlDefaultConnection);
            connection.Open();
        }

        [HttpGet("", Name = "getinitialdepositrent")]
        //取得首期租金
        public String get_initial_deposit_rent(int station_no, String demonth_start_date, int member_attr, int period)
        {
            try
            {
                calculate_rents_amt context = HttpContext.RequestServices.GetService
                (typeof(admins_station.Models.calculate_rents_amt)) as calculate_rents_amt;
                return JsonConvert.SerializeObject(context.initial_deposit_rent(station_no, demonth_start_date, member_attr, period, connection));
            }
            catch (Exception ex)
            {
                return "取得失敗";
            }
        }
        [HttpGet("", Name = "getstoprentsamt")]
        //取得退租租金
        public String get_stop_rents_amt(String start_date, String member_no)
        {
            try
            {
                calculate_rents_amt context = HttpContext.RequestServices.GetService
                (typeof(admins_station.Models.calculate_rents_amt)) as calculate_rents_amt;
                Dictionary<string, string> parms = new Dictionary<string, string>();
                parms.Add("start_date", start_date);// 退租日期
                parms.Add("member_no", member_no);// 會員代號
                return JsonConvert.SerializeObject(context.calculate_stop_rents_amt(parms, connection));
            }
            catch (Exception ex)
            {
                return "取得失敗";
            }

        }
    }

}