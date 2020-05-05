# DiscordServices.cs
C# api lib for the website

# How to use
Install the NuGet package here https://www.nuget.org/packages/DiscordServices.cs

Create an instance of the client, it is encouraged to not put tokens in this and instead load a file with the tokens incase you leak all your tokens
```
public static DServicesClient DServices;

DServicesClient = new DServicesClient(DiscordClient, Token);
```
You can automatically post your server count in the background with this (every 10 minutes)

> DServices.Start();

This stops the background task

> DServices.Stop();

Or you can post manual requests using

> DServices.PostServerCount();

> DServices.PostNews(new News { title = "News!", content = "Yay" } );

> DServices.PostCommands(List<Command> commands); 
