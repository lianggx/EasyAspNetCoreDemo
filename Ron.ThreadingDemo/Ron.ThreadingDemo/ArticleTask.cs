using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.ThreadingDemo
{
    public class ArticleTask
    {
        public static void Test()
        {
            Random rand = new Random();
            CancellationTokenSource cts = new CancellationTokenSource();
            List<Task<Article>> tasks = new List<Task<Article>>();
            TaskFactory factory = new TaskFactory(cts.Token);
            foreach (var t in new string[] { "Article", "Post", "Love" })
            {
                Console.WriteLine("开始请求");
                tasks.Add(factory.StartNew(() =>
                            {
                                var article = new Article { Type = t };
                                if (t == "Article")
                                {
                                    article.Data.Add("文章已加载");
                                }
                                else
                                {
                                    for (int i = 1; i < 5; i++)
                                    {
                                        Thread.Sleep(rand.Next(1000, 2000));
                                        Console.WriteLine("load:{0}", t);
                                        article.Data.Add($"{t}_{i}");
                                    }
                                }
                                return article;
                            }, cts.Token));
            }

            Console.WriteLine("开始合并结果");
            foreach (var task in tasks)
            {
                Console.WriteLine();
                var result = task.Result;
                foreach (var d in result.Data)
                {
                    Console.WriteLine("{0}:{1}", result.Type, d);
                }
                task.Dispose();
            }

            cts.Cancel();
            cts.Dispose();
            Console.WriteLine("\nIsCancellationRequested:{0}", cts.IsCancellationRequested);
        }
    }

    public class Article
    {
        public string Type { get; set; }
        public List<string> Data { get; set; } = new List<string>();
    }
}
