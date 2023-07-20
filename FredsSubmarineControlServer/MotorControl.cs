using Iot.Device.Pwm;
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.Pwm;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FredsSubmarineControlServer
{
    public class MotorControl
    {
        I2cBus i2cBus;
        I2cDevice i2cDevice;

        Pca9685 pwmController;

        PwmChannel motorTop;
        double topMul = 1;
        PwmChannel motorBottom;
        double bottomMul = 1;
        PwmChannel motorLeft;
        double leftMul = 1;
        PwmChannel motorRight;
        double rightMul = 1;

        private double throttle = 0;
        public double Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                throttle = value;
                motorTop.DutyCycle = topMul * throttle;
                motorBottom.DutyCycle = bottomMul * throttle;
                motorLeft.DutyCycle = leftMul * throttle;
                motorRight.DutyCycle = rightMul * throttle;
            }
        }

        public Vector3 Direction { get; set; }

        public MotorControl()
        {
            i2cBus = I2cBus.Create(1);
            i2cDevice = i2cBus.CreateDevice(0);

            //servos use 50-200Hz
            pwmController = new Pca9685(i2cDevice, 200);

            motorTop = pwmController.CreatePwmChannel(0);
            motorBottom = pwmController.CreatePwmChannel(1);
            motorLeft = pwmController.CreatePwmChannel(2);
            motorRight = pwmController.CreatePwmChannel(3);
        }

        

        
    }
}
