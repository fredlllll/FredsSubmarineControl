using Iot.Device.Pwm;
using System.Device.Gpio;
using System.Device.I2c;

namespace FredsSubmarineControlServer
{
    internal class Program
    {
        static Camera camera;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            camera = new Camera();


            
        }
    }
}