
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GME;

class UserConfig
{
	
    public static string GetExperientialAppID()
    {
        return "AppID To Replace Here";
    }

    public static string GetExperientialauthkey()
    {
        return "AppKey To Replace Here";
    }

    public static bool GetTestEnv() {
		return PlayerPrefs.GetInt("TestEnv", 0) != 0;
	}
	
	public static void SetTestEnv(bool test) {
		PlayerPrefs.SetInt("TestEnv", test ? 1 : 0);
	}
//
// APP ID和APP KEY, 可能会不定期修改因此并不支持产品使用, 仅供测试评估.
// 如需产品使用请参照文档创建 GME应用,  https://console.cloud.tencent.com/gamegme
// 同时替换此处(两处一起替换)
// *********************************************************
	public static string GetAppID() {
        return PlayerPrefs.GetString("AppID", "AppID To Replace Here");
	}
// *********************************************************
//
	public static void SetAppID(string appID) {
		PlayerPrefs.SetString("AppID", appID);
	}

	public static string GetUserID() {
		int randomUId = UnityEngine.Random.Range(12345, 22345);
		return PlayerPrefs.GetString("UserID", randomUId.ToString() );
	}

	public static void SetUserID(string userID) {
		PlayerPrefs.SetString("UserID", userID);
	}

    public static string GetAuthKey()
    {
        return PlayerPrefs.GetString("AuthKey", "AppKey To Replace Here");
    }

    public static void SetAuthKey(string AuthKey)
    {
        PlayerPrefs.SetString("AuthKey", AuthKey);
    }

    public static string GetRoomID() {
		return PlayerPrefs.GetString("strRoomID", "banana");
	}

	public static void SetRoomID(string roomID) {
		PlayerPrefs.SetString("strRoomID", roomID);
	}



	public static ITMGRoomType GetRoomType() {
		return (ITMGRoomType)PlayerPrefs.GetInt("RoomType", 1);
	}

	public static void SetRoomType(ITMGRoomType roomtype) {
		PlayerPrefs.SetInt("RoomType", (int)roomtype);
	}

	public static byte[] GetAuthBuffer(string sdkAppID, string userID, string roomID,string authKey)
	{
        string key = "";

        //switch (sdkAppID)
        //{
        //case "AppID To Replace Here":
        //	key = "AppKey To Replace Here";
        //	break;
        // case "1400111984":
        //      key = "94PNjuWJt5vRVZ9S";
        //      break;
        //	default:
        //          key = authKey;
        //	break;
        //}
        key = authKey;
        return QAVAuthBuffer.GenAuthBuffer(int.Parse(sdkAppID), roomID, userID, key);
	}

    public static void SetIsFirstOpenApplication(int flag)
    {
        PlayerPrefs.SetInt("FirstOpenApplication", flag);
    }

    public static int IsFistOpenApplication()
    {
        return PlayerPrefs.GetInt("FirstOpenApplication", 0);
    }
}
