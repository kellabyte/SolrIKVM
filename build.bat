@echo on
set ikvmc="..\..\lib\ikvm\ikvmc.exe"
set ikvm="..\..\lib\ikvm"
cd lib\solr
%ikvmc% -out:..\solr.dll -target:library *.jar
