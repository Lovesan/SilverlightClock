using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace SimpleServer
{
    class Program
    {
        static string prefix;

        static int Main(string[] args)
        {            
            switch (args.Length)
            {
                case 0:
                    prefix = "site/";
                    break;
                case 1:
                    prefix = args[0];
                    if (!Directory.Exists(prefix))
                    {
                        Console.Error.WriteLine("Directory does not exist: {0}", prefix);
                        return 1;
                    }
                    break;
                default:
                    Console.Error.WriteLine("Usage: {0} prefix", System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                    return 1;
            }
            prefix = new DirectoryInfo(prefix).FullName;
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:80/");
            listener.Prefixes.Add("http://*:8080/");
            listener.Start();
            while (listener.IsListening)
            {
                ThreadPool.QueueUserWorkItem(Respond, listener.GetContext());                
            }
            return 0;
        }

        static void Respond(object state)
        {
            HttpListenerContext ctx = (HttpListenerContext)state;
            HttpListenerRequest req = ctx.Request;
            HttpListenerResponse res = ctx.Response;
            string path = Path.GetFullPath(prefix + ("/" == req.RawUrl ? "/index.html" : req.RawUrl));
            Console.WriteLine("Accepted connection from {0}\n" +
                              "User agent: {1}\n" +
                              "File: {2}\n",
                              req.RemoteEndPoint,
                              req.UserAgent,
                              path);
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                res.StatusCode = 200;
                switch (fileInfo.Extension.ToLower())
                {
                    case ".ico":
                        res.ContentType = "image/vnd.microsoft.icon";
                        break;
                    case ".htm":
                    case ".html":
                        res.ContentType = "text/html";
                        break;
                    case ".xaml":
                        res.ContentType = "application/xaml+xml";
                        break;
                    case ".js":
                        res.ContentType = "text/javascript";
                        break;
                    default:
                        res.ContentType = "application/octet-stream";
                        break;
                }
                using (FileStream s = fileInfo.OpenRead())
                {
                    long length = res.ContentLength64 = s.Length;
                    byte[] buffer = new byte[4096];
                    int n;
                    while ((n = s.Read(buffer, 0, 4096)) > 0)
                    {
                        res.OutputStream.Write(buffer, 0, n);
                    }
                }
            }
            else
            {
                res.StatusCode = 404;
            }
            res.Close();
        }
    }
}

