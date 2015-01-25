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

    class GenerateIndexArticle
    {

        static string sConn = SiteGlobelDefine.DBConnectstring;

        public void FuncGenerateArticle(string strfilename)
        {
            //生成文档
            string siteRecordfilepath = strfilename; //获取当前的路径
            HtmlDocument hdom = new HtmlDocument();
            hdom.Load(siteRecordfilepath , Encoding.UTF8);
            HtmlNode rootNode = hdom.DocumentNode;

            MySqlConnection conn = new MySqlConnection(sConn);
            conn.Open();

            string query = "SELECT * FROM dballwithme.articleinfo ";
            string qConditon = "ORDER BY articletime DESC";
            query = query + qConditon;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            
            //排行            
            HtmlNodeCollection ArticleRankList = rootNode.SelectNodes("//div[@class='ArticleRank']//ul//li");
            foreach (HtmlNode nodetemp in ArticleRankList)
            {
                reader.Read();
                HtmlNode ContentT = nodetemp.SelectSingleNode(".//div[@class='ArticleRankT']"); //标题                
                ContentT.InnerHtml = reader[1].ToString();

                HtmlNode ContentTime = nodetemp.SelectSingleNode(".//div[@class='ArticleRankTime']"); //时间
                ContentTime.InnerHtml = reader[5].ToString();

                HtmlNode ContentC = nodetemp.SelectSingleNode(".//div[@class='ArticleRankC']"); //内容
                ContentC.Element("p").InnerHtml = reader[2].ToString();

                HtmlNode ContentI = nodetemp.SelectSingleNode(".//div[@class='ArticleInfo']"); //信息
                ContentI.InnerHtml = reader[4].ToString();

                
            }
            reader.Close();


            query = "SELECT * FROM dballwithme.articleinfo ";
            qConditon = "ORDER BY articlereadnum DESC";
            query = query + qConditon;
            cmd.CommandText = query;
            MySqlDataReader readerC = cmd.ExecuteReader();
            //正文内容加载
            HtmlNodeCollection ArticleAreaList = rootNode.SelectNodes("//div[@class='ArticleArea']//ul//li");
            foreach (HtmlNode nodetemp in ArticleAreaList)
            {
                readerC.Read();

                HtmlNode ContentT = nodetemp.SelectSingleNode(".//div[@class='ArticleAreaT']"); //标题                
                ContentT.InnerHtml = readerC[1].ToString();

                HtmlNode ContentImg = nodetemp.SelectSingleNode(".//div[@class='ArticleAreaImg']"); //图片                
                ContentImg.Element("img").SetAttributeValue("src", "PageArticle/PageArticleImg/" + readerC[3].ToString());

                HtmlNode ContentC = nodetemp.SelectSingleNode(".//div[@class='ArticleAreaC']"); //内容
                ContentC.Element("p").InnerHtml = readerC[2].ToString();
                 

                HtmlNode ContentB = nodetemp.SelectSingleNode(".//div[@class='ArticleAreaShoucB']"); //button
                ContentB.InnerHtml = "";

                HtmlNode ContentM = nodetemp.SelectSingleNode(".//div[@class='ArticleAreaReadMore']"); //more
                ContentM.InnerHtml = "";

                HtmlNode ContentI = nodetemp.SelectSingleNode(".//div[@class='ArticleInfo']"); //信息
                ContentI.InnerHtml = readerC[4].ToString();

            }
            readerC.Close();

            hdom.Save(siteRecordfilepath, Encoding.UTF8);

            conn.Close();
        }
    }
}
