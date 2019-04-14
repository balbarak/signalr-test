# Introduction 
This repos is to help address signalr xamarin client issue where sometimes the cient get disconnected for unknown reason after sending a continuse message to a group every 1 second

# How to use
The source contains 3 projects

* `SingalrTest.Server.sln` aspnet core 2.2 server which host signalr
* `SingalrTest.TestClient.sln` a console app that send a message every second
* `SignalrTest.Android.sln` android project which recieves the messages from test client and the issue happend here

in order to run the project follow the instructions

* Open and run the server in `SignalrTest.Server.sln`
* Open `SignalrTest.Android.sln` and edit the `URL` found in the class `AppClient` in `SignalrTest.Common` project to match your intranet IP address remember to keep the port `5000` 
* Run `SingalrTest.Android.sln` and click `Connect` and you should see `Connected` in the Status label
* Open and run `SignalrTest.TestClient.sln` to keep sending message to the client

After a short while a xamarin client get diconnected for unkown reason

if you uncomment `AddMessagePackProtocol()` in the `SetupClient()` method found in class `AppClient` the client never get disconnected.
