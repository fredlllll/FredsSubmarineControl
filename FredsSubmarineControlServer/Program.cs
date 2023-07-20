using Iot.Device.Pwm;
using System.Device.Gpio;
using System.Device.I2c;

namespace FredsSubmarineControlServer
{
    internal class Program
    {
        static Camera? camera;
        static PwmMotorControl? motorControl;

        static void Main(string[] args)
        {
            camera = new Camera();
            motorControl = new PwmMotorControl();

            //TODO: server for receiving control messages
        }
    }
}