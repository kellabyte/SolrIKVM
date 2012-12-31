@echo on
set ikvmc="..\..\lib\ikvm\bin\ikvmc.exe"
set ikvm="..\..\lib\bin\ikvm"
cd lib\solr
%ikvmc% -out:..\solr.dll -target:library *.jar
