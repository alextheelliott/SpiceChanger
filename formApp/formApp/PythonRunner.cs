using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PythonTest
{
    public class PythonRunner
    {
        private Process _process;

        public PythonRunner(String script) 
        { 
            _process = new Process();
            _process.StartInfo.FileName = "py";
            _process.StartInfo.Arguments = script;
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardInput = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.CreateNoWindow = true;
        }

        public void Start(DataReceivedEventHandler callback)
        {
            _process.OutputDataReceived += callback;

            _process.Start();
            _process.BeginOutputReadLine();
        }

        public void Send(string msg)
        {
            _process.StandardInput.WriteLine(msg);
            _process.StandardInput.Flush();
        }
    }

}
