using Mono.Unix.Native;
using System.Diagnostics;

namespace FredsSubmarineControlServer
{
    public class Camera
    {
        private readonly string devicePath;
        Process? streamingProcess, stillImageProcess;
        public Camera(string devicePath = "/dev/video0")
        {
            this.devicePath = devicePath;
            StartStreaming();
        }

        public void InterruptStreamingAndTakeStillImage()
        {
            StopStreaming();
            TakeStillImage();
            StartStreaming();
        }

        void StartStreaming()
        {
            streamingProcess = new Process();
            streamingProcess.StartInfo.FileName = "/bin/bash";
            streamingProcess.StartInfo.Arguments = "streamcamera.sh " + devicePath;
            streamingProcess.StartInfo.CreateNoWindow = true;
            streamingProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            streamingProcess.Start();
        }

        void StopStreaming()
        {
            Syscall.kill(streamingProcess.Id, Signum.SIGINT);
            if (!streamingProcess.WaitForExit(3000))
            {
                streamingProcess.Kill();
            }
            streamingProcess.WaitForExit();
        }

        void TakeStillImage()
        {
            stillImageProcess = new Process();
            stillImageProcess.StartInfo.FileName = "/bin/bash";
            stillImageProcess.StartInfo.Arguments = "captureimage.sh " + devicePath;
            stillImageProcess.StartInfo.CreateNoWindow = true;
            stillImageProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            stillImageProcess.Start();
            if (!stillImageProcess.WaitForExit(5000))
            {
                stillImageProcess.Kill();
            }
        }
    }
}
