using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using admins_station.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;


namespace admins_station.Controllers
{
    [ApiController]
    public class staffController : ControllerBase
    {
        private MySqlConnection connection;
        private String AESpassword;
        private IOptions<EnvironmentVariable> EnvironmentVariablesettings;

        public staffController(IOptions<EnvironmentVariable> settings)
        {
            this.EnvironmentVariablesettings = settings;
            connection = new MySqlConnection(EnvironmentVariablesettings.Value.MysqlDefaultConnection);
            connection.Open();
            AESpassword = EnvironmentVariablesettings.Value.AESpassword;
        }
        [HttpPost("[controller]/login", Name = "stafflogin")]
        public String login(String empolyee_no, String password)
        {
           staffContext context = HttpContext.RequestServices.GetService
           (typeof(admins_station.Models.staffContext)) as staffContext;
           return JsonConvert.SerializeObject(context.login(empolyee_no, password,connection, AESpassword));
            
        }
        [HttpPut("[controller]/{empolyee_no}", Name = "staffupdatePassword")]
        public String updatePassword(String empolyee_no, String newpassword)
        {
            staffContext context = HttpContext.RequestServices.GetService
            (typeof(admins_station.Models.staffContext)) as staffContext;
            return JsonConvert.SerializeObject(context.updatePassword(empolyee_no, newpassword, connection));
        }
        [HttpPost("[controller]", Name = "staffInstert")]
        public String Instert(String staffdata)
        {
            staffContext context = HttpContext.RequestServices.GetService
            (typeof(admins_station.Models.staffContext)) as staffContext;
            return JsonConvert.SerializeObject(context.Instert(staffdata, connection, AESpassword));
        }

    }
}