using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//mysql数据库的程序集
using MySql.Data;
using MySql.Data.MySqlClient;

//页面生成
using HtmlAgilityPack;

namespace GeneratePage
{
    class GenerateIndexData
    {
        static string sConn = SiteGlobelDefine.DBConnectstring;

        public void FuncGenerateData(string strfilename)
        {
            //生成文档
            string siteRecordfilepath = strfilename; //当前的路径
            HtmlDocument hdom = new HtmlDocument();
            hdom.Load(siteRecordfilepath, Encoding.UTF8);
            HtmlNode rootNode = hdom.DocumentNode;

            MySqlConnection conn = new MySqlConnection(sConn);
            conn.Open();

            string query = "SELECT * FROM dballwithme.datainfo inner join dballwithme.userinfo on datainfo.dataauthor=userinfo.UserId ";
            string qConditon = "ORDER BY datatime DESC";
            query = query + qConditon;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            HtmlNodeCollection ArticleRankList = rootNode.SelectNodes("//div[@class='DataArticleListContent']//ul//li");
            foreach (HtmlNode nodetemp in ArticleRankList)
            {
                reader.Read();
                HtmlNode ContentT = nodetemp.SelectSingleNode(".//div[@class='DataArticleListT']"); //标题                
                ContentT.InnerHtml = reader[1].ToString();

                HtmlNode ContentTime = nodetemp.SelectSingleNode(".//div[@class='DataArticleListTime']"); //时间
                ContentTime.InnerHtml = reader[5].ToString();

                HtmlNode ContentImg = nodetemp.SelectSingleNode(".//div[@class='DataArticleListImg']"); //图片
                ContentImg.Element("img").SetAttributeValue("src", "PageData/PageDataImg/" + reader[3].ToString());

                HtmlNode ContentCC = nodetemp.SelectSingleNode(".//div[@class='DataArticleListCC']"); //内容
                ContentCC.Element("p").InnerHtml = reader[2].ToString();

                HtmlNode ContentInfo = nodetemp.SelectSingleNode(".//div[@class='DataArticleListInfo']"); //标题                
                ContentInfo.InnerHtml = reader[15].ToString() + "," + reader[11].ToString() + "," + reader[4].ToString();

            }
            reader.Close();


            hdom.Save(siteRecordfilepath, Encoding.UTF8);
            conn.Close();


        }


    }
}
