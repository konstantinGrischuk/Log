using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace logClass.Tests
{
    [TestClass()]
    public class LogTests
    {

        [TestMethod()]
        public void Error()
        {
            Log log = new Log("Error");
            log.Error("Error");
            string path = log.GetLogName();
            log.Dispose();
            string line = File.ReadLines(path).ToArray()[0];
            Assert.IsTrue(line.Contains("Error"));
            File.Delete(path);
        }
        [TestMethod()]
        public void Write()
        {
            Log log = new Log("Write");
            log.Write("Write");
            string path = log.GetLogName();
            log.Dispose();
            string line = File.ReadLines(path).ToArray()[0];
            Assert.IsTrue(line.Contains("Write"));
            File.Delete(path);
        }
        [TestMethod()]
        public void Info()
        {
            Log log = new Log("Info");
            log.Info("Info");
            string path = log.GetLogName();
            log.Dispose();
            string line = File.ReadLines(path).ToArray()[0];
            Assert.IsTrue(line.Contains("Info"));
            File.Delete(path);
        }


    }
}