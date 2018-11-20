using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace admins_station.Models
{
    public class staffContext
    {
        public Dictionary<string, object> Getallstaff(MySqlConnection connection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Dictionary<string, object> staffmysqlSerializedata = new Dictionary<string, object>();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            MySqlCommand cmd = null;
            cmd = new MySqlCommand("SELECT * FROM `staff` WHERE 1", connection);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    try
                    {
                        //將資料庫資料轉成物件
                        staffmysqlSerializedata = mysqlSerialize(reader);
                        list.Add(staffmysqlSerializedata);                        
                    }
                    catch (Exception ex)
                    {
                        result.Add("resultCode", "1");
                        result.Add("message", "資料庫資料轉換發生錯誤");
                        return result;
                    }
                }
                result.Add("resultCode", "0");
                result.Add("staffobject", list);
                // staffobject.Add("staffobject", Aes.AesEncrypt(JsonConvert.SerializeObject(staffmysqlSerializedata), aespassword));
                result.Add("message", "取得操作人員成功!!");
                return result;


            }            
        }
        public Dictionary<string, object> login(String empolyee_no, String password,MySqlConnection connection, String aespassword)
        {
            Dictionary<string, object> staffobject = new Dictionary<string, object>();
            Dictionary<string, object> staffmysqlSerializedata  = new Dictionary<string, object>();
            MySqlCommand cmd = null;
            try
            {
                cmd = new MySqlCommand("SELECT * FROM `staff` WHERE `empolyee_no` = '" + empolyee_no + "'  and `pswd`= '" + password + "'", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())//驗證成功
                    {
                        try
                        {
                           //將資料庫資料轉成物件
                           staffmysqlSerializedata = mysqlSerialize(reader);
                        }
                        catch (Exception ex)
                        {
                            staffobject.Add("resultCode", "4");
                            staffobject.Add("message", "資料庫資料轉換發生錯誤");
                            return staffobject;
                        }
                        
                        if (!bool.Parse(staffmysqlSerializedata["is_first"].ToString()))//首次登入需要更改密碼
                        {
                            staffobject.Add("resultCode", "1");
                            staffobject.Add("message", "第一次登入成功需要修改密碼!!");
                            return staffobject;
                        }
                        else
                        {

                            staffobject.Add("resultCode", "0");
                            staffobject.Add("staffobject", staffmysqlSerializedata);
                            // staffobject.Add("staffobject", Aes.AesEncrypt(JsonConvert.SerializeObject(staffmysqlSerializedata), aespassword));
                            staffobject.Add("message", "登入成功!!");
                            return staffobject;
                        }
                    }
                    else
                    {
                        staffobject.Add("resultCode", "3");
                        staffobject.Add("message", "帳號或密碼有誤");
                        return staffobject;
                    }
                }
            }
            catch (Exception ex)
            {
                staffobject.Add("resultCode", "2");
                staffobject.Add("message", "資料庫查詢資料發生錯誤");
                return staffobject;
            }
          
         
        }
        public Dictionary<string, string> deleteStaff(String empolyee_no, MySqlConnection connection)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            MySqlCommand cmd = null;
            String staff_id = null;          
            cmd = new MySqlCommand("SELECT staff_id FROM `staff` WHERE `empolyee_no` = '" + empolyee_no + "'", connection);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())//驗證成功
                {
                    try
                    {
                        staff_id = reader["staff_id"].ToString();
                        cmd.Parameters.Clear();                      
                        reader.Close();
                        //刪除會員案場連結 
                        cmd = new MySqlCommand("DELETE FROM `staff_sites_mapping` WHERE `staff_id`='" + staff_id + "'", connection);
                        cmd.ExecuteNonQuery();//刪除成功
                    }
                    catch (Exception ex)
                    {
                        result.Add("resultCode", "3");
                        result.Add("message", "資料庫刪除案場連結發生錯誤");
                        return result;
                    }
                    try
                    {
                        cmd.Parameters.Clear();
                        //刪除員工
                        cmd = new MySqlCommand("DELETE FROM `staff` WHERE `empolyee_no` = '" + empolyee_no + "'", connection);
                        if (cmd.ExecuteNonQuery() > 0)//刪除成功
                        {
                            result.Add("resultCode", "0");
                            result.Add("message", " 會員刪除成功!!");
                            return result;
                        }
                        else
                        {
                            result.Add("resultCode", "1");
                            result.Add("message", "員工刪除失敗!!");
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Add("resultCode", "2");
                        result.Add("message", "資料庫刪除員工發生錯誤");
                        return result;
                    }
                       
                    }
                else//查無此帳號
                {
                    result.Add("resultCode", "4");
                    result.Add("message", "此帳號不存在，無法刪除!!");
                    return result;
                }
            }            
        }
        
        public Dictionary<string, string> editstaff(String staffId, String staffdata, MySqlConnection connection)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            return result;

        }
        public Dictionary<string, string> updatePassword(String empolyee_no, String newpassword, MySqlConnection connection)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            MySqlCommand cmd = null;
            try
            {
                cmd = new MySqlCommand("UPDATE `staff` SET `pswd` = '" + newpassword + "' WHERE `empolyee_no`= '" + empolyee_no + "'", connection);
                if (cmd.ExecuteNonQuery() > 0)//成功
                {
                    //更新是否第一次登入的的狀態
                    cmd = new MySqlCommand("UPDATE `staff` SET `is_first` = '1' WHERE `empolyee_no`= '" + empolyee_no + "'", connection);
                    cmd.ExecuteNonQuery();
                    result.Add("resultCode", "0");
                    result.Add("message", "密碼更新成功!!");
                    return result;
                }
                else
                {
                    result.Add("resultCode", "1");
                    result.Add("message", "密碼更新失敗!!");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Add("resultCode", "2");
                result.Add("message", "資料庫修改密碼發生錯誤");
                return result;
            }
        }
        public Dictionary<string, string> Instert(String staffdata,MySqlConnection connection, String aespassword)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, string> staffDictionarydata = new Dictionary<string, string>();
            MySqlCommand insertcmd = null, Selectcmd = null;
            String staffid = null;
            //解密
            // String staffjsondata=Aes.AesDecrypt(staffdata, aespassword);
            try
            {
                //josn轉Dictionary物件
                staffDictionarydata = JsonConvert.DeserializeObject<Dictionary<string, string>>(staffdata);
            }
            catch (Exception ex)
            {
                result.Add("resultCode", "5");
                result.Add("message", "josn格式有誤!!");
                return result;
            } 
            //查詢是否有重複帳號
            Selectcmd = new MySqlCommand("SELECT * FROM `staff` WHERE `empolyee_no` = '" + staffDictionarydata["empolyee_no"].ToString() + "'", connection);
            using (var reader = Selectcmd.ExecuteReader())
            {
                if (reader.Read())//驗證成功
                {
                    result.Add("resultCode", "6");
                    result.Add("message", "帳號重複!!");
                    return result;

                }
                else//沒有重複
                {
                    try
                    {
                        Selectcmd.Parameters.Clear();
                        reader.Close();
                        String MYsqlstring = "INSERT INTO `staff`(`empolyee_no`, `pswd`, `mobile_no`, `tel`, `email`, `addr`, `status`, `is_first`, `permission`, `update_time`) VALUES (";
                        foreach (KeyValuePair<string, string> item in staffDictionarydata)
                        {
                            if (item.Key == "sites")
                                continue;
                            if (item.Key == "empolyee_no")
                                MYsqlstring += "'" + item.Value + "'";
                            else
                                MYsqlstring += ",'" + item.Value + "'";
                        }
                        MYsqlstring += ")";
                        //新增員工
                        insertcmd = new MySqlCommand(MYsqlstring, connection);
                        if (insertcmd.ExecuteNonQuery() > 0)//成功
                        {
                            insertcmd.Parameters.Clear();
                            //讀取已新增的id
                            Selectcmd = new MySqlCommand("SELECT staff_id FROM `staff` WHERE `empolyee_no` = '" + staffDictionarydata["empolyee_no"].ToString() + "'", connection);
                            using (var staff_idreader = Selectcmd.ExecuteReader())
                            {
                                if (staff_idreader.Read())//驗證成功
                                {
                                    staffid = staff_idreader["staff_id"].ToString();
                                    Selectcmd.Parameters.Clear();
                                    staff_idreader.Close();
                                    try
                                    {
                                        String[] Sites = staffDictionarydata["sites"].ToString().Split(",");
                                        //新增staff_sites_mapping
                                        for (int Sitesindex = 0; Sitesindex < Sites.Length; Sitesindex++)
                                        {
                                            insertcmd = new MySqlCommand("INSERT INTO `staff_sites_mapping`(`staff_id`, `site_id`) VALUES ('" + staffid + "','" + Sites[Sitesindex] + "')", connection);
                                            insertcmd.ExecuteNonQuery();
                                        }
                                        result.Add("resultCode", "0");
                                        result.Add("message", "員工資料新增成功!!");
                                    }
                                    catch (Exception ex)
                                    {
                                        result.Add("resultCode", "4");
                                        result.Add("message", "資料庫新增案場發生錯誤!!");
                                        return result;
                                    }
                                }
                                else
                                {
                                    result.Add("resultCode", "3");
                                    result.Add("message", "驗證錯誤!!");
                                    return result;
                                }
                            }                            
                        }
                        else
                        {
                            result.Add("resultCode", "1");
                            result.Add("message", "員工資料新增失敗!!");
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        result.Add("resultCode", "2");
                        result.Add("message", "資料庫新增會員發生錯誤!!");
                        return result;
                    }
                }
             }
        }
        public Dictionary<string, object> mysqlSerialize(MySqlDataReader reader)
        {
            var results = new Dictionary<string, object>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                results.Add(reader.GetName(i), (reader.IsDBNull(i) ? null : reader.GetValue(i)));
            }
            return results;
        }
    }

}
