using System.Diagnostics;

namespace Homework_5.MemoryChecker
{
    public class MemoryChecker: IMemoryChecker
    {
        private readonly Process currentProcess;
        public MemoryChecker()
        {
            currentProcess = Process.GetCurrentProcess();
        }
        public string ApplicationUsedMemory()
        {
            currentProcess.Refresh();
            return "Память, использующаяся приложением " + currentProcess.ProcessName + ": " + currentProcess.WorkingSet64 / 1_024;
        }
    }
}
