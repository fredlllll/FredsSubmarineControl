using System;
using System.Collections.Generic;
using System.Device.Pwm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FredsSubmarineControlServer
{
    public class PwmMotor
    {
        private readonly PwmChannel channel;

        public PwmMotor(PwmChannel channel)
        {
            this.channel = channel;
        }

        double throttleMultiplier = 1;
        public double ThrottleMultiplier
        {
            get
            {
                return throttleMultiplier;
            }
            set
            {
                throttleMultiplier = Math.Max(0, Math.Min(1, value));
            }
        }

        public void SetThrottle(double throttle)
        {
            channel.DutyCycle = Math.Max(0, Math.Min(1, ThrottleMultiplier * throttle));
        }
    }
}
