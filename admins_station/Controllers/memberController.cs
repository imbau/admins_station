using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using admins_station.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace admins_station.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class memberController : ControllerBase
    {
        private MySqlConnection connection;
        private IOptions<EnvironmentVariable> MysqlSettings;

        public memberController(IOptions<EnvironmentVariable> settings)
        {
            this.MysqlSettings = settings;
            connection= new MySqlConnection(MysqlSettings.Value.MysqlDefaultConnection);
            connection.Open();
        }
      
      

        [HttpGet("", Name = "Getallmember")]
        public String Getallmember()
        {
            try
            {
                MemberContext context = HttpContext.RequestServices.GetService
                (typeof(admins_station.Models.MemberContext)) as MemberContext;
                return JsonConvert.SerializeObject(context.GetMember("null","null", 0, connection));
            }
            catch (Exception ex)
            {
                return "取得失敗";
            }

        }


        [HttpGet("", Name = "Getmemberbycolumn")]
        public String Getmemberbycolumn( String column,String value)
        {
            try
            {
                MemberContext context = HttpContext.RequestServices.GetService
                (typeof(admins_station.Models.MemberContext)) as MemberContext;
                return JsonConvert.SerializeObject(context.GetMember(column, value,1,connection));
            }
            catch (Exception ex)
            {
                return "資料庫沒有"+ column+"欄位";
            }
            
        }
     

        // PUT api/Member/5 修改欄位值
        [HttpPut("", Name = "PutTodo")]
        public String Putmemberbycolum(String id,String column, String value)
        {
            try
            {
                MemberContext context = HttpContext.RequestServices.GetService
                (typeof(admins_station.Models.MemberContext)) as MemberContext;
                if (context.updateMember(id,column, value, connection) > 0)
                    return "會員資料更新成功";
                else
                    return "會員資料更新失敗";
            }
            catch (Exception ex)
            {
                return "資料庫沒有" + column + "欄位";
            }
        }
        // DELETE api/Member/5
        [HttpDelete("", Name = "DELETETodo")]
        public String  Deletememberbycolumn(String column, String value)
        {
            try
            {
                MemberContext context = HttpContext.RequestServices.GetService
                (typeof(admins_station.Models.MemberContext)) as MemberContext;
                if (context.deleteMember(column, value, connection) >0)
                    return "會員刪除成功";
                else
                    return "會員刪除失敗";
            }
            catch (Exception ex)
            {
                return "資料庫操作失敗";
            }

        }



    }
}