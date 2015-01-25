using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GeneratePage
{
    class Program
    {

        static void Main(string[] args)
        {
            //GenerateIndexRes GindxRes = new GenerateIndexRes();
            //GindxRes.FuncGenerateRes("C:\\Users\\wjing\\Desktop\\IndexRes.htm");

            //GenerateIndexData GindxData = new GenerateIndexData();
            //GindxData.FuncGenerateData("C:\\Users\\Administrator\\Desktop\\IndexData.htm");

            GenerateIndexOpinion GindxOpinion = new GenerateIndexOpinion();
            GindxOpinion.FuncGenerateOpinion("C:\\Users\\wjing\\Desktop\\IndexOpinion.htm");
        }
    }
}
