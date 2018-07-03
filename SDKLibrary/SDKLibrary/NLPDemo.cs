using System.Collections.Generic;
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

        // 词法分析
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

        // 依存句法分析
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

        // 词向量表示
        public static WordEmbeddingSerialize WordEmbeddingDemo(string text)
        {
            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用词向量表示，可能会抛出网络等异常，请使用try/catch捕获
            JObject result = client.WordEmbedding(text);
            //System.Diagnostics.Debug.WriteLine(result);

            WordEmbeddingSerialize res = new WordEmbeddingSerialize();
            res.word = result["word"].ToString();
            res.vec = (JArray)result["vec"];
            return res;
        }

        // DNN语言模型
        public static DnnlmCnSerialize DnnlmCnDemo(string text)
        {
            //床前明月光;
            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用DNN语言模型，可能会抛出网络等异常，请使用try/catch捕获
            JObject result = client.DnnlmCn(text);
            //System.Diagnostics.Debug.WriteLine(result);

            DnnlmCnSerialize res = new DnnlmCnSerialize();
            res.log_id = result["log_id"].ToString();
            res.text = result["text"].ToString();
            res.items = (JArray)result["items"];
            for (int i = 0; i < res.items.Count; i++)
            {
                DnnlmCn_Sub _item = new DnnlmCn_Sub();
                _item.word = res.items[i]["word"].ToString();
                _item.prob = double.Parse(res.items[i]["prob"].ToString());
            }
            res.ppl = double.Parse(result["ppl"].ToString());
            return res;
        }

        // 词义相似度
        // 两个文本相似度得分
        public static WordSimEmbeddingSerialize WordSimEmbeddingDemo(string word1, string word2, Dictionary<string, object> options = null)
        {
            //"北京", "上海"
            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用词义相似度，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.WordSimEmbedding(word1, word2);
            //System.Diagnostics.Debug.WriteLine(result);

            // 如果有可选参数
            /*
            var options = new Dictionary<string, object>{
                {"mode", 0}
            };
            */

            // 带参数调用词义相似度
            //var result = client.WordSimEmbedding(word1, word2, options);
            //System.Diagnostics.Debug.WriteLine(result);

            WordSimEmbeddingSerialize res = new WordSimEmbeddingSerialize();
            res.score = result["score"].ToString();
            return res;
        }

        // 短文本相似度
        // 短文本相似度接口用来判断两个文本的相似度得分
        public static SimnetSerialize SimnetDemo(string text1, string text2)
        {
            //"浙富股份","万事通自考网"

            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用短文本相似度，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.Simnet(text1, text2);
            //System.Diagnostics.Debug.WriteLine(result);

            /*
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"model", "CNN"}
            };

            // 带参数调用短文本相似度
            result = client.Simnet(text1, text2, options);
            //System.Diagnostics.Debug.WriteLine(result);
            */

            SimnetSerialize res = new SimnetSerialize();
            res.score = result["score"].ToString();
            return res;
        }

        // 评论观点抽取
        public static CommentTagSerialize CommentTagDemo(string text)
        {
            //"三星电脑电池不给力"

            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用评论观点抽取，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.CommentTag(text);
            //System.Diagnostics.Debug.WriteLine(result);

            /*
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"type", 13}
            };

            // 带参数调用评论观点抽取
            result = client.CommentTag(text, options);
            //System.Diagnostics.Debug.WriteLine(result);
            */

            CommentTagSerialize res = new CommentTagSerialize();
            res.items = (JArray)result["items"];
            for (int i = 0; i < res.items.Count; i++)
            {
                CommentTag_Sub _item = new CommentTag_Sub();
                _item.prop = res.items[i]["prop"].ToString();
                _item.adj = res.items[i]["adj"].ToString();
                _item.sentiment = int.Parse(res.items[i]["sentiment"].ToString());
                _item.begin_pos = int.Parse(res.items[i]["begin_pos"].ToString());
                _item.end_pos = int.Parse(res.items[i]["end_pos"].ToString());
                _item.abs = res.items[i]["abstract"].ToString();
            }
            return res;
        }

        // 情感倾向分析
        public static SentimentClassifySerialize SentimentClassifyDemo(string text)
        {
            //"苹果是一家伟大的公司"

            client = new Nlp(API_KEY, SECRET_KEY);

            // 调用情感倾向分析，可能会抛出网络等异常，请使用try/catch捕获
            JObject result = client.SentimentClassify(text);
            //System.Diagnostics.Debug.WriteLine(result);

            SentimentClassifySerialize res = new SentimentClassifySerialize();
            res.text = result["text"].ToString();
            res.items = (JArray)result["items"];
            for (int i = 0; i < res.items.Count; i++)
            {
                SentimentClassify_Sub _sub = new SentimentClassify_Sub();
                _sub.sentiment = int.Parse(res.items[i]["sentiment"].ToString());
                _sub.confidence = float.Parse(res.items[i]["confidence"].ToString());
                _sub.positive_prob = float.Parse(res.items[i]["positive_prob"].ToString());
                _sub.negative_prob = float.Parse(res.items[i]["negative_prob"].ToString());
            }
            return res;
        }
    }
}

