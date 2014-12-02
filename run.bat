:run.bat
@echo off
cd ./Project2/bin/Debug
@echo Demonstrating Project2

Project2.exe *.* ../../../Test_Folder "/Thaha" "/Tabc"  /R  /N3 
Project2.exe *.* ../../../Test_Folder "/THaha" "/Tabc"  /N3 
Project2.exe *.* ../../../Test_Folder "/Thaha" "/Tabc"  /O /N3 
Project2.exe *.txt ../../../Test_Folder "/Thaha" "/Tabc"  /O /N3 
 
Project2.exe *.* ../../../Test_Folder /MDescription,KeyWords /R /N4
Project2.exe *.* ../../../Test_Folder /MDescription,KeyWords /N4
Project2.exe *.* ../../../Test_Folder /MKeyWords /N4
Project2.exe *.* ../../../Test_Folder /MKeyWords /R /N4

Project2.exe ../../../Test_Folder/test_Metadata.txt /Ttest /Kwords_3 /Db.cpp /N6
Project2.exe ../../../Test_Folder/2.doc /Ttest  /Db.cpp /N6


cd ../../..