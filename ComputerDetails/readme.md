# About

An easy-to-follow example for creating a dotnet tool which presents a computer name, is a VPN active if Microsoft Visual Studio is installed and lists dotnet runtime.

For advance dotnet tools code samples check out my article with source code [C# .NET Tools with System.CommandLine](https://dev.to/karenpayneoregon/c-net-tools-withsystemcommandline-2nc2).



## Commands: install, uninstall

```
dotnet tool install --global --add-source ./nupkg computerdetails
dotnet tool uninstall -g computerdetails
```

	