// 总体对象
[System.Serializable]
public class ResultSerialize
{
    public string log_id;
    public string text;
    public JArray items;
}

// 词法分析
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

// 依存句法分析
[System.Serializable]
public class DepParserSerialize
{
    //public string id; //官方api有，实际返回没有
    public string word;
    public string postag;
    public string head;
    public string deprel;
}

// 词向量
[System.Serializable]
public class WordEmbeddingSerialize
{
    //public string log_id; //请求唯一标识码
    public string word; //查询词
    public JArray vec; //词向量结果表示
}

// DNN
[System.Serializable]
public class DnnlmCnSerialize
{
    public string log_id; //请求唯一标识码
    public string text; //文本内容
    public JArray items;
    public string word; //句子的切词结果
    public double prob; //该词在句子中的概率值,取值范围[0,1]
    public double ppl; //描述句子通顺的值：数值越低，句子越通顺
}

public class DnnlmCn_Sub
{
    public string word; //句子的切词结果
    public double prob; //该词在句子中的概率值,取值范围[0,1]
}

// 语义相似度
[System.Serializable]
public class WordSimEmbeddingSerialize
{
    public string log_id; //请求唯一标识码
    public string score; //相似度分数
    public JArray words; //输入的词列表
}

public class WordSimEmbedding_Sub
{
    public string word_1; //输入的word1参数
    public string word_2; //输入的word2参数
}

// 短文本相似度
[System.Serializable]
public class SimnetSerialize
{
    public string log_id; //请求唯一标识码
    public JArray texts; //输入文本
    public string score; //两个文本相似度得分
}

// 评论观点抽取
[System.Serializable]
public class CommentTagSerialize
{
    //public string log_id; //请求唯一标识码
    public JArray items;
}

[System.Serializable]
public class CommentTag_Sub
{
    public string prop; //匹配上的属性词
    public string adj; //匹配上的描述词
    public int sentiment; //该情感搭配的极性（0表示消极，1表示中性，2表示积极）
    public int begin_pos; //该情感搭配在句子中的开始位置
    public int end_pos; //该情感搭配在句子中的结束位置
    public string abs; //对应于该情感搭配的短句摘要
}

// 情感倾向分析
[System.Serializable]
public class SentimentClassifySerialize
{
    public string text; //输入的文本内容
    public JArray items; //输入的词列表
}

[System.Serializable]
public class SentimentClassify_Sub
{
    public int sentiment; //表示情感极性分类结果, 0:负向，1:中性，2:正向
    public float confidence; //表示分类的置信度
    public float positive_prob; //表示属于积极类别的概率
    public float negative_prob; //表示属于消极类别的概率
}
