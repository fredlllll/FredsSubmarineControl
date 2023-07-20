#for usb cam
ffmpeg -i $1 -vf scale=640:480 -preset ultrafast -vcodec libx264 -tune zerolatency -b:v 900k -f mpegts -omit_video_pes_length 0 -g 10 tcp://0.0.0.0:10000?listen