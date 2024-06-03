using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Timers;

[assembly: InternalsVisibleTo("Program.XmlSerializers")]
namespace Dane
{
    internal class Logger
    {
        private ConcurrentQueue<IBall> ballsQueue;
        private string filename;
        //private CancellationTokenSource StateChange = new CancellationTokenSource();
        private System.Timers.Timer timer;
        bool isRunning;

        public Logger()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            filename = Path.Combine(path, "Logger.xml");
            ballsQueue = new ConcurrentQueue<IBall>();
            this.isRunning = true;
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            //Task.Run(writeDataToLogger);
        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            writeDataToLogger();
        }
        public void addToQueue(IBall ball)
        {
            ballsQueue.Enqueue(ball);
            //StateChange.Cancel();
        }

        public async void writeDataToLogger()
        {
            while (this.isRunning)
            {
                if (!ballsQueue.IsEmpty)
                {
                    while (ballsQueue.TryDequeue(out IBall ball))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(filename, true))
                        using (XmlWriter writer = XmlWriter.Create(streamWriter, new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true }))
                        {
                            DataContractSerializer xmlSer = new DataContractSerializer(typeof(Ball));
                            xmlSer.WriteObject(writer, ball);
                        }
                       // await Task.Delay(Timeout.Infinite, StateChange.Token).ContinueWith(_ => { });
                    }
                }
            }
        }

    }
}