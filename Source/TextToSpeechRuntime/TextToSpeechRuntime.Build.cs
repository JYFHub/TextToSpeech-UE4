// Copyright 1998-2015 Epic Games, Inc. All Rights Reserved.
using UnrealBuildTool;
using System;
using System.IO;

namespace UnrealBuildTool.Rules
{
	public class TextToSpeechRuntime : ModuleRules
	{
		public TextToSpeechRuntime(TargetInfo Target)
		{
			PublicIncludePaths.AddRange(
				new string[] {
					
				}
				);

			PrivateIncludePaths.AddRange(
				new string[] {
					"TextToSpeechRuntime/Private",
					"FMRTTSLib"
					
				}
				);

			PublicDependencyModuleNames.AddRange(
				new string[]
				{
					"Core",
					"CoreUObject",
					"Engine",
					"InputCore",
					"Media"
					
				}
				);

			PrivateDependencyModuleNames.AddRange(
				new string[]
				{
					
				}
				);

			DynamicallyLoadedModuleNames.AddRange(
				new string[]
				{
					
				}
				);
			//Necessary to build Media Sound Wave - should be fixed in 4.11
			PrivateIncludePathModuleNames.Add("Media");
			PrivateIncludePathModuleNames.Add("Engine");
			
			LoadFMRTTSLib(Target);
		}
		
		public bool LoadFMRTTSLib(TargetInfo Target) {
			bool isLibrarySupported = false;
			string filename = "";
			if ((Target.Platform == UnrealTargetPlatform.Win64) || (Target.Platform == UnrealTargetPlatform.Win32))
			{
				isLibrarySupported = true;
				if (Target.Platform == UnrealTargetPlatform.Win64)
				{
					filename = "FMRTTSLib.x64.lib";
				} else if (Target.Platform == UnrealTargetPlatform.Win32)
				{
					filename = "FMRTTSLib.x86.lib";
				}
				
				if (filename != "")
				{
					// path to directory where this .Build.cs is located
					string BaseDir = Path.GetDirectoryName(RulesCompiler.GetModuleFilename(this.GetType().Name));
					PublicLibraryPaths.Add(Path.Combine(BaseDir, "../FMRTTSLib"));
					PublicAdditionalLibraries.Add(filename);
					PrivateIncludePaths.Add("FMRTTSLib/FMRTTSLib.h");
					Definitions.Add(string.Format( "WITH_FMRTTSLIB={0}", isLibrarySupported ? 1 : 0 ) );
				}
			}
			return isLibrarySupported;
		}
	}
}