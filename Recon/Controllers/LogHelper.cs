using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Recon.Controllers
{
    public class LogHelper
    {
        public enum LogTypes
        {
            /// <summary>
            /// Informs the Log Engine that the Given Log Message is "Exception" Type.
            /// </summary>
            Exception,
            /// <summary>
            /// Informs the Log Engine that the Given Log Message is "Exception" Type.
            /// </summary>
            Message,
            /// <summary>
            /// Informs the Log Engine that the Given Log Message is "Exception" Type.
            /// </summary>
            Warning,
        }

        private static PerformanceCounter cpuCounter;
        private static PerformanceCounter ramCounter;
        private static PerformanceCounter pfCounter;
        private static void InitilizeCPUCounter()
        {
            try
            {
                if (cpuCounter == null)
                    cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            }
            catch (Exception) { }
        }
        private static void InitilizeRAMCounter()
        {
            try
            {
                if (ramCounter == null)
                    ramCounter = new PerformanceCounter("Memory", "Available MBytes", true);
            }
            catch (Exception) { }
        }
        private static void InitilizePFCounter()
        {
            try
            {
                if (pfCounter == null)
                    pfCounter = new PerformanceCounter("Paging File", "% Usage", "_Total", Environment.MachineName);
            }
            catch (Exception) { }
        }
        /// <param name="sMessage">Actual Log Message to be Written</param>
        public static void WriteLog(string LogMessage,string control, LogTypes lgType = LogTypes.Message)
        {
            //LogMessage = ConstructMessage(lgType, LogMessage, new StackFrame(1, true));
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //InitilizeCPUCounter(); InitilizePFCounter(); InitilizeRAMCounter();
            try
            {
                #region Message Formation
                if (LogMessage.Replace("-", "").Length > 0)
                {
                    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                    try
                    {
                        if (cpuCounter != null && ramCounter != null && pfCounter != null)
                        {
                            LogMessage = DateTime.Now.ToString() + "|"
                                + Convert.ToInt32(cpuCounter.NextValue()).ToString() + "%" + "|"
                                + Convert.ToInt32(ramCounter.NextValue()).ToString() + "MB" + "|"
                                + String.Format("{0:##0}%", pfCounter.NextValue()) + "|"
                                + "(Thrd ID:" + managedThreadId + ")|"
                                + LogMessage;
                        }
                        else
                        {
                            LogMessage = DateTime.Now.ToString() + "|"
                            + "0%" + "|"
                            + "0MB" + "|"
                            + String.Format("{0:##0}%", 0) + "|"
                            + "(Thrd ID:" + managedThreadId + ")|"
                            + LogMessage;
                        }
                    }
                    catch (Exception)
                    {
                        LogMessage = DateTime.Now.ToString() + "|"
                            + "0%" + "|"
                            + "0MB" + "|"
                            + String.Format("{0:##0}%", 0) + "|"
                            + "(Thrd ID:" + managedThreadId + ")|"
                            + LogMessage;
                    }
                }
                #endregion
                //Console.WriteLine(DateTime.Now.ToString() + " : " + LogMessage.Substring(LogMessage.LastIndexOf('|') + 1));
                log.Info(DateTime.Now.ToString() + "-" + LogMessage + control);
            }
            catch (Exception)
            {
                //Console.WriteLine("Exception : " + ex);
            }
        }
        private static string ConstructMessage(LogTypes lgType, string Message, StackFrame CallingStackFrame)
        {
            var method = CallingStackFrame.GetMethod();
            var type = method.DeclaringType;
            var name = method.Name;
            string CallingSource = type.FullName.ToString() + "." + name + "()|Line :" + CallingStackFrame.GetFileLineNumber(); ;
            string FormedMessage = string.Empty;
            FormedMessage = CallingSource + "|" + lgType.ToString() + "|" + Message;
            return FormedMessage;
        }
    }
}
