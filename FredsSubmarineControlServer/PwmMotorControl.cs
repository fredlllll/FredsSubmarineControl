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
    public class PwmMotorControl
    {
        readonly I2cBus i2cBus;
        readonly I2cDevice i2cDevice;

        readonly Pca9685 pwmController;

        readonly PwmMotor motorTop;
        readonly PwmMotor motorBottom;
        readonly PwmMotor motorLeft;
        readonly PwmMotor motorRight;
        readonly PwmMotor motorReverse;

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
                if (throttle >= 0)
                {
                    motorTop.SetThrottle(throttle);
                    motorBottom.SetThrottle(throttle);
                    motorLeft.SetThrottle(throttle);
                    motorRight.SetThrottle(throttle);
                    motorReverse.SetThrottle(0);
                }
                else
                {
                    //only straight in reverse for now
                    motorTop.SetThrottle(0);
                    motorBottom.SetThrottle(0);
                    motorLeft.SetThrottle(0);
                    motorRight.SetThrottle(0);
                    motorReverse.SetThrottle(-throttle);
                }
            }
        }

        private Vector3 direction = VectorConstants.forward;

        public Vector3 Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = Vector3.Normalize(value);

                float leftness = MathF.Max(0, Vector3.Dot(direction, VectorConstants.left));
                float rightness = MathF.Max(0, Vector3.Dot(direction, VectorConstants.right));
                float upness = MathF.Max(0, Vector3.Dot(direction, VectorConstants.up));
                float downness = MathF.Max(0, Vector3.Dot(direction, VectorConstants.down));
                float forwardness = MathF.Max(0, Vector3.Dot(direction, VectorConstants.forward));

                motorLeft.ThrottleMultiplier = forwardness + rightness;
                motorRight.ThrottleMultiplier = forwardness + leftness;
                motorTop.ThrottleMultiplier = forwardness + downness;
                motorBottom.ThrottleMultiplier = forwardness + upness;
            }
        }

        public PwmMotorControl()
        {
            i2cBus = I2cBus.Create(1);
            i2cDevice = i2cBus.CreateDevice(0);

            //servos use 50-200Hz
            pwmController = new Pca9685(i2cDevice, 200);

            motorTop = new PwmMotor(pwmController.CreatePwmChannel(0));
            motorBottom = new PwmMotor(pwmController.CreatePwmChannel(1));
            motorLeft = new PwmMotor(pwmController.CreatePwmChannel(2));
            motorRight = new PwmMotor(pwmController.CreatePwmChannel(3));
            motorReverse = new PwmMotor(pwmController.CreatePwmChannel(4));
        }
    }
}
