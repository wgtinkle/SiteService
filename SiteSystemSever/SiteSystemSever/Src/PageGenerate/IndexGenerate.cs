using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SiteSystemSever.PageGenerate
{
    class IndexGenerate
    {
        public void GenerateIndexFile(string srcpath,string targpath)//对外接口
        {
            HtmlDocument IndexPage = new HtmlDocument();
            IndexPage.Load(srcpath, Encoding.UTF8);

            HtmlNode rootNode = IndexPage.DocumentNode;
            IndexBlockArticle(rootNode);
            IndexBlockOpinion(rootNode);
            IndexBlockData(rootNode);
            IndexBlockRes(rootNode);
            IndexBlockRelation(rootNode);
            IndexBlockFlow(rootNode);
            IndexPage.Save(targpath);
        }

        void IndexBlockArticle(HtmlNode rootn)  //主页文章模块 从数据库中读取前5条
        {
            //图片
            HtmlNodeCollection ListImgli = rootn.SelectNodes("//div[@class='NewsBrowser']//ul//li");
            foreach (HtmlNode LiNode in ListImgli)
            {
                LiNode.FirstChild.SetAttributeValue("src","Testsrc");
            }
            //内容
            HtmlNodeCollection ListContentli = rootn.SelectNodes("//div[@class='NewsContent']//ul//li");
            foreach (HtmlNode LiNode in ListContentli)
            {
                LiNode.FirstChild.InnerHtml = "123";
            }
            //索引
            HtmlNodeCollection ListIndexli = rootn.SelectNodes("//div[@class='NewsIndex']//ul//li");
            foreach (HtmlNode LiNode in ListIndexli)
            {
                LiNode.Element("a").SetAttributeValue("href", "http://123");
                LiNode.Element("a").Element("p").InnerHtml = "123";
            }
        }
        void IndexBlockOpinion(HtmlNode rootn)  //主页观点模块
        {
            //最热的5个观点
            HtmlNodeCollection ListHotli = rootn.SelectNodes("//div[@class='COpinionRank']//ul//li");
            foreach (HtmlNode LiNode in ListHotli)
            {
                IEnumerable<HtmlNode> divNodes = LiNode.Elements("div");
                foreach (HtmlNode divNode in divNodes)
                {
                    divNode.InnerHtml = "标题";
                }
                
            }
            //最新的5个观点
            HtmlNodeCollection ListLatestTitleli = rootn.SelectNodes("//div[@class='COpComment']");
            foreach (HtmlNode LiNode in ListLatestTitleli)
            {
                LiNode.InnerHtml = "观点标题";
            }
            HtmlNodeCollection ListLatestContentul = rootn.SelectNodes("//ul[@class='COpCommentul']");
            foreach (HtmlNode UlNode in ListLatestContentul)
            {
                IEnumerable<HtmlNode> liNodes = UlNode.Elements("li");
                foreach (HtmlNode liNode in liNodes)
                {
                    liNode.InnerHtml = "观点内容";
                }
            }
        }
        void IndexBlockData(HtmlNode rootn)  //主页数据模块
        {
            //趋势
            HtmlNodeCollection ListTrandli = rootn.SelectNodes("//div[@class='COpTrend']//ul//li");
            foreach (HtmlNode LiNode in ListTrandli)
            {
                LiNode.Element("div").FirstChild.SetAttributeValue("src", "./dfdf/dfdfd.png");
                LiNode.Element("a").SetAttributeValue("href","http://123");
                LiNode.Element("a").InnerHtml= "趋势标题";
            }
            //分布
            HtmlNodeCollection ListSpreadli = rootn.SelectNodes("//div[@class='COpSpread']//ul//li");
            foreach (HtmlNode LiNode in ListSpreadli)
            {
                LiNode.Element("div").FirstChild.SetAttributeValue("src", "./dfdf/dfdfd.png");
                LiNode.Element("a").SetAttributeValue("href", "http://123");
                LiNode.Element("a").InnerHtml = "分布标题";
            }

        }
        void IndexBlockRes(HtmlNode rootn)  //主页资源模块
        {

        }
        void IndexBlockRelation(HtmlNode rootn)  //主页关系模块
        {

        }
        void IndexBlockFlow(HtmlNode rootn)  //主页流模块
        {

        }

    }
}
