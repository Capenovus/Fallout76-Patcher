@ECHO OFF
cls
title el script delicioso
ECHO Press 1 for Steam
ECHO Press 2 for Microsoft
SET /p choice=What version of Fallout76 do you have? 
if not '%choice%'=='' set choice=%choice:~0,1%
if '%choice%'=='1' goto steam
if '%choice%'=='2' goto microsoft
goto end
:steam
ECHO.
ECHO About to remove old SeventySix.esm from data folder, Please wait...
rd /s /q %localappdata%\Fallout76\associated_media\data
ECHO.
ECHO Copying SeventySix.esm to data directory
mkdir %localappdata%\Fallout76\associated_media\data
xcopy /s /q "SeventySix.esm" %localappdata%\Fallout76\associated_media\data
goto end
:microsoft
ECHO.
ECHO About to remove old SeventySix.esm from data folder, Please wait...
rd /s /q %localappdata%\Project76\associated_media\data
ECHO.
ECHO Copying SeventySix.esm to data directory
mkdir %localappdata%\Project76\associated_media\data
xcopy /s /q "SeventySix.esm" %localappdata%\Project76\associated_media\data
goto end
:end
pause
EXIT


:: thx Korinican for the script ;p