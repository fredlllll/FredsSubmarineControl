using System;
using System.Collections.Generic;
using System.Device.Pwm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FredsSubmarineControlServer
{
    public class Motor
    {
        private readonly PwmChannel channel;
        private double throttle = 0;
        private bool reverse = false;

        public Motor(PwmChannel channel)
        {
            this.channel = channel;
        }

        private bool Reverse
        {
            get { return reverse; }
            set
            {
                reverse = value;
                //TODO: somehow reverse the motor??
            }
        }


        public double Throttle
        {
            get { return Reverse ? -throttle : throttle; }
            set
            {
                if (value < 0)
                {
                    throttle = -value;
                    Reverse = true;
                }
                else
                {
                    throttle = value;
                    Reverse = false;
                }
                channel.DutyCycle = throttle;
            }
        }
    }
}
