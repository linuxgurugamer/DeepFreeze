
rem @echo off

rem H is the destination game folder
rem GAMEDIR is the name of the mod folder (usually the mod name)
rem GAMEDATA is the name of the local GameData
rem VERSIONFILE is the name of the version file, usually the same as GAMEDATA,
rem    but not always

set H=%KSPDIR%

set GAMEDATA=GameData
set REPODIR=REPOSoftTech
set GAMEDIR=DeepFreeze
set VERSIONFILE=%GAMEDIR%Continued.version
set PLUGINS="%GAMEDATA%\%REPODIR%\%GAMEDIR%\Plugins"

set DP0=r:\dp0\kspdev

copy /Y "%1%2" "%PLUGINS%"
copy /Y "%1%3".pdb "%PLUGINS%""

copy /Y %VERSIONFILE% %GAMEDATA%\%GAMEDIR%

xcopy /y /s /I %GAMEDATA%\%REPODIR% "%H%\GameData\%REPODIR%"
xcopy /y /s /I %GAMEDATA%\%REPODIR% "%DP0%\GameData\%REPODIR%"

rem pause
