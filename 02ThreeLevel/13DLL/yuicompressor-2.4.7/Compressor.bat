@echo off
if "%1" == "" goto exit
if "%2" == "" goto exit
pushd "%1"
echo 正在压缩Css文件
for /r %%i in (*.css) do call "D:\Program Files (x86)\Java\jdk1.6.0_45\bin\java.exe" -jar F:\NetHttp_02\0042eggsoft.cnvs2015_GaoErFu\02ThreeLevel\13DLL\yuicompressor-2.4.7\build\yuicompressor-2.4.7.jar -o %%i %%i
echo 正在压缩js文件
for /r %%i in (*.js) do call "D:\Program Files (x86)\Java\jdk1.6.0_45\bin\java.exe" -jar F:\NetHttp_02\0042eggsoft.cnvs2015_GaoErFu\02ThreeLevel\13DLL\yuicompressor-2.4.7\build\yuicompressor-2.4.7.jar -o %%i %%i
::call "D:\Program Files\7-Zip\7z.exe" a %2.7z -r PackageTmp
pause
:exit
exit