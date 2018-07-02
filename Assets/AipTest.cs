﻿/** 2018.01.10 官方sdk文档
 * http://ai.baidu.com/docs#/NLP-Csharp-SDK/826f1761
 */
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using NLPForUnity;
using Baidu.Aip.Nlp;
using Baidu.Aip.Ocr;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class AipTest : MonoBehaviour
{
    public static string APP_ID = "10666046";
    public static string API_KEY = "75mdtOebRv3HEuMnVxlcmnQF";
    public static string SECRET_KEY = "AhBDSRoSAMk8y4UCWucRs5ZVHw4e6vKM";
    public static Nlp client;

    public string text = "百度是一家高科技公司";
    public string log;

    [SerializeField] private Button LexicalButton;
    [SerializeField] private Button SyntacticButton;
    [SerializeField] private Button WordEmbeddingButton;
    [SerializeField] private Button DnnlmCnButton;

    void Awake()
    {
        //https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
        //一个Mono的坑点，SDK中的有网络请求
        ServicePointManager.ServerCertificateValidationCallback +=
           delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                   System.Security.Cryptography.X509Certificates.X509Chain chain,
                                   System.Net.Security.SslPolicyErrors sslPolicyErrors)
           {
               return true; // **** Always accept
           };

        LexicalButton.onClick.AddListener(NLPLexical);
        SyntacticButton.onClick.AddListener(NLPSyntactic);
        WordEmbeddingButton.onClick.AddListener(NLPWordEmbedding);
        DnnlmCnButton.onClick.AddListener(NLPDnnlmCn);
    }

    void OnDestroy()
    {
        LexicalButton.onClick.RemoveListener(NLPLexical);
        SyntacticButton.onClick.RemoveListener(NLPSyntactic);
        WordEmbeddingButton.onClick.RemoveListener(NLPWordEmbedding);
        DnnlmCnButton.onClick.RemoveListener(NLPDnnlmCn);
    }

    void Start()
    {
        //初始化SDK
        client = new Nlp(API_KEY, SECRET_KEY);
    }

    // 词法分析
    void NLPLexical()
    {
        //百度是一家高科技公司

        log = "";
        JArray array = NLPDemo.LexerDemo(text).items;
        for (int i = 0; i < array.Count; i++)
        {
            log += array[i]["item"].ToString();
            log += " | ";
        }
        Debug.Log(log);
    }

    // 依存句法分析
    void NLPSyntactic()
    {
        //今天天气怎么样
        text = "今天天气怎么样";

        log = "";
        JArray array = NLPDemo.DepParserDemo(text).items;
        for (int i = 0; i < array.Count; i++)
        {
            log += array[i]["word"].ToString();
            log += " | ";
        }
        Debug.Log(log);
    }

    // 词向量表示
    void NLPWordEmbedding()
    {
        text = "张飞";
        log = "";
        JArray array = NLPDemo.WordEmbeddingDemo(text).vec;
        for (int i = 0; i < array.Count; i++)
        {
            log += array[i].ToString();
            log += " | ";
        }
        Debug.Log(log);
    }

    // DNN语言模型
    void NLPDnnlmCn()
    {
        text = "床前明月光";
        log = "";
        JArray array = NLPDemo.DnnlmCnDemo(text).items;
        for (int i = 0; i < array.Count; i++)
        {
            log += array[i]["word"].ToString() + array[i]["prob"].ToString();
            log += " | ";
        }
        Debug.Log(log);
    }

    // 使用Newtonsoft.Json 序列化/反序列化
    void HowToUseNewtonsoft()
    {
        var product = new CharacterListItem();
        product.Id = 0;
        product.Name = "haha";

        //将Product对象转换为Json字符串
        string json = JsonConvert.SerializeObject(product);
        //Debug.Log(json);

        //将Json字符串转换为CharacterListItem类对象
        var Object = JsonConvert.DeserializeObject<CharacterListItem>(json);
        //Debug.Log(Object.Name);
    }
    
}

[System.Serializable]
public class CharacterListItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public string Class { get; set; }
    public string Sex { get; set; }
}
