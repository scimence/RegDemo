
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sci
{
    // http://scimence.gitee.io/url/QRPay.html 开发者账号注册时，设置您的开发者名称
    // 添加RegTool.cs类到项目，为应用提供注册功能

    // 1、初始化，RegDemo为软件名称请自定义，scimence修改为您自己的支付宝收款账号
    // Sci.RegTool.Init("RegDemo", "scimence");

    // 2、调用注册检测，若已注册则执行软件自己的功能逻辑 ToolLogic()
    // Sci.RegTool.CheckRegDoing(ToolLogic);

    /// <summary>
    /// 软件注册插件。
    /// 使用说明：https://mp.csdn.net/console/editor/html/90448771
    /// </summary>
    class RegTool
    {
        /// <summary>
        /// 调用assembly中的静态方法
        /// </summary>
        private static object RunStatic(Assembly assembly, string classFullName, string methodName, object[] args)
        {
            if (assembly == null) return null;

            Type type = assembly.GetType(classFullName, true, true);

            //object[] arg = new object[] { "参数1", "参数2" };
            object tmp = type.InvokeMember(methodName, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, args);
            return tmp;
        }

        private static Assembly asm = null;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="softName">软件名称</param>
        /// <param name="authorName">软件作者</param>
        public static void Init(string softName, string authorName)
        {
            if (asm == null)
            {
                asm = Assembly.Load(GetByte());
            }

            if (asm != null)
            {
                object[] args = new object[] { softName, authorName };
                RunStatic(asm, "SciReg.RegTool", "Init", args);
            }
        }

        /// <summary>
        /// 检测是否已注册，若已注册，则执行regSuccessDoing函数逻辑
        /// </summary>
        /// <param name="regSuccessDoing">回调函数</param>
        public static void CheckRegDoing(RegTool.Func regSuccessDoing)
        {
            if (asm != null)
            {
                thisCall = regSuccessDoing;
                Delegate deleg = NewDelegate(asm);

                object[] args = new object[] { deleg };
                RunStatic(asm, "SciReg.RegTool", "CheckRegDoing", args);
            }
        }

        #region 拓展接口

        /// <summary>
        /// 获取后台配置的注册金额信息
        /// </summary>
        public static string GetPrice()
        {
            if (asm != null)
            {
                object[] args = new object[] { };
                Object obj = RunStatic(asm, "SciReg.RegTool", "GetPrice", args);
                return obj.ToString();
            }
            return "0.00";
        }

        /// <summary>
        /// 获取后台配置的链接地址信息
        /// </summary>
        public static string LinkUrl()
        {
            if (asm != null)
            {
                object[] args = new object[] { };
                Object obj = RunStatic(asm, "SciReg.RegTool", "LinkUrl", args);
                return obj.ToString();
            }
            return "";
        }

        /// <summary>
        /// 获取后台配置的推荐地址信息
        /// </summary>
        public static string RecommendUrl()
        {
            if (asm != null)
            {
                object[] args = new object[] { };
                Object obj = RunStatic(asm, "SciReg.RegTool", "RecommendUrl", args);
                return Convert.ToString(obj);
            }
            return "";
        }

        /// <summary>
        /// 判断当前是否需要注册
        /// </summary>
        public static bool NeedRegister()
        {
            if (asm != null)
            {
                object[] args = new object[] { };
                Object obj = RunStatic(asm, "SciReg.RegTool", "NeedRegister", args);
                return Boolean.Parse(obj.ToString());
            }
            return false;
        }

        /// <summary>
        /// 判断当前是否为注册用户
        /// </summary>
        public static bool IsRegUser()
        {
            if (asm != null)
            {
                object[] args = new object[] { };
                Object obj = RunStatic(asm, "SciReg.RegTool", "IsRegUser", args);
                return Boolean.Parse(obj.ToString());
            }
            return false;
        }

        /// <summary>
        /// 获取后台配置的extKey对应的信息
        /// </summary>
        public static string ExtInfo(string extKey)
        {
            if (asm != null)
            {
                object[] args = new object[] { extKey };
                Object obj = RunStatic(asm, "SciReg.RegTool", "ExtInfo", args);
                return obj.ToString();
            }
            return "";
        }

        #endregion


        public delegate void Func();
        private static Func thisCall = null;
        public static void MethodF()
        {
            if (thisCall != null) thisCall();
        }

        /// <summary>
        /// 构建assembly中申明的Delegate对象
        /// </summary>
        private static Delegate NewDelegate(Assembly assembly)
        {
            //string name = Assembly.GetCallingAssembly().GetName().Name;                             // 获取当前程序集名称 RegDemo
            //Type methodF = Assembly.GetCallingAssembly().GetType(name + ".RegTool", true, true);    // RegDemo.RegTool类
            Type methodF = Assembly.GetCallingAssembly().GetType("Sci.RegTool", true, true);        // Sci.RegTool类
            MethodInfo info = methodF.GetMethod("MethodF");                                         // 获取方法MethodF

            // 使用当前类的Method函数创建一个deleget,用于接收dll执行回调
            Type typeCall = assembly.GetType("SciReg.RegTool+Func", true, true);
            Delegate d = Delegate.CreateDelegate(typeCall, info);
            return d;
        }

        #region 注册插件

        public static byte[] GetByte()
        {
            string data_run = getData("https://scimence.gitee.io/scitools/DATA/SciReg.data");
            byte[] bytes = ToBytes(data_run);
            return bytes;
        }

        /// <summary>  
        /// 解析字符串为Bytes数组
        /// </summary>  
        private static byte[] ToBytes(string data)
        {
            byte[] B = new byte[data.Length / 2];
            char[] C = data.ToCharArray();

            for (int i = 0; i < C.Length; i += 2)
            {
                byte b = ToByte(C[i], C[i + 1]);
                B[i / 2] = b;
            }

            return B;
        }

        /// <summary>  
        /// 每两个字母还原为一个字节  
        /// </summary>  
        private static byte ToByte(char a1, char a2)
        {
            return (byte)((a1 - 'a') * 16 + (a2 - 'a'));
        }

        /// <summary>
        /// 从指定dataUrl载入数据，并在本地缓存
        /// </summary>
        /// <param name="dataUrl"></param>
        /// <returns></returns>
        private static string getData(string dataUrl)
        {
            string data = "";
            try
            {
                string fileName = dataUrl.Substring(dataUrl.LastIndexOf("/") + 1);
                string localPath = AppDomain.CurrentDomain.BaseDirectory + fileName;

                // 优先从本地载入数据
                if (File.Exists(localPath))
                {
                    data = File.ReadAllText(localPath).Trim();
                    if (data.Trim().Equals("")) File.Delete(localPath);
                }

                // 若本地无数据，则从网址加载
                if (!File.Exists(localPath))
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    data = client.DownloadString(dataUrl).Trim();

                    File.WriteAllText(localPath, data);     // 本地缓存
                }

            }
            catch (Exception) { }
            return data;
        }

        #endregion
    }
}