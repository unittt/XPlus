set WORKSPACE=..

set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set CONF_ROOT=%WORKSPACE%\DesignerConfigs

dotnet %LUBAN_DLL% ^
    -t all ^
    -c cs-bin ^
    -d bin  ^
    --schemaPath %CONF_ROOT%\Defines\__root__.xml ^
    -x inputDataDir=%CONF_ROOT%\Datas ^
    -x outputCodeDir=%WORKSPACE%/Client/Assets/Scripts/Gen ^
    -x outputDataDir=%WORKSPACE%\Client\Assets\GameRes\GenerateDatas\bytes ^
    -x pathValidator.rootDir=%WORKSPACE%\Client ^
	-x l10n.textProviderFile=*@%WORKSPACE%\DesignerConfigs\Datas\l10n\texts.json

pause