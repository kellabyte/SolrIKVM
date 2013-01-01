#!/bin/bash

LIBDIR=./lib
IKVMBINDIR=${LIBDIR}/IKVM/bin
COMPILE_DEPS_ONLY=false
OS=`uname -s`
IKVM_PREFIX=
BUILD_TOOL=msbuild
SOLUTION_NAME="SolrIKVM.sln"

if [ $OS != "Windows_NT" ]; then
    IKVM_PREFIX="mono "
    BUILD_TOOL="xbuild"
    echo "Using Mono"
else
    echo "Using .NET"
fi

while getopts "d" opt; do
  case $opt in
    d)
      COMPILE_DEPS_ONLY=true
      ;;
    \?)
      echo "Invalid option: -$OPTARG" >&2
      ;;
  esac
done

if [ ! -f ${LIBDIR}/Solr/Solr.dll ]; then
    echo "Compiling Solr using IKVM..."    
    ${IKVM_PREFIX}${IKVMBINDIR}/ikvmc.exe -out:${LIBDIR}/Solr/Solr.dll -target:library ${LIBDIR}/Solr/*.jar
fi

if [ ${COMPILE_DEPS_ONLY} = false ]; then
    echo "Compiling SolrIKVM..."
    cd src
    ${BUILD_TOOL} ${SOLUTION_NAME}
    cd ..
fi
