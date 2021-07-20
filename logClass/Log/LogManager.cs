using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace logClass
{
    public static class LogManager
    {
        public static List<Log> Log_List = new List<Log>();
        private static  readonly Log log = new Log("LogMgr");
         
        public static async Task Write(string mes, [CallerMemberName] string caller = "")
        {           
            Log l= Log_List.Find(x=> x.FileName == caller);
            if (l != null) await l.Write(mes);
            else
            {
                Log l_new = new Log(caller);
                await l_new.Write(mes);
                await log.Info("Лог "+caller+" найден, создан " + caller); 
            }
        }
        public static async Task Info(string mes, [CallerMemberName] string caller = "")
        {
            Log l = Log_List.Find(x => x.FileName == caller);
            if (l != null) await l.Info(mes);
            else
            {
                Log l_new = new Log(caller);
                await l_new.Write(mes);
                await log.Info("Лог " + caller + " найден, создан " + caller);
            }
        }
        public static async Task Error(string mes, [CallerMemberName] string caller = "")
        {
            Log l = Log_List.Find(x => x.FileName == caller);
            if (l != null) await l.Error(mes);
            else
            {
                Log l_new = new Log(caller);
                await l_new.Write(mes);
                await log.Info("Лог " + caller + " найден, создан " + caller);
            }
        }







    }
}
