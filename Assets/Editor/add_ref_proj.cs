using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_ref_proj : MonoBehaviour {

	[UnityEditor.Callbacks.PostProcessBuild(1001)]
	public  static void OnPostprocessBuild (UnityEditor.BuildTarget BuildTarget, string path){  
		// if (BuildTarget == UnityEditor.BuildTarget.iOS) {
		// 	path = Path.GetFullPath(path);
		// 	UnityEngine.Debug.Log ("OnPostprocessBuild add_ref_proj:"+path);  
  //
		// 	bool addRefProj = false;
		// 	string addRefProjEvn = Environment.GetEnvironmentVariable ("ADD_REF_PROJ");
		// 	UnityEngine.Debug.LogWarning ("ADD_REF_PROJ:"+addRefProjEvn);
		// 	if (addRefProjEvn != null && addRefProjEvn.Equals ("NO"))
		// 	{
		// 		addRefProj = false;
		// 	}
  //
		// 	bool useAvsdkDylib = false;
		// 	string usingDylibEvn = Environment.GetEnvironmentVariable ("USING_AVSDK_DYLIB");
		// 	UnityEngine.Debug.LogWarning ("USING_AVSDK_DYLIB:"+usingDylibEvn);
		// 	if (usingDylibEvn != null && usingDylibEvn.Equals ("YES"))
		// 	{
		// 		useAvsdkDylib = true;
		// 	}
  //
		// 	string projPath = UnityEditorAV.iOS.Xcode.PBXProject.GetPBXProjectPath (path);  
		// 	UnityEditorAV.iOS.Xcode.PBXProject proj = new UnityEditorAV.iOS.Xcode.PBXProject ();  
  //
		// 	proj.ReadFromString (System.IO.File.ReadAllText (projPath));  
		// 	string targetGuid = proj.TargetGuidByName (UnityEditorAV.iOS.Xcode.PBXProject.GetUnityTargetName ()); 
  //
		// 	string buildLocation = UnityEngine.Application.dataPath;
		// 	UnityEngine.Debug.Log ("buildLocation:" + buildLocation);  
  //
		// 	string refProjPath = buildLocation.Replace("Demo/Unity/Unity_OpenSDK_Audio/Assets", null);
		// 	UnityEngine.Debug.LogWarning (projPath + "=>" + refProjPath);
  //
		// 	if (addRefProj){
		// 		if (useAvsdkDylib) {
		// 			AddProjRef (proj,targetGuid,refProjPath+"platform_build/ios/avsdk_dy/avsdk_dy.xcodeproj/project.pbxproj",
		// 			            refProjPath+"/platform_build/ios/avsdk_dy/avsdk_dy.xcodeproj",
		// 				"avsdk_dy.framework",
		// 				"avsdk_dy");
		// 		} else {
  //
		// 			AddProjRef (proj,targetGuid,refProjPath+"/platform_build/ios/QAVSDK.xcodeproj/project.pbxproj",
		// 			            refProjPath+"/platform_build/ios/QAVSDK.xcodeproj",
		// 						"libGMESDK.a",
		// 			            "GMESDK"
		// 						);
		// 		}
  //           }
  //
  //
  //           if (useAvsdkDylib) {
		// 		string dylibGuid = null;
		// 		if(addRefProj){
		// 			dylibGuid=proj.FindReferenceFileGuid("avsdk_dy.framework");
		// 		}else{
		// 			dylibGuid=proj.FindFileGuidByProjectPath("Frameworks/Plugins/iOS/avsdk_dy.framework");
		// 		}
  //
		// 		if (dylibGuid == null) {
		// 			UnityEngine.Debug.LogWarning ("avsdk_dy.framework guid not found");
		// 		} else {
		// 			UnityEngine.Debug.LogWarning ("avsdk_dy.framework guid:" + dylibGuid);
		// 			proj.AddDynamicFramework (targetGuid, dylibGuid);
  //
		// 			proj.SetBuildProperty (targetGuid, "IPHONEOS_DEPLOYMENT_TARGET","8.0");
		// 		}
		// 	}
  //
  //
		// 	//				AddProjRef (proj,targetGuid,"/Users/tobinchen/Documents/Project/OpenSDK_feiche/platform_build/ios/QAVSDK_CSharp.xcodeproj","/Users/tobinchen/Documents/Project/OpenSDK_feiche/platform_build/ios/QAVSDK_CSharp.xcodeproj","QAVSDK_CSharp.xcodeproj","QAVSDK_CSharp.framework","QAVSDK_CSharp");
		// 	System.IO.File.WriteAllText (projPath, proj.WriteToString ());
		// }
		
		
		if (BuildTarget == UnityEditor.BuildTarget.iOS) {
			UnityEngine.Debug.Log ("OnPostprocessBuild add_dylib:"+path);
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
			{
				string projPath = UnityEditor.iOS.Xcode.PBXProject.GetPBXProjectPath (path);  
				UnityEditor.iOS.Xcode.PBXProject proj = new UnityEditor.iOS.Xcode.PBXProject ();  

				proj.ReadFromString (System.IO.File.ReadAllText (projPath));  
				string targetGuid = proj.TargetGuidByName (UnityEditor.iOS.Xcode.PBXProject.GetUnityTargetName ()); // 2018
				// string targetGuid = proj.GetUnityMainTargetGuid();	// 2019
				string[] framework_names = {
					"libgmefdkaac.framework",
					"libgmelamemp3.framework",
					"libgmeogg.framework",
					"libgmesoundtouch.framework",
					"libxnn_core.framework",
					"libgmevoicechanger.framework"
				};

				for (int i = 0; i < framework_names.Length; i++)
				{
					string framework_name = framework_names[i];
					string dylibGuid = null;
					dylibGuid = proj.FindFileGuidByProjectPath("Frameworks/GME/Plugins/iOS/" + framework_name);

					if (dylibGuid == null) {
						UnityEngine.Debug.LogWarning (framework_name + " guid not found");
					} else {
						UnityEngine.Debug.LogWarning (framework_name + " guid:" + dylibGuid);
						UnityEditor.iOS.Xcode.Extensions.PBXProjectExtensions.AddFileToEmbedFrameworks(proj, targetGuid, dylibGuid);

						//	proj.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

						
					}
				}
                
				proj.AddBuildProperty(targetGuid, "LD_EXCLUDED_ARCHITECTURES", "armv7");
				System.IO.File.WriteAllText (projPath, proj.WriteToString ());
			}
#endif
		}
		
	}

	private static void AddProjRef(UnityEditorAV.iOS.Xcode.PBXProject proj,string targetGuid,string refProjPath,string refProjRelatePath,string productName,string remoteInfo)
	{
		UnityEditorAV.iOS.Xcode.PBXProject refProj = new UnityEditorAV.iOS.Xcode.PBXProject ();  
		refProj.ReadFromString (System.IO.File.ReadAllText (refProjPath));  

		string remoteLibGuid = refProj.FindFileGuidByRealPath(productName,UnityEditorAV.iOS.Xcode.PBXSourceTree.Absolute);
		string remoteTargetGuid = refProj.TargetGuidByName(remoteInfo);


		proj.AddExternalProjectDependency(refProjRelatePath, remoteInfo+".xcodeproj",
			UnityEditorAV.iOS.Xcode.PBXSourceTree.Source);
		proj.AddExternalLibraryDependency(targetGuid, productName, remoteLibGuid,
			refProjRelatePath, remoteInfo);
		proj.AddRemoteTargetDependeny(targetGuid, remoteTargetGuid,
			refProjRelatePath, remoteInfo);
	}
}
