using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Baidu.Aip.Face;
using Newtonsoft.Json.Linq;

namespace NLPForUnity
{
    public class FaceDemo
    {
        public static string APP_ID = "10482113";
        public static string API_KEY = "zlLbcpmVj6BAEfmt5YIsERqb";
        public static string SECRET_KEY = "499S5OSVSKLN1tqG9RcPTfuAbnk69HGP";
        public static Face client;

        public static void DetectDemo(string filepath)
        {
            client = new Face(API_KEY, SECRET_KEY);

            var image = File.ReadAllBytes(filepath);
            // 调用人脸检测，可能会抛出网络等异常，请使用try/catch捕获

            JObject result = client.Detect(image);
            System.Diagnostics.Debug.WriteLine(result);

            /*
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"max_face_num", 2},
                {"face_fields", "age"}
            };
            // 带参数调用人脸检测
            result = client.Detect(image, options);
            System.Diagnostics.Debug.WriteLine(result);
            */
        }
    }
}
