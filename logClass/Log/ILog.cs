using System.Threading.Tasks;

namespace logClass
{
    public interface ILog
    {
        event Log.LogEvent OnError;
        event Log.LogEvent OnInfo;
        event Log.LogEvent OnWrite;

        void Dispose();
        Task Error(string s);
        Task Info(string s);
        Task Write(string s);
    }
}