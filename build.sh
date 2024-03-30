#!/bin/bash
msbuild='/c/Program Files (x86)/Microsoft Visual Studio/2022/BuildTools/MsBuild/Current/Bin/MSBuild.exe'
"$msbuild" -p:Configuration=Release \
	&& cp -v bin/Release/StartTheGamePaused.dll ./mod/dll/ \
	&& cp -v lib/0Harmony.dll ./mod/dll/ \
	&& rm -rf ../saves/mods/StartTheGamePaused \
	&& cp -vr ./mod ../saves/mods/StartTheGamePaused

