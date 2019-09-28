<h1 align="center"> AppExcluder </h1> <br>

this App very useful for make Exception for a appliction that use a specific Network Adapter.
it can used for direct connect (without VPN) to a network adapter.
* [This Program Use ForceBindIP As a Core](https://r1ch.net/projects/forcebindip)

<p align="center">
  <img alt="AppExcluder" title="AppExcluder" src="https://github.com/porya-gohary/AppExcluder/blob/master/AppExcluder.png" >
</p>

NOTICE: THIS PROGRAM IS UNDER DEVELOPMENT...

## Getting Started

### Prerequisites

For Using this Program your system required the following packages:

* [.NET Framework 4.6](https://www.microsoft.com/en-gb/download/details.aspx?id=48130)
* [Visual Studio 2015 Runtimes (x86 and x64)](https://www.microsoft.com/en-us/download/details.aspx?id=52685)


### Installing

First Download Application From link below and then extract it somewhere.

* [AppExcluder-Ver1.0](https://porya-gohary.ir/AppExcluder-v1.0.zip)

## Running

For running program run "AppExcluder.exe" and then select one of your Network Adapter that you want used for routing. now select your application from "Select Application" Button. then you can use "RUN!" Button for run it and "Make Shortcut" For making a fast shortcut.

### NOTICE!
the following part adapted from [ForceBindIP](https://r1ch.net/projects/forcebindip)

#### Google Chrome Compatibility
Chrome requires additional configuration to run under ForceBindIP. This is because Chrome 72 or later blocks 3rd party programs from injecting DLLs. To allow ForceBindIP to work, install this [enterprise policy registry file](https://r1ch.net/assets/forcebindip/Chrome72.reg) to re-enable DLL injection, then open Chrome and go to
```
chrome://flags/#network-service-in-process
```
and enable the setting (Chrome 76+) or
```
chrome://flags#network-service
```
and disable the setting (Chrome 75-).

##### Firefox Compatibility
Firefox requires the
```
about:config?filter=browser.launcherProcess.enabled
```
preference set to false, otherwise ForceBindIP attaches to the launcher and not the actual program.

## Built With

* C#
* C++


## License
Copyright © 2019 [Porya Gohary](porya-gohary.ir)
This project is licensed under the Apache License 2.0 - see the [LICENSE.md](LICENSE.md) file for details
