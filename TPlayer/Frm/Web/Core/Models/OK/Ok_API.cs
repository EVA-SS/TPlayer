using System.Collections.Generic;

namespace TPlayer.Frm.Web
{
    /// <summary>
    /// 数据详情列表
    /// </summary>
    public class Ok_API
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string page { get; set; }
        public string pagecount { get; set; }
        public string limit { get; set; }
        public int total { get; set; }
        public List<List1> list { get; set; }

        public List<Class1> @class { get; set; }
    }

    public class Class1
    {
        public string type_id { get; set; }
        public string type_name { get; set; }
    }


    public class List1
    {
        public int vod_id { get; set; }
        public int type_id { get; set; }
        public int type_id_1 { get; set; }
        public int group_id { get; set; }
        public string vod_name { get; set; }
        public string vod_sub { get; set; }
        public string vod_en { get; set; }
        public int vod_status { get; set; }
        public string vod_letter { get; set; }
        public string vod_color { get; set; }
        public string vod_tag { get; set; }
        public string vod_class { get; set; }
        public string vod_pic { get; set; }
        public string vod_pic_thumb { get; set; }
        public string vod_pic_slide { get; set; }
        public string vod_actor { get; set; }
        public string vod_director { get; set; }
        public string vod_writer { get; set; }
        public string vod_behind { get; set; }
        public string vod_blurb { get; set; }
        public string vod_remarks { get; set; }
        public string vod_pubdate { get; set; }
        public int vod_total { get; set; }
        public string vod_serial { get; set; }
        public string vod_tv { get; set; }
        public string vod_weekday { get; set; }
        public string vod_area { get; set; }
        public string vod_lang { get; set; }
        public string vod_year { get; set; }
        public string vod_version { get; set; }
        public string vod_state { get; set; }
        public string vod_author { get; set; }
        public string vod_jumpurl { get; set; }
        public string vod_tpl { get; set; }
        public string vod_tpl_play { get; set; }
        public string vod_tpl_down { get; set; }
        public int vod_isend { get; set; }
        public int vod_lock { get; set; }
        public int vod_level { get; set; }
        public int vod_copyright { get; set; }
        public int vod_points { get; set; }
        public int vod_points_play { get; set; }
        public int vod_points_down { get; set; }
        public int vod_hits { get; set; }
        public int vod_hits_day { get; set; }
        public int vod_hits_week { get; set; }
        public int vod_hits_month { get; set; }
        public string vod_duration { get; set; }
        public int vod_up { get; set; }
        public int vod_down { get; set; }
        public string vod_score { get; set; }
        public int vod_score_all { get; set; }
        public int vod_score_num { get; set; }
        public string vod_time { get; set; }
        public int vod_time_add { get; set; }
        public int vod_time_hits { get; set; }
        public int vod_time_make { get; set; }
        public int vod_trysee { get; set; }
        public int vod_douban_id { get; set; }
        public string vod_douban_score { get; set; }
        public string vod_reurl { get; set; }
        public string vod_rel_vod { get; set; }
        public string vod_rel_art { get; set; }
        public string vod_pwd { get; set; }
        public string vod_pwd_url { get; set; }
        public string vod_pwd_play { get; set; }
        public string vod_pwd_play_url { get; set; }
        public string vod_pwd_down { get; set; }
        public string vod_pwd_down_url { get; set; }
        public string vod_content { get; set; }
        public string vod_play_from { get; set; }
        public string vod_play_server { get; set; }
        public string vod_play_note { get; set; }
        public string vod_play_url { get; set; }
        public string vod_down_from { get; set; }
        public string vod_down_server { get; set; }
        public string vod_down_note { get; set; }
        public string vod_down_url { get; set; }
        public string type_name { get; set; }
    }

}
