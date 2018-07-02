using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace com.baidu.ai
{
    public class Utterance :MonoBehaviour
    {
        // unit对话接口
        public static string unit_utterance()
        {
            string token = ""; //"#####调用鉴权接口获取的token#####";
            string host = "https://aip.baidubce.com/rpc/2.0/unit/bot/chat?access_token=" + token;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.ContentType = "application/json";
            request.KeepAlive = true;
            string str = "{\"bot_session\":\"\",\"log_id\":\"7758521\",\"request\":{\"bernard_level\":0,\"client_session\":\"{\\\"client_results\\\":\\\"\\\", \\\"candidate_options\\\":[]}\",\"query\":\"你好\",\"query_info\":{\"asr_candidates\":[],\"source\":\"KEYBOARD\",\"type\":\"TEXT\"},\"updates\":\"\",\"user_id\":\"88888\"},\"bot_id\":1057,\"version\":\"2.0\"}"; // json格式 
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            Console.WriteLine("对话接口返回:");
            Console.WriteLine(result);
            return result;
        }
    }
}