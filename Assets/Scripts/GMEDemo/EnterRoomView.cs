
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using GME;
using UnityEngine.SceneManagement;

public class EnterRoomView : BaseViewController
{
    static string DEMO_VERSION = "Unity Demo None.None.None.None.None";

    int mClickCount = 0;
    double mFirstClickTime = 0;
   // 如需产品使用请参照文档创建 GME应用,  https://console.cloud.tencent.com/gamegme
    private string strAppid = ""; //AppID To Replace Here https://cloud.tencent.com/document/product/607/10782;
    private string strAppKey = ""; // AppKey To Replace Here https://cloud.tencent.com/document/product/607/10782；
    private string strRoomID = "1234563";
    private Dropdown outRoomTypeDropdown;
    private Dictionary<string, ITMGRoomType> _RoomtypeDic;
    
    public override void Start()
    {
        base.Start();
#if UNITY_SWITCH
        ITMGContext.GetInstance().SetLogPath("gme_log_cache:/GME_LOG/unity");
#else
        ITMGContext.GetInstance().SetLogPath(Application.persistentDataPath);
#endif
        Screen.orientation = ScreenOrientation.Landscape;
        Debug.Log("LoginViewController start!!!");
#if !UNITY_SWITCH
        StartCoroutine(copyFileFromAssetsToPersistent("3d_model"));
        StartCoroutine(copyFileFromAssetsToPersistent("111.mp3"));
        StartCoroutine(copyFileFromAssetsToPersistent("111.ogg"));
        StartCoroutine(copyFileFromAssetsToPersistent("222.mp3"));
        StartCoroutine(copyFileFromAssetsToPersistent("222.ogg"));

        CopyVoiceDataFromAssetsToPersistent();
#endif
        Debug.Log("EnterRoomViewController start");
        _RoomtypeDic = new Dictionary<string, ITMGRoomType>();
        _RoomtypeDic.Add("Smooth sound quality", ITMGRoomType.ITMG_ROOM_TYPE_FLUENCY);
        // _RoomtypeDic.Add("Standard sound quality", ITMGRoomType.ITMG_ROOM_TYPE_STANDARD);
        // _RoomtypeDic.Add("High definition sound quality", ITMGRoomType.ITMG_ROOM_TYPE_HIGHQUALITY);

        transform.Find("outroomPanel/userIDInputField").GetComponent<InputField>().text = UserConfig.GetUserID().ToString();
        outRoomTypeDropdown = transform.Find("outroomPanel/outRoomTypeDropdown").gameObject.GetComponent<Dropdown>();
        outRoomTypeDropdown.value = (int)UserConfig.GetRoomType() - 1;

        Button joinRoomBtn = transform.Find("outroomPanel/joinRoomBtn").GetComponent<Button>();
        if (joinRoomBtn)
        {
            joinRoomBtn.onClick.AddListener(delegate () {
                this.OnClickJoinRoomBtn();
            });
        }

        showErrorUI(GameSceneScript.GetErrcode());
    }

    void OnDestroy()
    {
        Debug.Log("LoginViewController, OnDestroy");
    }
    
    void ShowWarnning(string warningContent)
    {
        Text warningLabel = transform.Find("warningLabel").GetComponent<Text>();
        if (warningLabel)
        {
            warningLabel.text = warningContent;
        }
    }

    public override void Update()
    {
        base.Update();
    }

    bool OnClickLogin()
    {
        //ITMGContext.GetInstance().SetTestEnv(UserConfig.GetTestEnv());  //// warning : never call this API for any reason, it's only for internal use
        ITMGContext.GetInstance().SetAppVersion(DEMO_VERSION);        //// Just for Test
        if (strAppid == "" || strAppKey == "")
        {
            ShowWarnning("Appid or AppKey is null");
            return false;
        }
        string sUserID = transform.Find("outroomPanel/userIDInputField").GetComponent<InputField>().text;
        if(sUserID == "")
        {
            ShowWarnning("UserID not empty!");
            return false;
        }
        int ret = ITMGContext.GetInstance().Init(strAppid, sUserID);
        if (ret != QAVError.OK)
        {
            ShowWarnning(string.Format("Init Failed {0}", ret));
            return false;
        }

        UserConfig.SetAppID(strAppid);
        UserConfig.SetUserID(sUserID);
        UserConfig.SetAuthKey(strAppKey);
        UserConfig.SetRoomID(strRoomID);
        byte[] authBuffer = UserConfig.GetAuthBuffer(UserConfig.GetAppID(), UserConfig.GetUserID(), null, UserConfig.GetAuthKey());
        ITMGContext.GetInstance().GetPttCtrl().ApplyPTTAuthbuffer(authBuffer);
        int retCode = (int)ITMGContext.GetInstance().CheckMicPermission();
        Debug.Log(string.Format("Check permission Code is {0}", retCode));

        if(retCode != 0)
        {
            ShowWarnning(string.Format("CheckMicPermission code is {0}", retCode));
            //return false;
        }

        return true;
    }
    
    void OnClickJoinRoomBtn()
    {
        if(!OnClickLogin())
        {
            return;
        }
        string roomtyepString = outRoomTypeDropdown.options[outRoomTypeDropdown.value].text;
        ITMGRoomType roomtype = _RoomtypeDic[roomtyepString];
        UserConfig.SetRoomID(strRoomID);
        UserConfig.SetRoomType(roomtype);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);

        Destroy(this.gameObject);
    }

    public override void closeScene()
    {
        Destroy(this.gameObject);
        Application.Quit();
    }

    public void showErrorUI(int code)
    {
        switch (code)
        {
            case 0:
                break;
            case -1:
                ShowWarnning("Connect server error, please check net");
                break;
            default:
                ShowWarnning(string.Format("Enter room error,code: {0}", code));
                break;
        }
    }
}
