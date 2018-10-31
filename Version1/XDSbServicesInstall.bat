@echo off

rem install services
installutil /account="user" /user=%1 /password=%2 ".\WindowsServices\XDSRepositoryServiceHost\XDSRepositoryServiceHost\bin\Debug\XDSDocumentRepositoryServiceHost.exe"
installutil /account="user" /user=%1 /password=%2 ".\WindowsServices\XDSRegistryServiceHost\bin\Debug\XDSRegistryServiceHost.exe"

rem start services
net start XDSDocumentRepositoryServiceHost
net start XDSDocumentRegistryServiceHost
