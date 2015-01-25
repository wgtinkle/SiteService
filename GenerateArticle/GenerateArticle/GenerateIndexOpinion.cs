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
    class GenerateIndexOpinion
    {
        static string sConn = SiteGlobelDefine.DBConnectstring;

        public void FuncGenerateOpinion(string strfilename)
        {
            //生成文档
            string siteRecordfilepath = strfilename; //获取当前的路径
            HtmlDocument hdom = new HtmlDocument();
            hdom.Load(siteRecordfilepath, Encoding.UTF8);
            HtmlNode rootNode = hdom.DocumentNode;

            MySqlConnection conn = new MySqlConnection(sConn);
            conn.Open();

            string query = "SELECT * FROM dballwithme.opinioninfo inner join dballwithme.userinfo on opinioninfo.opinionauthor=userinfo.UserId";
            string qConditon = " ORDER BY opiniontime DESC";
            query = query + qConditon;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            HtmlNodeCollection ArticleRankList = rootNode.SelectNodes("//div[@class='OpinionRank']//ul//li");
            foreach (HtmlNode nodetemp in ArticleRankList)
            {
                reader.Read();

                HtmlNode ContentImg = nodetemp.SelectSingleNode(".//div[@class='OpinionRankHead']"); //图片
                ContentImg.Element("img").SetAttributeValue("src", "FrameImg/UserHead/" + reader[15].ToString());

                HtmlNode ContentCC = nodetemp.SelectSingleNode(".//div[@class='OpinionRankTitle']"); //标题
                ContentCC.Element("p").InnerHtml = reader[1].ToString();

                HtmlNode ContentTime = nodetemp.SelectSingleNode(".//div[@class='OpinionRankTime']"); //时间
                ContentTime.InnerHtml = reader[4].ToString();

                string str = reader[1].ToString() + "#"; //title
                str = str + reader[2].ToString() + "#"; //abstract
                str = str + reader[3].ToString() + "#"; //img
                str = str + reader[4].ToString() + "#"; //time
                str = str + reader[0].ToString() + "$"; //Id
                str = str + reader[9].ToString(); //content

                HtmlNode ContentInfo = nodetemp.SelectSingleNode(".//div[@class='OpinionC']"); //内容
                ContentInfo.InnerHtml = str;

            }
            reader.Close();

            query = "SELECT * FROM dballwithme.opinioninfo inner join dballwithme.userinfo on opinioninfo.opinionauthor=userinfo.UserId ";
            qConditon = "ORDER BY opinionreadnum DESC";
            query = query + qConditon;
            cmd.CommandText = query;
            MySqlDataReader readerC = cmd.ExecuteReader();
            //正文内容加载


            HtmlNode ArticleAreaListul = rootNode.SelectSingleNode("//div[@class='OpinionContent']//ul[@class='OpinionContentUL']");


            foreach (HtmlNode nodetemp in ArticleAreaListul.ChildNodes)
            {                
                if (nodetemp.Name == "#text")
                    continue;

                readerC.Read();
                HtmlNode ContentT = nodetemp.SelectSingleNode(".//div[@class='OpinionCT']"); //标题                
                ContentT.InnerHtml = readerC[1].ToString();

                HtmlNode ContentCC = nodetemp.SelectSingleNode(".//div[@class='OpinionCC']"); //内容
                ContentCC.Element("p").InnerHtml = readerC[2].ToString();

                HtmlNode ContentImg = nodetemp.SelectSingleNode(".//div[@class='OpinionCimg']"); //图片
                ContentImg.Element("img").SetAttributeValue("src", "PageOpinion/" + readerC[3].ToString());

                string str = readerC[1].ToString() + "#"; //title
                str = str + readerC[2].ToString() + "#"; //abstract
                str = str + readerC[3].ToString() + "#"; //img
                str = str + readerC[4].ToString() + "#"; //time
                str = str + readerC[0].ToString() + "$"; //Id
                str = str + readerC[9].ToString(); //content

                HtmlNode ContentInfo = nodetemp.SelectSingleNode(".//div[@class='OpinionC']"); //内容
                ContentInfo.InnerHtml = str;

                string strClist = readerC[9].ToString();
                string []strClistArry = strClist.Split(',');

                HtmlNode OpinionList = nodetemp.SelectSingleNode(".//div[@class='OpinionCList']//ul"); //意见列表

                int nCounter = 0;
                foreach (HtmlNode nodetempli in OpinionList.ChildNodes)
                {
                    if (nodetempli.Name == "#text")
                        continue;

                    if (nCounter >= 3)
                        break;
                    string strCCC = strClistArry[nCounter];
                    strCCC = strCCC.Replace("@",",");

                    string []strCCCList = strCCC.Split(',');

                    HtmlNode ContentImgHead = nodetempli.SelectSingleNode(".//div[@class='OpinionCHead']"); //图片
                    ContentImgHead.Element("img").SetAttributeValue("src", "FrameImg/UserHead/" + strCCCList[2].ToString());

                    HtmlNode ContentCT = nodetempli.SelectSingleNode(".//div[@class='OpinionCCT']"); //内容
                    ContentCT.InnerHtml = strCCCList[3].ToString();

                    nCounter++;
                }



            }
            readerC.Close();

            hdom.Save(siteRecordfilepath, Encoding.UTF8);
            conn.Close();
        }

    }
}
