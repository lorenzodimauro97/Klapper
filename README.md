# Klapper
Klapper is a free, open-source and not Vue-based Klipper web interface for managing your 3d printer.

![image](https://user-images.githubusercontent.com/50343905/168448853-52b39bcb-264e-4da1-8457-631693d31e78.png)
![image](https://user-images.githubusercontent.com/50343905/169483045-e6efe1ab-a683-41ac-812c-2764ce1acdac.png)

# Why
Because I don't know Vue and I really don't want to learn it. This is also a great opportunity to learn a bit more on how to develop a web application of this relatively small magnitude, and a great opportunity to learn Blazor, too!

# What's the end goal here?
The idea is to implement everything Fluidd and Moonraker has, plus:
- Custom 3D GCode viewer with realtime nozzle position
- Custom Tools to automate Calibrating your printer (eg: Manual Leveling Tool, PID Calibration Tool, etc)

# What's missing, currently?
- A a couple simple information (Eg: ~~Number of layers~~, Flow Rate)
~~- A lot of controls (Eg: Extruder / Motor force)~~
~~-	Config Editing~~
- Webcam interface
- 3D GCode viewer with realtime nozzle position
- Probably something else I'm missing

# How to run?
The ASP.NET Core Based Application is self-contained and does not require any further Runtime or SDK installation to do it's job.

by default, the appsettings.json is configured to allow both HTTP and HTTPS connections, but to allow HTTPS connections, you need a certificate.

It is possible to generate a certificate using the command "dotnet dev-certs https" followed by the command "dotnet dev-certs https --trust" (.NET SDK 6 IS required for this operation, and it's fairly easy to install on both Windows and Linux environments).

If you just don't care about running with HTTPS, you may remove the Https endpoint from the appsettings.json file, so it looks like this:
![image](https://user-images.githubusercontent.com/50343905/168447046-20bd6fe4-963e-4bbc-bd8e-487c9de710b4.png)

WARNING: For Klapper to run properly, it is imperative that nginx request timeout be set to a much more suitable number than 60 seconds, otherwise some Tools may not properly work!

This is how I expect your /etc/nginx/nginx.conf to be:

![image](https://user-images.githubusercontent.com/50343905/170728932-35a710a7-bf5b-4db7-82ea-51790bd99d16.png)

On Windows, simply run Klapper.exe and let the Kestrel server do the magic!

To run in Linux, simply run ./Klapper or if you want to make Klapper start automatically when Linux starts, you can create a systemd service using the following Template:

![image](https://user-images.githubusercontent.com/50343905/168447083-3fefeb7a-a54b-4c1c-a459-4e9260ab07c8.png)

#Systemd service file for Klapper
[Unit]
Description=Starts Klapper Kestrel server on startup
[Service]
 WorkingDirectory=$INSERTKLAPPERDIRECTORYHERE
ExecStart=$INSERTKLAPPERDIRECTORYHERE/Klapper
Restart=always
#Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=klapper-identifier
User=$INSERTUSERNAMEHERE
[Install]
WantedBy=multi-user.target

and it will start listening on ports 5000 and 5001 (for HTTPS, if enabled).
