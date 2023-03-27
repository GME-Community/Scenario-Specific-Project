using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;


public class IosProjectScript : UnityEngine.MonoBehaviour {  
	[UnityEditor.Callbacks.PostProcessBuild(999)]  
	public  static void OnPostprocessBuild (UnityEditor.BuildTarget BuildTarget, string path){  
		if (BuildTarget == UnityEditor.BuildTarget.iOS) {  
			UnityEngine.Debug.Log (path);  
			
			{  
				string projPath = UnityEditorAV.iOS.Xcode.PBXProject.GetPBXProjectPath (path);  
				UnityEditorAV.iOS.Xcode.PBXProject proj = new UnityEditorAV.iOS.Xcode.PBXProject ();  
				
				proj.ReadFromString (System.IO.File.ReadAllText (projPath));  
				string target = proj.TargetGuidByName (UnityEditorAV.iOS.Xcode.PBXProject.GetUnityTargetName ());  
				
				proj.AddFrameworkToProject (target, "CoreTelephony.framework", false);
				proj.AddFrameworkToProject (target, "OpenAL.framework", false);
				proj.AddFrameworkToProject (target, "libc++.tbd", false);
				proj.AddFrameworkToProject (target, "libz.tbd", false);
				proj.AddFrameworkToProject (target, "libresolv.9.tbd", false);
				proj.AddFrameworkToProject (target, "libsqlite3.0.tbd", false);
				proj.AddFrameworkToProject (target, "AssetsLibrary.framework", false);

				//proj.AddExternalLibraryDependency(target,"libsqlite3.0.tbd",proj.AddFile())
				//proj.AddExternalProjectDependency("/Users/tobinchen/Documents/Project/OpenSDK_feiche/platform_client/avsdk_pack/avsdk_pack.xcodeproj","sss",UnityEditor.iOS.Xcode.PBXSourceTree.Absolute);

				proj.SetBuildProperty (target, "ENABLE_BITCODE", "NO");
				
				proj.SetBuildProperty (target, "CODE_SIGN_IDENTITY", "");
				proj.SetBuildProperty (target, "CODE_SIGN_IDENTITY[sdk=iphoneos*]", "");
				proj.SetBuildProperty (target, "PROVISIONING_PROFILE", "");
				proj.SetBuildProperty (target, "PROVISIONING_PROFILE_SPECIFIER", "");
				
				proj.SetBuildProperty (target, "CODE_SIGN_STYLE","Manual");
				proj.SetBuildProperty (target,"PRODUCT_BUNDLE_IDENTIFIER", "com.Company.GMEDemo");
				proj.SetBuildProperty (target,"DEVELOPMENT_TEAM", "");
				proj.SetBuildProperty (target,"PRODUCT_NAME", "GMEDemo");
				proj.SetBuildProperty(target, "EXCLUDED_ARCHS", "armv7");
				proj.AddBuildProperty(target,"OTHER_LDFLAGS","-ObjC");
				proj.AddBuildProperty(target,"GCC_PREPROCESSOR_DEFINITIONS","__declspec(noreturn)=__attribute__((noreturn))");
			
	

				System.IO.File.WriteAllText (projPath, proj.WriteToString ());
			}  
			
			{  
				string plistPath = path + "/Info.plist";
				UnityEditorAV.iOS.Xcode.PlistDocument plist = new UnityEditorAV.iOS.Xcode.PlistDocument ();  
				plist.ReadFromString (System.IO.File.ReadAllText (plistPath));  
				UnityEditorAV.iOS.Xcode.PlistElementDict rootDict = plist.root;
				
				rootDict.SetString ("NSMicrophoneUsageDescription", "QAVSDKDemo");
				rootDict.SetBoolean ("UIFileSharingEnabled", true);
				
				// UnityEditorAV.iOS.Xcode.PlistElementDict NSAppTransportSecurity = rootDict.CreateDict("NSAppTransportSecurity"); 
				// NSAppTransportSecurity.SetBoolean("NSAllowsArbitraryLoads", true);
				
				UnityEditorAV.iOS.Xcode.PlistElementArray CFBundleDocumentTypes = rootDict.CreateArray("CFBundleDocumentTypes"); // just for test
				CFBundleDocumentTypes.AddDict().CreateArray("LSItemContentTypes").AddString("public.content");
				
				rootDict.SetString ("NSCameraUsageDescription", "QAVSDKDemo");
				System.IO.File.WriteAllText (plistPath, plist.WriteToString ());  
 			}
		}
	}  
}