@echo off
rem stop services
net stop XDSDocumentRepositoryServiceHost
net stop XDSDocumentRegistryServiceHost

rem install services
installutil /u ".\WindowsServices\XDSRepositoryServiceHost\XDSRepositoryServiceHost\bin\Debug\XDSDocumentRepositoryServiceHost.exe"
installutil /u ".\WindowsServices\XDSRegistryServiceHost\bin\Debug\XDSRegistryServiceHost.exe"
