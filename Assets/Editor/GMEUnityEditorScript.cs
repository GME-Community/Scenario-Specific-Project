using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System;
using System.Reflection;
using UnityEditor.Build;

public class GMEUnityEditorScript : MonoBehaviour {


    [MenuItem("Tools/GME/0.Set Appid", false, 1)]
    public static void setInfo() {
        EditorWindow.GetWindow(typeof(EditorSetAppid));
    }

    [MenuItem("Tools/GME/1.Check files", false, 2)]
    public static void Checkfiles()
    {
        string[] fileName = {
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/arm64-v8a/libgmefaad2.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/arm64-v8a/libgmefdkaac.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/arm64-v8a/libgmelamemp3.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/arm64-v8a/libgmeogg.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/arm64-v8a/libgmesdk.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/arm64-v8a/libgmesoundtouch.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/arm64-v8a/libgmevoicechanger.so",


            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/armeabi-v7a/libgmefaad2.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/armeabi-v7a/libgmefdkaac.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/armeabi-v7a/libgmelamemp3.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/armeabi-v7a/libgmeogg.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/armeabi-v7a/libgmesdk.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/armeabi-v7a/libgmesoundtouch.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/armeabi-v7a/libgmevoicechanger.so",


            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86/libgmefaad2.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86/libgmefdkaac.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86/libgmelamemp3.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86/libgmeogg.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86/libgmesdk.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86/libgmesoundtouch.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86/libgmevoicechanger.so",


            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86_64/libgmefaad2.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86_64/libgmefdkaac.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86_64/libgmelamemp3.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86_64/libgmeogg.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86_64/libgmesdk.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86_64/libgmesoundtouch.so",
            "Assets/GME/Plugins/Android/Opensdk.plugin/libs/x86_64/libgmevoicechanger.so",


            "Assets/GME/Plugins/x86_64/gmesdk.dll",
            "Assets/GME/Plugins/x86_64/gmesdk.lib",
            "Assets/GME/Plugins/x86_64/libgmecrypto-1_1-x64.dll",
            "Assets/GME/Plugins/x86_64/libgmefaad2.dll",
            "Assets/GME/Plugins/x86_64/libgmefdkaac.dll",
            "Assets/GME/Plugins/x86_64/libgmelamemp3.dll",
            "Assets/GME/Plugins/x86_64/libgmeogg.dll",
            "Assets/GME/Plugins/x86_64/libgmesoundtouch.dll",
            "Assets/GME/Plugins/x86_64/libgmessl-1_1-x64.dll",
            "Assets/GME/Plugins/x86_64/libgmevoicechanger.dll",

            "Assets/GME/Plugins/iOS/libGMESDK.a"
        };

        for (int i = 0; i < fileName.Length; i++)
        {
            if (!File.Exists(fileName[i]))
            {
                Debug.LogWarning(fileName[i]+ "is not here!");
            }
        }


        
    }

    [MenuItem("Tools/GME/2.OpenDemo", false, 3)]
    public static void OpenDemo()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/HomeScene.unity");
        }
    }

    [MenuItem("Tools/GME/3.BuildIOS", false, 4)]
    public static void BuildIOS()
    {
        PlayerSettings.productName = "GMEDemo";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "com.Company.GMEDemo");
        BuildPipeline.BuildPlayer(GetBuildScenes(), projectName, BuildTarget.iOS, BuildOptions.None);
    }

    [MenuItem("Tools/GME/4.BuildAndroid", false, 5)]
    public static void BuildAndroid()
    {
        PlayerSettings.productName = "GMEDemo";
        SetAndroidAppId("com.gme.unityDemo");
        BuildPipeline.BuildPlayer(GetBuildScenes(), projectName + ".apk", BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("Tools/GME/Delete Windows x86 plugin")]
    public static void DeletPlugin()
    {
        string[] fileName = {
            "Assets/GME/Plugins/x86/gmesdk.dll",
            "Assets/GME/Plugins/x86/gmesdk.lib",
            "Assets/GME/Plugins/x86/libgmecodec.dll",
            "Assets/GME/Plugins/x86/libgmecodec_ogg.dll",
            "Assets/GME/Plugins/x86/msvcp100.dll",
            "Assets/GME/Plugins/x86/msvcr100.dll",
        };
        for (int i = 0; i < fileName.Length; i++)
        {
            if (File.Exists(fileName[i]))
            {
                File.Delete(fileName[i]);
            }
        }
    }

    [MenuItem("Tools/GME/Open GME official website")]
    public static void openWeb()
    {
        WWW www = new WWW("https://cloud.tencent.com/document/product/607/18521");
        Application.OpenURL(www.url);
    }

    [MenuItem("Tools/GME/Contact GME")]
    public static void Contact()
    {
        string email = "GME_Service@tencent.com";
        string subject = WWW.EscapeURL("Contact the GME official developer").Replace(" +", "%20");
        string body = WWW.EscapeURL("I am a game developer that working with the Unity 3D game engine").Replace("+", "%20"); 
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    public static string projectName
    {
        get
        {
            return "GMEDemo";
        }
    }

    public static String[] GetBuildScenes()
    {
        List<string> names = new List<String>();
        names.Add("Assets/Scenes/HomeScene.unity");
        names.Add("Assets/Scenes/GameScene.unity");
        return names.ToArray();
    }


    public static void SetAndroidAppId(string appId)
    {
        MethodInfo methodInfo = typeof(PlayerSettings).GetMethod("SetApplicationIdentifier");
        if (methodInfo != null)
        {
            object[] argArr = { BuildTargetGroup.Android, appId };
            //UnityEngine.Debug.Log("methodInfo.Invoke:" + argArr);
            methodInfo.Invoke(null, argArr);
        }
    }
}


public class EditorSetAppid : EditorWindow
{
    private string appid = "";
    private string key = "";
    //对话框中的各种内容通过OnGUI函数来设置
    void OnGUI()
    {
        
        //Label
        GUILayout.Label("AppId from the Tencent Cloud Console.", EditorStyles.boldLabel);
        appid = EditorGUILayout.TextField("Appid", appid);
        GUILayout.Label("Permission key from the Tencent Cloud Console.", EditorStyles.boldLabel);
        key = EditorGUILayout.TextField("Key", key);
        //Button
        if (GUILayout.Button("Apply"))
        {
            PlayerPrefs.SetString("AppID", appid);
            PlayerPrefs.SetString("AuthKey", key);
        }
    }

}
