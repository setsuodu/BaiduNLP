﻿using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Baidu.Aip.Nlp;

namespace NLPForUnity
{
    public class NLPDemo
    {
        public static string APP_ID = "10666046";
        public static string API_KEY = "75mdtOebRv3HEuMnVxlcmnQF";
        public static string SECRET_KEY = "AhBDSRoSAMk8y4UCWucRs5ZVHw4e6vKM";
        public static Nlp client;

        //词法分析
        public static ResultSerialize LexerDemo(string text)
        {
            client = new Nlp(API_KEY, SECRET_KEY);

            JObject result = client.Lexer(text);
            //System.Diagnostics.Debug.WriteLine(result);

            ResultSerialize res = new ResultSerialize();
            res.log_id = result["log_id"].ToString();
            res.text = result["text"].ToString();
            res.items = (JArray)result["items"];
            for (int i = 0; i < res.items.Count; i++)
            {
                LexerSerialize _item = new LexerSerialize();
                //loc_details = 
                _item.byte_offset = int.Parse(res.items[i]["byte_offset"].ToString());
                _item.uri = res.items[i]["uri"].ToString();
                _item.pos = res.items[i]["pos"].ToString();
                _item.ne = res.items[i]["ne"].ToString();
                _item.item = res.items[i]["item"].ToString();
                //basic_words = 
                _item.byte_length = int.Parse(res.items[i]["byte_length"].ToString());
                _item.formal = res.items[i]["formal"].ToString();
                //System.Diagnostics.Debug.Print("item是 " + res.items[i]["item"].ToString());
            }
            return res;
        }

        //依存句法分析
        public static ResultSerialize DepParserDemo(string text)
        {
            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用依存句法分析，可能会抛出网络等异常，请使用try/catch捕获
            JObject result = client.DepParser(text);
            //System.Diagnostics.Debug.WriteLine(result);

            ResultSerialize res = new ResultSerialize();
            res.log_id = result["log_id"].ToString();
            res.text = result["text"].ToString();
            res.items = (JArray)result["items"];
            for (int i = 0; i < res.items.Count; i++)
            {
                DepParserSerialize _gammar = new DepParserSerialize();
                //_gammar.id = res.items[i]["id"].ToString();
                _gammar.word = res.items[i]["word"].ToString();
                _gammar.postag = res.items[i]["postag"].ToString();
                _gammar.head = res.items[i]["head"].ToString();
                _gammar.deprel = res.items[i]["deprel"].ToString();
            }
            return res;

            // 如果有可选参数
            var options = new Dictionary<string, object> {
                {"mode", 1}
            };
            // 带参数调用依存句法分析
            result = client.DepParser(text, options);
        }

        //词向量表示
        public static void WordEmbeddingDemo(string text)
        {
            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用词向量表示，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.WordEmbedding(text);
            System.Diagnostics.Debug.WriteLine(result);
        }

        //DNN语言模型
        public static void DnnlmCnDemo(string text)
        {
            //床前明月光;
            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用DNN语言模型，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.DnnlmCn(text);
            System.Diagnostics.Debug.WriteLine(result);
        }

        //词义相似度
        //两个文本相似度得分
        public static void WordSimEmbeddingDemo(string word1, string word2)
        {
            //"北京", "上海"
            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用词义相似度，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.WordSimEmbedding(word1, word2);
            System.Diagnostics.Debug.WriteLine(result);

            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"mode", 0}
            };

            // 带参数调用词义相似度
            result = client.WordSimEmbedding(word1, word2, options);
            System.Diagnostics.Debug.WriteLine(result);
        }

        //短文本相似度
        //短文本相似度接口用来判断两个文本的相似度得分
        public static void SimnetDemo(string text1, string text2)
        {
            //"浙富股份","万事通自考网"

            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用短文本相似度，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.Simnet(text1, text2);
            System.Diagnostics.Debug.WriteLine(result);

            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"model", "CNN"}
            };

            // 带参数调用短文本相似度
            result = client.Simnet(text1, text2, options);
            System.Diagnostics.Debug.WriteLine(result);
        }

        //评论观点抽取
        public static void CommentTagDemo(string text)
        {
            //"三星电脑电池不给力"

            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用评论观点抽取，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.CommentTag(text);
            System.Diagnostics.Debug.WriteLine(result);

            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"type", 13}
            };

            // 带参数调用评论观点抽取
            result = client.CommentTag(text, options);
            System.Diagnostics.Debug.WriteLine(result);
        }

        //情感倾向分析
        public static void SentimentClassifyDemo(string text)
        {
            //"苹果是一家伟大的公司"

            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用情感倾向分析，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.SentimentClassify(text);
            System.Diagnostics.Debug.WriteLine(result);
        }
    }
}

//总体对象
[System.Serializable]
public class ResultSerialize
{
    public string log_id;
    public string text;
    public JArray items;
}

//词法分析
[System.Serializable]
public class LexerSerialize
{
    public JArray loc_details;
    public int byte_offset;
    public string uri;
    public string pos;
    public string ne;
    public string item;
    public JArray basic_words;
    public int byte_length;
    public string formal;
}

//依存句法分析
[System.Serializable]
public class DepParserSerialize
{
    //public string id; //官方api有，实际返回没有
    public string word;
    public string postag;
    public string head;
    public string deprel;
}

