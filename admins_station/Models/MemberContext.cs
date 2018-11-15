using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;


namespace admins_station.Models
{

    public class MemberContext
    {
        public int updateMember(String id, String column, String value, MySqlConnection connection)
        {           
            MySqlCommand cmd = null;
            cmd = new MySqlCommand("UPDATE master_db.members  set " + column + " = '" + value + "' where member_no = '" + id + "'", connection);
            int count = cmd.ExecuteNonQuery();
            return count;

        }
        public int deleteMember(String column, String value, MySqlConnection connection)
        {
            MySqlCommand cmd = null;
            cmd = new MySqlCommand("DELETE FROM master_db.members where " + column + "='" + value + "'", connection);
            int count = cmd.ExecuteNonQuery();
            return count;
        }

        //取得某個會員資料，依據傳入的欄位當key
        public List<MemberItem> GetMember(String column, String value, int type, MySqlConnection connection)
        {
            List<MemberItem> list = new List<MemberItem>();
            MySqlCommand cmd = null;
            if (type == 0)//不帶條件查詢
                cmd = new MySqlCommand("SELECT * FROM master_db.members", connection);
            else
                cmd = new MySqlCommand("SELECT * FROM master_db.members where " + column + "='" + value + "'", connection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new MemberItem()
                    {
                        member_id = reader["member_id"].ToString(),
                        member_no = reader["member_no"].ToString(),
                        station_no = reader["station_no"].ToString(),
                        member_name = reader["member_name"].ToString(),
                        member_nick_name = reader["member_nick_name"].ToString(),
                        member_attr = reader["member_attr"].ToString(),
                        member_type = reader["member_type"].ToString(),
                        member_rank = reader["member_rank"].ToString(),
                        contract_no = reader["contract_no"].ToString(),
                        card_id = reader["card_id"].ToString(),
                        login_id = reader["login_id"].ToString(),
                        passwd = reader["passwd"].ToString(),
                        bank_name = reader["bank_name"].ToString(),
                        bank_no = reader["bank_no"].ToString(),
                        account_no = reader["account_no"].ToString(),
                        account_name = reader["account_name"].ToString(),
                        mobile_no = reader["mobile_no"].ToString(),
                        tel_o = reader["tel_o"].ToString(),
                        tel_h = reader["tel_h"].ToString(),
                        fax = reader["fax"].ToString(),
                        email = reader["email"].ToString(),
                        line_id = reader["line_id"].ToString(),
                        wechart = reader["wechart"].ToString(),
                        qq = reader["qq"].ToString(),
                        skype = reader["skype"].ToString(),
                        zip_code = reader["zip_code"].ToString(),
                        city = reader["city"].ToString(),
                        district = reader["district"].ToString(),
                        addr = reader["addr"].ToString(),
                        lpr = reader["lpr"].ToString(),
                        locked = reader["locked"].ToString(),
                        etag = reader["etag"].ToString(),
                        fee_period = reader["fee_period"].ToString(),
                        start_date = reader["start_date"].ToString(),
                        end_date = reader["end_date"].ToString(),
                        amt = reader["amt"].ToString(),
                        rent_amt = reader["rent_amt"].ToString(),
                        points = reader["points"].ToString(),
                        regist_date = reader["regist_date"].ToString(),
                        remarks = reader["remarks"].ToString(),
                        operate_time = reader["operate_time"].ToString(),
                        update_time = reader["update_time"].ToString(),
                        seqno = reader["seqno"].ToString(),
                        fee_code = reader["fee_code"].ToString(),
                        payed_date = reader["payed_date"].ToString(),
                        park_time = reader["park_time"].ToString(),
                        member_company_no = reader["member_company_no"].ToString(),
                        company_no = reader["company_no"].ToString(),
                        suspended = reader["suspended"].ToString(),
                        proved = reader["proved"].ToString(),
                        demonth_start_date = reader["demonth_start_date"].ToString(),
                        demonth_end_date = reader["demonth_end_date"].ToString(),
                        rent_start_date = reader["rent_start_date"].ToString(),
                        rent_end_date = reader["rent_end_date"].ToString(),
                        fee_period1 = reader["fee_period1"].ToString(),
                        deposit = reader["deposit"].ToString(),
                        amt_tot = reader["amt_tot"].ToString(),
                        amt_accrued = reader["amt_accrued"].ToString(),
                        valid_time = reader["valid_time"].ToString(),
                        refund_transfer_id = reader["refund_transfer_id"].ToString()

                    });
                }
            }
            return list;
        }      
    }
}
