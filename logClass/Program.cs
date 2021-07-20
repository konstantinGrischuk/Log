//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace logClass
//{
//    class Program
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
//        }
//    }
//}
