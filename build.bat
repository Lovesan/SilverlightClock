@set SL_DIR=%PROGRAMFILES%\Reference Assemblies\Microsoft\Framework\Silverlight\v4.0
@set CSC=%WINDIR%\Microsoft.NET\Framework\v2.0.50727\csc.exe
@set ZIP_7Z=%PROGRAMFILES%\7-Zip\7z
@set SC_DIR=SilverlightClock
@set SS_DIR=SimpleServer
@set SITE_DIR=site

@rmdir /S /Q site
@cd %SC_DIR%
@"%CSC%" /nologo /out:SilverlightClock.dll /t:library /optimize+ /nostdlib+ /noconfig /r:"%SL_DIR%\mscorlib.dll" /r:"%SL_DIR%\system.dll" /r:"%SL_DIR%\System.Core.dll" /r:"%SL_DIR%\System.Net.dll" /r:"%SL_DIR%\System.Windows.dll" /r:"%SL_DIR%\System.Windows.Browser.dll" /r:"%SL_DIR%\System.Xml.dll" *.cs
@"%ZIP_7Z%" a SilverlightClock.zip *.xaml *.dll
@del /Q /F *.dll
@cd ..
@mkdir %SITE_DIR%
@move /Y %SC_DIR%\SilverlightClock.zip %SITE_DIR%\SilverlightClock.xap
@copy /B /Y %SC_DIR%\favicon.ico %SITE_DIR%\favicon.ico
@copy /B /Y %SC_DIR%\index.html %SITE_DIR%\index.html
@copy /B /Y %SC_DIR%\Silverlight.js %SITE_DIR%\Silverlight.js
@cd %SS_DIR%
@"%CSC%" /nologo /out:..\SimpleServer.exe /optimize+ /win32res:SimpleServer.res *.cs
@cd ..
@echo Compilation finished
