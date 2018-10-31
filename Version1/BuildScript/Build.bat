@ECHO ===========================================================================================================
@ECHO IHE Build Script - START
@ECHO Run this batch file from Visual Studio 2005 Command Prompt.
@ECHO Refer IHE deployment guide document for more information.
@ECHO Build log files will be generated for each project.
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\IHE\IHECommon\IHEXDSCommon.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=IHEXDSCommon.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\DataAccess\DataAccess.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=DataAccess.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\BusinessLogic\BusinessLogic.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=BusinessLogic.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\IHE\ATNA\ATNA.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=ATNA.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\IHE\StorageProvider\StorageProvider.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=StorageProvider.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\IHE\RepositoryProfile\DocumentRepository.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=DocumentRepository.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\IHE\RegistryProfile\DocumentRegistry.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=DocumentRegistry.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\WindowsServices\XDSRepositoryServiceHost\XDSRepositoryServiceHost\XDSDocumentRepositoryServiceHost.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=XDSDocumentRepositoryServiceHost.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\WindowsServices\XDSRegistryServiceHost\XDSRegistryServiceHost.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=XDSRegistryServiceHost.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
MSBuild "..\ScriptedInstaller\ScriptedInstaller.csproj" /t:Rebuild /clp:NoItemAndPropertyList /p:Configuration=Release /logger:FileLogger,Microsoft.Build.Engine;LogFile=ScriptedInstaller.csproj.BuildLog.log /v:q
@ECHO ===========================================================================================================

@ECHO ===========================================================================================================
@ECHO IHE Build Script - END
@ECHO Run this batch file from Visual Studio 2005 Command Prompt.
@ECHO Refer IHE deployment guide document for more information.
@ECHO Build log files will be generated for each project.
@ECHO ===========================================================================================================












