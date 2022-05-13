# Klapper
Klapper is a free, open-source and not Vue-based Klipper web interface for managing your 3d printer.

# Why
Because I don't know Vue and I really don't want to learn it. This is also a great opportunity to learn a bit more on how to develop a web application of this relatively small magnitude, and a great opportunity to learn Blazor, too!

# What's the end goal here?
The idea is to implement everything Fluidd and Moonraker has, plus:
- Custom real-time 3D GCode Viewer with nozzle position

# What's missing, currently?
- A lot of simple information (Eg: Number of layers, Flow Rate)
- A lot of controls (Eg: Extruder / Motor force)
- Config editing
- Webcam interface
- 3D GCode viewer with realtime nozzle position
- Probably something else I'm missing

# How to run?
The ASP.NET Core Based Application is self-contained and does not require any further Runtime or SDK installation to do it's job.

On Windows, simply run Klapper.exe and let the Kestrel server do the magic!

To run in Linux, simply run ./Klapper or if you want to make Klapper start automatically when Linux starts, you can create a systemd service using the following Template:

#Systemd service file for Klapper
[Unit]
Description=Starts Klapper Kestrel server on startup
[Service]
 WorkingDirectory=$INSERTKLAPPERDIRECTORYHERE
ExecStart=$INSERTKLAPPERDIRECTORYHERE/Klapper
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=klapper-identifier
User=$INSERTUSERNAMEHERE
[Install]
WantedBy=multi-user.target

and it will start listening on ports 5000 and 5001 (for HTTPS).
