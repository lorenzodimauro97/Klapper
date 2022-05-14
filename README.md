# Klapper
Klapper is a free, open-source and not Vue-based Klipper web interface for managing your 3d printer.

# Why
Because I don't know Vue and I really don't want to learn it. This is also a great opportunity to learn a bit more on how to develop a web application of this relatively small magnitude, and a great opportunity to learn Blazor, too!

# What's the end goal here?
The idea is to implement everything Fluidd and Moonraker has, plus:
- Custom 3D GCode viewer with realtime nozzle position

# What's missing, currently?
- A lot of simple information (Eg: Number of layers, Flow Rate)
- A lot of controls (Eg: Extruder / Motor force)
- Config editing
- Webcam interface
- 3D GCode viewer with realtime nozzle position
- Probably something else I'm missing

# How to run?
The ASP.NET Core Based Application is self-contained and does not require any further Runtime or SDK installation to do it's job.
Before starting the server ensure to edit the appsettings.json to reflect your current running moonraker server's IP address (Eg: 192.168.xxx.xxx)

On Windows, simply run Klapper.exe and let the Kestrel server do the magic!

To run in Linux (in this example a Raspberrry Pi), simply run:
```./Klapper```

If you want to make Klapper start automatically when your Pi starts, you can create a systemd service.

```sudo nano /etc/systemd/system/klapper.service```

Add the snippet below. You'll have to change your working directory. I.E.

```
[Unit]
Description=Starts Klapper Kestrel server on startup

[Service]
WorkingDirectory=/home/pi/klapper
ExecStart=/home/pi/klapper/Klapper
Restart=always

#Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=klapper-identifier
User=pi

[Install]
WantedBy=multi-user.target
``` 

# Enable the Klapper service
```sudo systemctl enable klapper.service```

# Start the Klapper service
```sudo systemctl start klapper.service```

and it will start listening on ports 5000 and 5001 (for HTTPS).
