using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RedisTest
{

    class PubEventArgs : EventArgs
    {
        private string _magzineName;
        private string _pubDate;
        public string MagzineName { get { return _magzineName; } }
        public string PubDate { get{return _pubDate;} }

        public PubEventArgs(string magzineName, string pubDate)
        {
            _magzineName = magzineName;
            _pubDate = pubDate;
        }

    }


    class Singleton
    {
        private static Singleton singleton;
        private static readonly object obj = new object();
        public static Singleton GetInstance()
        {
            if (singleton == null)
            {
                lock (obj)
                { 
                    if(singleton == null)
                    {
                        singleton = new Singleton();
                    }
                }
            }

            return singleton;
        }

    }

    class Singleton<T> where T : class, new()
    {
        private static T _instance;
        private static readonly object obj = new object();
        public static T GetInstance()
        {
            if (_instance == null)
            {
                lock (obj)
                {
                    if (_instance == null)
                    {
                        var tmp = Activator.CreateInstance<T>();
                        System.Threading.Interlocked.Exchange(ref _instance, tmp);
                    }
                }
            }
            return _instance;
        }
    }


    class Publisher
    { 
        public delegate void PubComputerEventHandler(object sender, PubEventArgs e);
        public event PubComputerEventHandler PubComputer;

        public void OnPubComputer(PubEventArgs e)
        {
            PubComputerEventHandler handler = PubComputer;
            if(handler!=null)
            {
                handler(this, e);
            }
        }

        public void IssueComputer(string MagzineName,string PubDate)
        {
            Console.WriteLine("发布事件！");
            OnPubComputer(new PubEventArgs(MagzineName, PubDate));
        }

    }

    class Subscriber
    {
        private string _name;

        public Subscriber(string Name)
        {
            _name = Name;
        }

        public void Receive(object sender, PubEventArgs e)
        {
            Console.WriteLine(e.PubDate + ":  " + this._name + " 已收到订阅的 " + e.MagzineName);
        }
    }


    class Program
    {
        //全局变量
        private static int _result;
        #region
        static void Test(string s = "11111")
        {
            Console.WriteLine(s);
        }
        static void Test()
        {
            Console.WriteLine("2222");
        }

        public delegate  void EatAsync(string food); //定义委托
        private static void eat(string food)
        {
            Thread.Sleep(2000);
            Console.WriteLine("I am eating.... on thread: {0}", Thread.CurrentThread.ManagedThreadId);
            //Trace.TraceInformation("I am eating.... on thread: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private static void clean(IAsyncResult asyncResult)
        {
            Console.WriteLine("I am done eating.... on thread: {0}", Thread.CurrentThread.ManagedThreadId);
            //Trace.TraceInformation("I am done eating.... on thread: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        #endregion


        private static readonly object a;

        //线程调用方法
        public static void Work(int TaskID)
        {
            for (int i = 0; i < 10; i++)
            {
                _result++;

                //Interlocked.Increment(ref _result);

            }
        }

        public static async Task<string> GetName()
        {

            return await Task.Run(() =>
            {
                Console.WriteLine("task thread: {0}", Thread.CurrentThread.ManagedThreadId);
                return "bbbb";
            });
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern int EnumJobs(IntPtr hPrinter, int FirstJob, int NoJobs, int Level, IntPtr pInfo, int cdBuf, out int pcbNeeded, out int pcReturned);

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern long GetJob(long hPrinter, long JobId, long Level, long buffer, long pbSize, long pbSizeNeeded);

        /// <summary>
        /// 得到具体打印机作业数
        /// </summary>
        /// <param name="printerToPeek">打印机名称</param>
        /// <returns></returns>
        public static int peekPrinterJobs(string printerToPeek)
        {
            IntPtr handle;
            int FirstJob = 0;
            int NumJobs = 127;
            int pcbNeeded;
            int pcReturned;

            // open printer 
            OpenPrinter(printerToPeek, out handle, IntPtr.Zero);

            // get num bytes required, here we assume the maxt job for the printer quest is 128 (0..127) 
            EnumJobs(handle, FirstJob, NumJobs, 1, IntPtr.Zero, 0, out pcbNeeded, out pcReturned);


            // allocate unmanaged memory 
            IntPtr pData = Marshal.AllocHGlobal(pcbNeeded);

            // get structs 
            EnumJobs(handle, FirstJob, NumJobs, 1, pData, pcbNeeded, out pcbNeeded, out pcReturned);

            //Close the Printer
            ClosePrinter(handle);

            return pcReturned;
        }

        static void Main(string[] args)
        {

            int a = peekPrinterJobs("");
            int c = 0;
            //string a = Console.ReadLine();
            //int len = a.Length - 1;
            //int x, y;
            //for (x = 0, y = len - x; x < (len + 1) / 2 - 1; ++x, --y)
            //{
            //    if (a[x] != a[y])
            //    {
            //        Console.WriteLine("not");
            //    }
            //}

            //Console.ReadLine();

                //Subscriber zs = new Subscriber("张三");

                //Publisher comPub = new Publisher();

                //comPub.PubComputer += new Publisher.PubComputerEventHandler(zs.Receive);
                //comPub.IssueComputer("《读者》", DateTime.Now.ToString());


                //Thread t = new Thread((ThreadStart)delegate
                //{
                //    try {
                //        throw new ArgumentException("aaaaaaaa");
                //    }
                //    catch (Exception ex)
                //    {
                //    }
                //});
                //t.Start();

            Func<int, int> negate =
            (n) =>
            {
                Console.WriteLine("Task={0}, n={1}, -n={2}, Thread={3}", Task.CurrentId, n, -n, Thread.CurrentThread.ManagedThreadId);
                return -n;
            };

                //int a = negate.Invoke(4);
                //Console.WriteLine(a.ToString() + " Main thread: {0}", Thread.CurrentThread.ManagedThreadId);


                //new Func<int, bool>((a) => { if (a > 1) { Console.WriteLine("hello "); return true; } return false; }).Invoke(1);

                //var s = GetName().GetAwaiter();

                //GetName().GetAwaiter().GetResult();

                //new Thread(delegate() { Console.WriteLine("delegate thread: {0}", Thread.CurrentThread.ManagedThreadId); }).Start();
                //Task.Factory.StartNew(() => { Thread.Sleep(200); Console.WriteLine("task thread: {0}", Thread.CurrentThread.ManagedThreadId); });
                //Console.WriteLine("Main thread: {0}", Thread.CurrentThread.ManagedThreadId);


                //Console.WriteLine(s.GetResult());

                //var myeat = new EatAsync(eat);
                //myeat.BeginInvoke("apple", new AsyncCallback(clean), myeat);

                //while (true)
                //{
                //    string str1 = Console.ReadLine();
                //    string str2 = Console.ReadLine();

                //    Regex rg = new Regex(@"\W");

                //    var arr1 = rg.Split(str1);
                //    var arr2 = rg.Split(str2);

                //    var arr3 = arr1.Where(p => !string.IsNullOrEmpty(p) && !arr2.Any(q => q.ToLower() == p.ToLower())).ToArray();

                //    Console.WriteLine(string.Join(",",arr3));

                //}


                //while (true)
                //{
                //    Task[] _tasks = new Task[100];
                //    int i = 0;

                //    for (i = 0; i < _tasks.Length; i++)
                //    {
                //        _tasks[i] = Task.Factory.StartNew((num) =>
                //       {
                //           var taskid = (int)num;
                //           Work(taskid);

                //       }, i);
                //    }

                //    Task.WaitAll(_tasks);
                //    Console.WriteLine(_result);

                //    Console.ReadKey();
                //}


                //int[] arrSort = { 50, 25, 34, 60, 4, 90, 100, 7, 66, 41 };

                //for (int i = 0; i < arrSort.Length; i++)
                //{
                //    for (int j = i + 1; j < arrSort.Length; j++)
                //    {
                //        if (arrSort[j] < arrSort[i])
                //        {
                //            int temp;
                //            temp = arrSort[j];
                //            arrSort[j] = arrSort[i];
                //            arrSort[i] = temp;
                //        }
                //    }
                //}


                //for (int i = 0; i < arrSort.Length; i++)
                //{
                //    for (int j = i + 1; j < arrSort.Length; j++)
                //    {
                //        if (arrSort[j] < arrSort[i])
                //        {
                //            int temp;
                //            temp = arrSort[j];
                //            arrSort[j] = arrSort[i];
                //            arrSort[i] = temp;
                //        }
                //    }
                //}

                //for (int i = 0; i < arrSort.Length; i++)
                //{
                //    Console.Write(arrSort[i].ToString() + " ");
                //}



                //Test();



                //Console.WriteLine(a.GetType());
                //Thread td = new Thread(() => { Console.Write("aaa"); });
                //Thread.Sleep(1000);
                //td.Start();

                ////if (new Func<bool>(() => { Console.Write("hello "); return false; }).Invoke())
                ////if ((args = new string[1]{"hello "}).Length>0 && args.Any(s => { Console.Write(s ); return false; }))
                ////if (true) Console.Write("Hello World!"); else if (false)
                ////if ((new Thread(() => { Console.Write("hello "); }).Start() is object) ||  Thread.Sleep(100) is object)
                ////if(Console.Write("hello ") is object && Environment.Exit(0) is object)
                //if (new Func<bool>(() => { Console.Write("hello world"); Thread.Sleep(1000); Environment.Exit(0); return false; }).Invoke())
                //{
                //    Console.Write("Hello");
                //}
                //else
                //{
                //    Console.Write("world");
                //}
                /*
                var client = new RedisClient("localhost", 6379);
                client.Set<string>("name", "ericdeng");

                string s = client.Get<string>("name");
                Console.WriteLine(s);

                client.Set<int>("accessAcount", 0);
                client.IncrBy("accessAcount", 10);
                Console.WriteLine(client.Get<int>("accessAcount"));

                //hash
                Console.WriteLine("hash");
                client.SetEntryInHash("test", "u1", "hash1");
                client.SetEntryInHash("test", "u2", "hash2");
                client.SetEntryInHash("test", "u3", "hash3");
                client.GetAllEntriesFromHash("test").ToList().ForEach(e => Console.WriteLine(e.Key + "=>" + e.Value));

                //list
                Console.WriteLine("list");
                client.AddItemToList("aa", "list1");
                client.AddItemToList("aa", "list2");
                client.GetAllItemsFromList("aa").ForEach(e => Console.WriteLine(e));

                Console.WriteLine("出栈：" + client.PopItemFromList("aa"));
                Console.WriteLine("出栈：" + client.PopItemFromList("aa"));
            
                //set
                client.AddItemToSet("A", "B");
                client.AddItemToSet("A", "C");
                client.AddItemToSet("A", "D");
                client.AddItemToSet("A", "E");
                client.AddItemToSet("A", "F");

                client.AddItemToSet("B", "C");
                client.AddItemToSet("B", "F");

                //求差集
                Console.WriteLine("A,B集合差集");
                client.GetDifferencesFromSet("A", "B").ToList<string>().ForEach(e => Console.Write(e + ","));

                //求集合交集
                Console.WriteLine("\nA,B集合交集");
                client.GetIntersectFromSets(new string[] { "A", "B" }).ToList<string>().ForEach(e => Console.Write(e + ","));

                //求集合并集
                Console.WriteLine("\nA,B集合并集");
                client.GetUnionFromSets(new string[] { "A", "B" }).ToList<string>().ForEach(e => Console.Write(e + ","));

                //sort set
                client.AddItemToSortedSet("SA", "B", 2);
                client.AddItemToSortedSet("SA", "C", 1);
                client.AddItemToSortedSet("SA", "D", 5);
                client.AddItemToSortedSet("SA", "E", 3);
                client.AddItemToSortedSet("SA", "F", 4);

                //有序集合降序排列
                Console.WriteLine("\n有序集合降序排列");
                client.GetAllItemsFromSortedSetDesc("SA").ForEach(e => Console.Write(e + ","));
                Console.WriteLine("\n有序集合升序序排列");
                client.GetAllItemsFromSortedSet("SA").ForEach(e => Console.Write(e + ","));

                client.AddItemToSortedSet("SB", "C", 2);
                client.AddItemToSortedSet("SB", "F", 1);
                client.AddItemToSortedSet("SB", "D", 3);

                Console.WriteLine("\n获得某个值在有序集合中的排名，按分数的升序排列");
                Console.WriteLine(client.GetItemIndexInSortedSet("SB", "D"));

                Console.WriteLine("\n获得有序集合中某个值得分数");
                Console.WriteLine(client.GetItemScoreInSortedSet("SB", "D"));

                Console.WriteLine("\n获得有序集合中，某个排名范围的所有值");
                client.GetRangeFromSortedSet("SA", 0, 3).ForEach(e => Console.Write(e + ","));


                RedisBase.Hash_Set<string>("hashtest", "username1", "ericdeng");
                RedisBase.Hash_Set<string>("hashtest", "username2", "markdeng");
                */
                Console.ReadLine();

        }
    }
}
