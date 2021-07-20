using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace logClass
{


    public class Log : IDisposable, ILog
    {
        internal string FileName = (new StackTrace(true).GetFrame(1).GetMethod().DeclaringType.Name).Replace("<", "").Replace(">", "");
        private long MAX_SIZE = 1024 * 1024 * 5;
        EventLog EventLog = new EventLog();
        internal string LOG_DIR = "Logs";
        private FileStream fs;
        internal string ext = ".LOG";
        private readonly LogType lt = LogType.File;
        public string TIME_PATERN = DateTimePatern.Short;

        public delegate void LogEvent(string message);
        public event LogEvent OnWrite;
        public event LogEvent OnInfo;
        public event LogEvent OnError;


        public Log([CallerMemberName] string fn = "")
        {
            FileName = fn;
            Init();

        }
        public Log(string fn, LogType LogType)
        {
            lt = LogType;
            FileName = fn;
            Init();
        }
        public Log(LogType LogType, [CallerMemberName] string fn = "")
        {
            lt = LogType;
            FileName = fn;
            Init();
        }


        public string GetLogName()
        {       
            return fs.Name;
        }





        private void Init()
        {
         
            LogManager.Log_List.Add(this);
            switch (lt)
            {
                case LogType.File:
                    {
                        FileType();
                        break;
                    }
                case LogType.Event:
                    {
                        EventType();
                        break;
                    }
                case LogType.Both:
                    {
                        FileType();
                        EventType();
                        break;
                    }
                default:
                    {
                        FileType();
                        break;
                    }
            }
        }

        private void FileType()
        {          
            try
            {
            
                LOG_DIR = Path.Combine(Application.StartupPath, "Logs");
                if (!Directory.Exists(LOG_DIR)) Directory.CreateDirectory(LOG_DIR);

                if (!File.Exists(Path.Combine(LOG_DIR, FileName + ext)))
                {
                    fs = new FileStream(Path.Combine(LOG_DIR, FileName + ext), FileMode.Create, FileAccess.Write);
                }
                else fs = new FileStream(Path.Combine(LOG_DIR, FileName + ext), FileMode.Append, FileAccess.Write);

                Check_size();
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void Check_size()
        {

            if (fs.Length > MAX_SIZE)
            {

                fs.Flush();
                fs.Close();
                File.Move(fs.Name, fs.Name + " (" + DateTime.Now.ToString("d MMMM yyyy HH.mm.ss") + ")" + Path.GetExtension(fs.Name));
                fs = new FileStream(Path.Combine(LOG_DIR, FileName), FileMode.CreateNew, FileAccess.Write);

            }
        }




        private async void EventType()
        {
            EventLog = new EventLog();
            try
            {

                if (!EventLog.SourceExists("Log"))
                {
                    EventLog.CreateEventSource("Log", Path.GetFileNameWithoutExtension(FileName));
                    EventLog.Source = "Log";
                }
                else
                {
                    EventLog.Source = "Log";
                }
            }
            catch (Exception er)
            {
                EventLog.Source = "Application";
                await Error(er.Message + "Для регистрации журнала необходим запуск с правами администратора");
            }
        }

        public async Task Write(string s)
        {
            if ((lt == LogType.File) | (lt == LogType.Both))
                try
                {
                    Check_size();
                    var msg = Encoding.Default.GetBytes(DateTime.Now.ToString(TIME_PATERN)/*+" [" + "] "*/ + s + Environment.NewLine);
                    await fs.WriteAsync(msg, 0, msg.Length);
                    OnWrite?.Invoke(s + Environment.NewLine);
                }
                catch (Exception er)
                {
                    await Error(er.Message);
                    OnError?.Invoke(s + Environment.NewLine);
                };
            if ((lt == LogType.Event) | (lt == LogType.Both))
                try
                {
                    EventLog.WriteEntry(s);
                    OnWrite?.Invoke(s + Environment.NewLine);
                }
                catch (Exception er)
                {
                    await Error(er.Message);
                    OnError?.Invoke(s + Environment.NewLine);
                }
        }
        public async Task Info(string s)
        {
            if ((lt == LogType.File) | (lt == LogType.Both))
                try
                {
                    Check_size();
                    var msg = Encoding.Default.GetBytes(DateTime.Now.ToString(TIME_PATERN) + "[INFO] " + s + Environment.NewLine);
                    await fs.WriteAsync(msg, 0, msg.Length);
                    OnInfo?.Invoke(s + Environment.NewLine);
                }
                catch (Exception er)
                {
                    await Error(er.Message);
                    OnError?.Invoke(s + Environment.NewLine);
                };
            if ((lt == LogType.Event) | (lt == LogType.Both))
                try
                {
                    EventLog.WriteEntry(s, EventLogEntryType.Information);
                    OnInfo?.Invoke(s + Environment.NewLine);
                }
                catch (Exception er)
                {
                    await Error(er.Message);
                    OnError?.Invoke(s + Environment.NewLine);
                }
        }
        public async Task Error(string s)
        {
            if ((lt == LogType.File) | (lt == LogType.Both))
                try
                {
                    Check_size();
                    var msg = Encoding.Default.GetBytes(DateTime.Now.ToString(TIME_PATERN) + "[ERROR] " + s + Environment.NewLine);
                    await fs.WriteAsync(msg, 0, msg.Length);
                    OnWrite?.Invoke(s + Environment.NewLine);
                }
                catch (Exception er)
                {
                    await Error(er.Message);
                    OnError?.Invoke(s + Environment.NewLine);
                };
            if ((lt == LogType.Event) | (lt == LogType.Both))
                try
                {
                    EventLog.WriteEntry(s, EventLogEntryType.Error);
                    OnWrite?.Invoke(s + Environment.NewLine);
                }
                catch (Exception er)
                {
                    await Error(er.Message);
                    OnError?.Invoke(s + Environment.NewLine);
                }
        }

        public void Dispose()
        {
          //  Console.WriteLine(FileName+" - уничтожен");
            FileName = null;
            MAX_SIZE = 0;
            EventLog.Dispose();
            LOG_DIR = null;
            //string fs_path = fs.Name;
            fs.Dispose();
          //  File.Delete(fs_path);
            ext = null;
            TIME_PATERN = null;
            LogManager.Log_List.Remove(this);

        }
    }
}