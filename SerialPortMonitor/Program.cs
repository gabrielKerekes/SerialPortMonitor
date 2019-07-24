using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace SerialPortMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Arguments: port, baudrate, parity.");
            Console.WriteLine("Example: mono SerialPortMonitor.exe /dev/ttyBUS0 19200 None/Even/Odd/Mark/Space");
            Console.WriteLine();

            var port = new SerialPort();
            var portName = args.Length > 0 ? args[0] : "/dev/ttyBUS0";
            var baudRate = args.Length > 1 ? int.Parse(args[1]) : 9600;
            var parity = args.Length > 2 ? (Parity) Enum.Parse(typeof(Parity), args[2]) : Parity.None;

            Console.WriteLine($"Port: {portName}");
            Console.WriteLine($"Baudrate: {baudRate}");
            Console.WriteLine($"Parity: {parity}");

            port.PortName = portName;
            port.BaudRate = baudRate;
            port.Parity = parity;
            port.StopBits = StopBits.One;

            port.Open();

            while (true)
            {
                var bytesToRead = port.BytesToRead;
                var bytes = new byte[bytesToRead];
                var numberOfBytes = port.Read(bytes, 0, bytesToRead);

                if (bytes.Length > 0)
                {
                    Console.Write(ByteArrayToString(bytes));
                    Console.WriteLine();
                }

                Thread.Sleep(500);
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2} ", b);
            return hex.ToString();
        }
    }
}
