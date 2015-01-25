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
    class GenerateIndexRes
    {
        static string sConn = SiteGlobelDefine.DBConnectstring;

        public void FuncGenerateRes(string strfilename)
        {
            //生成文档
            string siteRecordfilepath = strfilename; //当前的路径
            HtmlDocument hdom = new HtmlDocument();
            hdom.Load(siteRecordfilepath, Encoding.UTF8);
            HtmlNode rootNode = hdom.DocumentNode;

            MySqlConnection conn = new MySqlConnection(sConn);
            conn.Open();

            string query = "SELECT * FROM dballwithme.resimginfo ";
            string qConditon = "ORDER BY resimgtime DESC";
            query = query + qConditon;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            //排行            
            HtmlNodeCollection ArticleRankList = rootNode.SelectNodes("//div[@class='ResRank']//ul//li");
            foreach (HtmlNode nodetemp in ArticleRankList)
            {
                reader.Read();
                HtmlNode ContentT = nodetemp.SelectSingleNode(".//div[@class='ResRankT']"); //标题                
                ContentT.InnerHtml = reader[1].ToString();

                HtmlNode ContentTime = nodetemp.SelectSingleNode(".//div[@class='ResRankTime']"); //时间
                ContentTime.InnerHtml = reader[3].ToString();

                string strImgInfo = reader[2].ToString();

                HtmlNode ContentI = nodetemp.SelectSingleNode(".//div[@class='ResImgAreaInfo']"); //信息
                ContentI.InnerHtml = strImgInfo.Substring( strImgInfo.IndexOf(',')+1);

                HtmlNode ContentImg = nodetemp.SelectSingleNode(".//div[@class='ResRankImg']"); //图片
                ContentImg.Element("img").SetAttributeValue("src", "PageResImg/" + strImgInfo.Substring(0, strImgInfo.IndexOf(',')));
            }
            reader.Close();


            query = "SELECT * FROM dballwithme.resimginfo ";
            qConditon = "ORDER BY resimgreadnum DESC";
            query = query + qConditon;
            cmd.CommandText = query;
            MySqlDataReader readerC = cmd.ExecuteReader();
            //正文内容加载
            //HtmlNodeCollection ArticleAreaList = rootNode.SelectNodes("//div[@class='ResImgArea']//ul[@class='ResImgAreaUl']");

            HtmlNode ArticleAreaListul = rootNode.SelectSingleNode("//div[@class='ResImgArea']//ul[@class='ResImgAreaUl']");


            foreach (HtmlNode nodetemp in ArticleAreaListul.ChildNodes)
            {
                
                if (nodetemp.Name == "#text")
                    continue;

                readerC.Read();
                HtmlNode ContentT = nodetemp.SelectSingleNode(".//div[@class='ResImgAreaTitle']"); //标题                
                ContentT.InnerHtml = readerC[1].ToString();              

                HtmlNode ContentB = nodetemp.SelectSingleNode(".//div[@class='ResImgAreaBut']"); //button
                ContentB.InnerHtml = "";

                HtmlNode ContentM = nodetemp.SelectSingleNode(".//div[@class='ResImgAreaPartPerson']"); //more
                ContentM.InnerHtml = readerC[6].ToString()+"参与";

                string strImgInfo = readerC[2].ToString();
                string[] strtemp = strImgInfo.Split(',');

                HtmlNode ContentC = nodetemp.SelectSingleNode(".//div[@class='ResImgAreaImgUl']//ul"); //内容
                ContentC.RemoveAllChildren();

                HtmlNode ContentI = nodetemp.SelectSingleNode(".//div[@class='ResImgAreaInfo']"); //信息
                

                if(strtemp.Length<6)
                {
                    for (int i = 0; i < strtemp.Length; i++)
                    {
                        HtmlNode ntemp = hdom.CreateTextNode("<li><img  src ='" + "PageResImg/" + strtemp[i] + "'/></li>");
                        ContentC.AppendChild(ntemp);
                    }
                    ContentI.InnerHtml = "";                    
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        HtmlNode ntemp = hdom.CreateTextNode("<li><img  src ='" + "PageResImg/" + strtemp[i] + "'/></li>");
                        ContentC.AppendChild(ntemp);
                    }
                    for (int i = 6; i < strtemp.Length-1; i++)
                    {
                        ContentI.InnerHtml = ContentI.InnerHtml+strtemp[i]+",";
                    }
                    ContentI.InnerHtml = ContentI.InnerHtml + strtemp[strtemp.Length - 1];
                }

                

            }
            readerC.Close();

            hdom.Save(siteRecordfilepath, Encoding.UTF8);

            conn.Close();
        }
    }
}
