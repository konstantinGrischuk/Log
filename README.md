Класс лога, написан как класс, библиотека или консольное приложение(для теста).
Лог имеет несколько режимов работы:
 1. Файловый (LogType.File)
 2. EventLog (LogType.Event)
 3. Файловый и EventLog (LogType.Both)

вНИМАНИЕ! EventLog требует прав администратора для регистрации журнала. При отсутствии прав, запись ведется в журнал Applications / Приложения.

Лог имеет 4 конструктора, которые могут быть объявлены в любом месте. 
Объявления конструкторов:
 1.  Log log = new Log();
 2.  Log log = new Log("LogName");
 3.  Log log = new Log(LogType.File);
 4.  Log log = new Log("LogName", LogType.File);

При создании лога без параметров, создается лог с именем текущей переменной.

 class Program
    {
        static readonly Log log = new Log();

        static async Task Main()
        {
            log.Write("Write");
        }
    
 Имя лога log.
             
 
// class Program
//    {
//        static readonly Log log = new Log();

//        static async Task Main()
//        {
//            log.Write("Write");
//            log.Info("Info");
//            log.Error("Error");

//            Log l1 = new Log();
//            await l1.Write("Write");

//            Log l2 = new Log("log2");
//            await l2.Write("Write");

//            Log l3 = new Log("log 3", LogType.File);
//            await l3.Write("Write");

//            //Log l4 = new Log(LogType.File);
//            //await l4.Write("Write 2");

//            await LogManager.Write("Write");

//            foreach (var v in LogManager.Log_List)
//                System.Console.WriteLine(v.FileName);

//            System.Console.WriteLine(LogManager.Log_List.Count.ToString());
          
//            for (int i= LogManager.Log_List.Count-1; i>=0;i--)
//             LogManager.Log_List[i].Dispose();
          

//            foreach (var v in LogManager.Log_List)
//                System.Console.WriteLine(v.FileName);

//            System.Console.WriteLine(LogManager.Log_List.Count.ToString());

//            System.Console.ReadKey();
