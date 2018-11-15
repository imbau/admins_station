using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admins_station.Models
{
    public class MemberItem
    {
        public String member_id { get; set; }//'身份證號/統一編號'
        public String member_no { get; set; }//'會員編號'
        public String station_no { get; set; }//場站編號
        public String member_name { get; set; }//姓名
        public String member_nick_name { get; set; }//暱稱
        public String member_attr { get; set; }//身份別, 1:一般,2:里民,3:VIP
        public String member_type { get; set; }//1:停車會員,2:車位業主會員,3:兩者皆是,4:新北交通局
        public String member_rank { get; set; }//1:一般會員,2:銀級會員,3:金級會員,4:鑽石會員
        public String contract_no { get; set; }//合約號碼
        public String card_id { get; set; }//控制器卡號
        public String login_id { get; set; }//登入ID
        public String passwd { get; set; }//登入密碼
        public String bank_name { get; set; }//銀行名稱
        public String bank_no { get; set; }//銀行名稱
        public String account_no { get; set; }//銀行帳號
        public String account_name { get; set; }//戶名
        public String mobile_no { get; set; }//手機號碼
        public String tel_o { get; set; }//公司電話
        public String tel_h { get; set; }//住宅電話
        public String fax { get; set; }//'傳真',
        public String email { get; set; }//'電子信箱',
        public String line_id { get; set; }//'LINE ID',
        public String wechart { get; set; }//'微信ID',
        public String qq { get; set; }//'qq ID',
        public String skype { get; set; }//'skype ID',
        public String zip_code { get; set; }//'郵遞區號',
        public String city { get; set; }//'城市',
        public String district { get; set; }//'區名',
        public String addr { get; set; }//'地址',
        public String lpr { get; set; }//'車牌號碼',
        public String locked { get; set; }//'鎖車碼:0:放行, 1:不准出場',
        public String etag { get; set; }//'eTag碼',
        public String fee_period { get; set; }//'首期繳期(1:月繳,2:雙月繳,3:季繳,6:半年繳,12:年繳)',
        public String start_date { get; set; }//'啟始日'
        public String end_date { get; set; }//'到期日',
        public String amt { get; set; }//'租金',
        public String rent_amt { get; set; }//'月租金額',
        public String points { get; set; }//'點數',
        public String regist_date { get; set; }//'加入會員日期',
        public String remarks { get; set; }//'註記事項',
        public String operate_time { get; set; }//'修改時間',
        public String update_time { get; set; }//'檔案修改時間',
        public String seqno { get; set; }//'會員總序號',
        public String fee_code { get; set; }//'期別碼(201605-1)',
        public String payed_date { get; set; }//'最近繳款日',
        public String park_time { get; set; }//'時段代碼表',
        public String member_company_no { get; set; }//'會員統一編號',
        public String company_no { get; set; }//'發票統一編號',
        public String suspended { get; set; }//'0:正常,1:停權',
        public String proved { get; set; }//'0:未驗證,1:初級驗證,2:二級驗證',
        public String demonth_start_date { get; set; }//'不足月開始日期',
        public String demonth_end_date { get; set; }//'不足月結束日期',
        public String rent_start_date { get; set; }//'足月開始日期',
        public String rent_end_date { get; set; }//足月結束日期',
        public String fee_period1 { get; set; }//首期繳期(1:月繳,2:雙月繳,3:季繳,6:半年繳,12:年繳)',
        public String deposit { get; set; }//押金',
        public String amt_tot { get; set; }//租金總計',
        public String amt_accrued { get; set; }//租金應計',
        public String valid_time { get; set; }//有效期限',
        public String refund_transfer_id { get; set; }//'轉租來源編號 (member_refund_id)'
    }
}
