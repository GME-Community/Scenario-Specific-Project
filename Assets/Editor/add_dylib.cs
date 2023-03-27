using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_dylib : MonoBehaviour {

	[UnityEditor.Callbacks.PostProcessBuild(1002)]
	public  static void OnPostprocessBuild (UnityEditor.BuildTarget BuildTarget, string path){  
		if (BuildTarget == UnityEditor.BuildTarget.iOS) {
			UnityEngine.Debug.Log ("OnPostprocessBuild add_dylib:"+path);  
			{

				string projPath = UnityEditorAV.iOS.Xcode.PBXProject.GetPBXProjectPath (path);  
				UnityEditorAV.iOS.Xcode.PBXProject proj = new UnityEditorAV.iOS.Xcode.PBXProject ();  

				proj.ReadFromString (System.IO.File.ReadAllText (projPath));  
				string target = proj.TargetGuidByName (UnityEditorAV.iOS.Xcode.PBXProject.GetUnityTargetName ()); 

				string dylibGuid = proj.FindFileGuidByProjectPath("Frameworks/Plugins/iOS/avsdk_dy.framework");//proj.AddFile("Frameworks/Plugins/iOS/avsdk_dy.framework", "Frameworks/avsdk_dy.framework");

				if (dylibGuid != null && dylibGuid.Length > 0) {
					proj.AddDynamicFramework (target, dylibGuid);

					System.IO.File.WriteAllText (projPath, proj.WriteToString ());
					//proj.AddBuildProperty(targetGuid, "LD_EXCLUDED_ARCHITECTURES", "armv7");
				} else {
					UnityEngine.Debug.LogWarning ("add_dylib Frameworks/Plugins/iOS/avsdk_dy.framework not found:"+path); 
				}
			}
		}
	}
}
