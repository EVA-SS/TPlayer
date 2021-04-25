using System;
using System.IO;
using System.Net;

namespace TPlayer
{
    public class HttpServerManager
    {
        public static HttpListener httpListener;
        // Start is called before the first frame update
        void Start()
        {
            httpListener = new HttpListener();
            httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            httpListener.Prefixes.Add("http://+:443/");
            // httpListener.Prefixes.Add("https://+:443/");

            httpListener.Start();
            httpListener.BeginGetContext(Result, null);
        }
        public static void Result(IAsyncResult ar)
        {
            //当接收到请求后程序流会走到这里

            //继续异步监听
            httpListener.BeginGetContext(Result, null);
            var guid = Guid.NewGuid().ToString();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"接到新的请求:{guid},时间：{DateTime.Now.ToString()}");
            //获得context对象
            var context = httpListener.EndGetContext(ar);
            var request = context.Request;
            var response = context.Response;
            context.Response.ContentType = "application/zip";//告诉客户端返回的ContentType类型为纯文本格式，编码为UTF-8
            context.Response.AddHeader("Content-type", "application/zip");//添加响应头信息
            context.Response.AddHeader("Accept-Ranges", "bytes");//添加响应头信息
            context.Response.AddHeader("Content-Disposition", "attachment; filename = testzip.zip");
            string returnObj = null;//定义返回客户端的信息
            if (request.HttpMethod == "POST" && request.InputStream != null)
            {
                //处理客户端发送的请求并返回处理信息
                returnObj = "123";
            }
            else
            {
                returnObj = $"不是post请求或者传过来的数据为空";
            }
            var returnByteArr = File.ReadAllBytes("D:\\orig.zip");//Encoding.UTF8.GetBytes(returnObj);//设置客户端返回信息的编码
            try
            {
                using (var stream = response.OutputStream)
                {
                    //把处理信息返回到客户端
                    stream.Write(returnByteArr, 0, returnByteArr.Length);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"网络蹦了：{ex.ToString()}");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"请求处理完成：{guid},时间：{ DateTime.Now.ToString()}\r\n");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
