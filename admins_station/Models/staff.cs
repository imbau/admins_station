using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admins_station.Models
{
    public class Staffobject
    {
        public int staff_id { get; set; }//資料庫編號
        public String empolyee_no { get; set; }//登入帳號'
        public String pswd { get; set; }//員工密碼
        public String mobile_no { get; set; }//手機號碼
        public String tel { get; set; }//'市話'  
        public String email { get; set; }//'電子信箱'  
        public String addr { get; set; }//'住家地址'  
        public String status { get; set; }//'狀態, 1:正常, 2:暫時停權, 3:永久停權',
        public int is_first { get; set; }//'是否初次login ->需要更改密碼
        public String permission { get; set; }//'Admin/staff
        public String update_time { get; set; }//修改時間'   
    }
}
