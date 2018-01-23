using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SerialPortMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = new SerialPort();
            var portName = args.Length > 0 ? args[0] : "COM7";
            Console.WriteLine(portName);
            port.PortName = portName;
            port.BaudRate = 9600;
            port.Parity = Parity.None;

            //port.DataReceived += (sender, eventArgs) =>
            //{
            //    var sp = (SerialPort)sender;

            //    var bytesToRead = sp.BytesToRead;
            //    var bytes = new byte[bytesToRead];
            //    var indata = sp.Read(bytes, 0, bytesToRead);

            //    if (bytes.Length > 2 && bytes[1] == 6)
            //    {
            //        Console.Write(ByteArrayToString(bytes));
            //        Console.WriteLine();
            //    }
            //};

            port.Open();

            while (true)
            {
                //var bytesToRead = port.BytesToRead;
                //var bytes = new byte[bytesToRead];
                //port.Read(bytes, 0, bytesToRead);

                //Console.WriteLine(ByteArrayToString(bytes));

                //Thread.Sleep(500);
                //port.Write("ASDASD");
                //Thread.Sleep(500);
                var bytesToRead = port.BytesToRead;
                var bytes = new byte[bytesToRead];
                var indata = port.Read(bytes, 0, bytesToRead);

                //if (bytes.Length > 2 && bytes[1] == 6)
                //{
                    Console.Write(ByteArrayToString(bytes));
                    Console.WriteLine();
                //}

                Thread.Sleep(500);
            }
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2} ", b);
            return hex.ToString();
        }
    }
}
