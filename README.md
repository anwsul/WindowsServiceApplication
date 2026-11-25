# WindowsServiceApplication

> A service is a long-running executable that does not support a user interface, and which might not run under the logged-on user account.

.NET Framework can be used to develop windows services. Unlike a Console application that we can just build and run, services need a little more work. If we were to just run the application directly (say, by double clicking it), it will throw an error.

To create a service, we first need to create a class that inherits from `System.Services.ServiceBase`. This base class, among other things, contains the signatures for the methods used to interact with a service (`OnStart`, `OnStop`...).

```csharp
class OurService : ServiceBase {
	....
}
```

We then write the code our service needs to perforom in this newly created class. Inside the callback methods (like `OnStart`) that are inherited, we write the logic that we need to happen when that event occurs (ex. when the service starts).

Like any kind of program, an application running services will also need an entry point (the `static Main` method). Let's assume that the purpose of our application is solely for running services. In that case, it's logical to assume the main method should run our services.

```csharp
ServiceBase.Run(ListOfServices) // or
ServiceBase.Run(SingleService)
```

Now we have two options to run our application and register our services. Using `sc`(Service Control) command line tool that comes with windows itself, or using `installutil` tool that is used to install any applications, not just services, and comes with the .NET framework.

If we were to use `installutil`, we need additional steps, which means creating classes that inherit from `System.Configuration.Install.Installer` class, and configuring an instance of both `System.ServiceProcess.ServiceProcessInstaller` (for configuring common things for the different services that our application might have, like the user account they use to run) and `System.ServiceProcess.ServiceInstaller` (for configuring the individual services, like their service name, and whether they should start automatically)

if we were to use `sc` however, we just need to build our application without the extra installer class.

The usage for both `sc` and `installutil` can be found by just running them on the terminal. Incase `installutil` throws an error saying it couldn't be found, make sure it is added in the list of path environment variables.
