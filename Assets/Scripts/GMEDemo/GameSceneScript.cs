using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GME;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
using System;

public class GameSceneScript : MonoBehaviour
{
    public GameObject Btn_Mic;
    public GameObject Btn_Speak;
    public Sprite Spr_MicClose;
    public Sprite Spr_MicOpen;
    public GameObject Panel_Help;
    public GameObject Panel_Setting;
    public NetworkManager NetManager;
    public GameObject Itme_Openid;
    public GameObject Content;
    public GameObject Panel_Help_chinese;

    private GameObject mToastObject = null;
    private int fDistance = 500;

    private bool isOpenMic = false;
    private bool isPlayMusic = false;
    private bool isOpenSpeak = false;

    public Sprite Spr_SpeakOpen;
    public Sprite Spr_SpeakClose;
    private VoiceList voiceChangeList = new VoiceList();
   // private Guide mGuide = null;
    public Button _btnEnsure = null;
    public Image _maskImage = null;
    private long startRequireTime = 0;
    private const int TIME_OUTMS = 20000;
    private static int sCode = 0;
    
    private void Start()
    {
        startRequireTime = 0;
        sCode = 0;
        //判断是否第一次在设备上打开该应用
        if (0 == UserConfig.IsFistOpenApplication())
        {
            SetImageWH();
            //显示用户指引操作流程
            SetGuideActive(true);
            _btnEnsure.GetComponent<Button>().onClick.AddListener(delegate
            {
                UserConfig.SetIsFirstOpenApplication(1);
                SetGuideActive(false);
                OpenOrCloseMic();
                OpenOrCloseSpeak();
            });
        }
        
        //Panel_Setting.SetActive(false);
        string sRoomID = UserConfig.GetRoomID();
        byte[] authBuffer = UserConfig.GetAuthBuffer(UserConfig.GetAppID(), UserConfig.GetUserID(), sRoomID, UserConfig.GetAuthKey());
        ITMGRoomType roomtype = UserConfig.GetRoomType();
        Debug.LogFormat("authBuffer size = {0}", authBuffer.Length);
        EnableRangeAudio();
        
        print("roomtype: " + roomtype);
		ITMGContext.GetInstance().SetAdvanceParams("AudienceAudioCategory", "1");
        
        ITMGContext.GetInstance().EnterRoom(sRoomID, roomtype, authBuffer);
        ITMGContext.GetInstance().OnEnterRoomCompleteEvent += new QAVEnterRoomComplete(OnEnterRoomComplete);
        ITMGContext.GetInstance().OnExitRoomCompleteEvent += new QAVExitRoomComplete(OnExitRoomComplete);
        ITMGContext.GetInstance().OnEndpointsUpdateInfoEvent += new QAVEndpointsUpdateInfo(OnEndpointsUpdateInfo);
        ITMGContext.GetInstance().GetAudioEffectCtrl().OnFetchVoiceChangerListCallback += new QAVFetchVoiceChangerListCallback(OnFetchVoiceChangerList);

        LanguageDataManager.Instancce.SetCurrentLanguageValue(Language.English);
    }

    private void Update()
    {
        ITMGContext.GetInstance().Poll();
        
        // unity netWORKmanager 监听链接服务状态回调失效，只能在业务层设置超时时间判断
        if (startRequireTime != 0 && !NetManager.IsClientConnected())
        {
            if (GetCurrentTimeMS() - startRequireTime > TIME_OUTMS)
            {
                Debug.Log("START Connect server error");
                sCode = -1;
                OnExitButtonDown();
            }
        }

        if (startRequireTime != 0 && NetManager.IsClientConnected())
        {
            startRequireTime = 0;
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log(string.Format("NetManager.IsClientConnected() {0}", NetManager.IsClientConnected()));
        if (hasFocus)
        {
            ITMGContext.GetInstance().Resume();
            if(!NetManager.IsClientConnected() ){
                Debug.Log(string.Format("IsClientConnected false"));
                StartConnectServer();
            }

        }
        else
        {
            ITMGContext.GetInstance().Pause();
#if UNITY_IOS
            NetManager.StopClient();
             NetManager.StopHost();
#endif
        }
    }
    
    void OnApplicationPause(bool pauseStatus)
    {
        Debug.Log(string.Format("OnApplicationPause {0}", pauseStatus));
    }

    private bool StartConnectServer()
    {
        NetManager.networkAddress = "localhost";
        NetManager.networkPort = 7777;
        
        //开启服务
        //NetManager.StartServer();
        
        //  客户端链接服务端
        //NetManager.StartClient();
        
        //  作为服务和客户端
        NetManager.StartHost();
        return true;
    }


    void OnEnterRoomComplete(int err, string errInfo)
    {
        Debug.Log("OnEnterRoomComplete err: " + err);
        sCode = err;
        if(err != 0)
        {
            SceneManager.UnloadSceneAsync("GameScene");
            GameObject obj = Resources.Load<GameObject>("Prefabs/EnterRoomSenceV2");
            Instantiate(obj);
            return;
        }
        
        //开启啸叫抑制
        ITMGContext.GetInstance().SetAdvanceParams("HowlingFlag","1");
        
        Enable3D();
        ITMGContext.GetInstance().GetRoom().UpdateAudioRecvRange(fDistance);
        ThirdPersonCharacter.SetPersonInfo(fDistance / 100);
        
        //判断当前userid是否为gme工作人员账号，如果是，则设为主持人模式
         if (ThirdPersonCharacter.IsGmePersonUin(UserConfig.GetUserID()))
         {
            Debug.Log("gme staff");
            SetRangeVoiceHost();
         }
         
        //get voicechanger info
        string path = Application.persistentDataPath + "/VoiceData";
        Debug.Log("VoiceChanger path" + path);
        int ret = ITMGContext.GetInstance().GetAudioEffectCtrl().InitVoiceChanger(path);
        Debug.Log("InitVoiceChanger  ret = " + ret);
        ret = ITMGContext.GetInstance().GetAudioEffectCtrl().FetchVoiceChangerList();
        Debug.Log("FetchVoiceChangerVoiceList ret = " + ret);
        
        //默认打开麦克风和扬声器
        if(0 != UserConfig.IsFistOpenApplication())
        {
            OpenOrCloseMic();
            OpenOrCloseSpeak();
        }

        StartConnectServer();
        startRequireTime = GetCurrentTimeMS();
    }

    public static int GetErrcode()
    {
        return sCode;
    }

    public void Enable3D()
    {
        string filePath = Application.persistentDataPath + "/3d_model";
        Debug.Log(filePath);
        int ret = QAVError.OK;
        ret = ITMGContext.GetInstance().GetAudioCtrl().InitSpatializer(filePath);
        Debug.Log(ret);
        int ret2 = ITMGContext.GetInstance().GetAudioCtrl().EnableSpatializer(true, false);
        if (ret2 != QAVError.OK)
        {
            Debug.Log("EnableSpatializer FALSE, ret: " + ret);
            return;
        }
    }

    private void SetRangeVoiceHost()
    {
        int ret = ITMGContext.GetInstance().SetRangeAudioTeamID(0);
        if(ret != QAVError.OK)
        {
            Debug.Log("SetRangeVoiceHost SetRangeAudioTeamID 0 error: " + ret);
            return;
        }
    }
    public void EnableRangeAudio()
    {
        System.Random rd = new System.Random();
        int teamId = rd.Next();
        print("teamI：" + teamId);
        int ret = ITMGContext.GetInstance().SetRangeAudioTeamID(teamId);
        if(ret != QAVError.OK)
        {
            Debug.Log("SetRangeAudioTeamID error: " + ret);
            return;
        }

        ret = ITMGContext.GetInstance().SetRangeAudioMode(ITMGRangeAudioMode.ITMG_RANGE_AUDIO_MODE_WORLD);

        Debug.Log("SetRangeAudioMod, ret: " + ret);
    }

    public void OnMicButtonDown() {
        Vector3 totastPosition = new Vector3(-280, -150, 0);   
        GameObject totastObj = Btn_Mic.transform.parent.gameObject;
        if (isOpenMic)
        {
            OpenOrCloseMic();
            string totastText = LanguageDataManager.Instancce.GetTotastTip("totast_micOff");
            ShowToastUI(totastText, totastPosition, totastObj.transform);
        }
        else {
            OpenOrCloseMic();
            string totastText = LanguageDataManager.Instancce.GetTotastTip("totast_micOn");
            ShowToastUI(totastText, totastPosition, totastObj.transform);
        }
    }

    public void OpenOrCloseMic()
    {
        if (isOpenMic)
        {
            ITMGContext.GetInstance().GetAudioCtrl().EnableMic(false);
            print("OnSpeakButtonDown EnableMic false");
            Btn_Mic.GetComponent<Image>().sprite = Spr_MicClose;
            isOpenMic = false;
        }
        else
        {
            ITMGContext.GetInstance().GetAudioCtrl().EnableMic(true);
            print("OnSpeakButtonDown EnableMic true");
            Btn_Mic.GetComponent<Image>().sprite = Spr_MicOpen;
            isOpenMic = true;
        }
    }

    public void OnSpeakButtonDown()
    {
        Vector3 totastPosition = new Vector3(-380, -149.8f, 0);
        GameObject totastObj = Btn_Speak.transform.parent.gameObject;
        //Btn_Mic.spriteState = spriteState.disabledSprite
        if (isOpenSpeak)
        {
            OpenOrCloseSpeak();
            string totastText = LanguageDataManager.Instancce.GetTotastTip("totast_speakOff");
            ShowToastUI(totastText, totastPosition, totastObj.transform);
        }
        else
        {
            OpenOrCloseSpeak();
            string totastText = LanguageDataManager.Instancce.GetTotastTip("totast_speakOn");
            ShowToastUI(totastText, totastPosition, totastObj.transform);
        }

    }

    public void OpenOrCloseSpeak()
    {
        if (isOpenSpeak)
        {
            ITMGContext.GetInstance().GetAudioCtrl().EnableSpeaker(false);
            print("OnSpeakButtonDown EnableSpeaker false");
            Btn_Speak.GetComponent<Image>().sprite = Spr_SpeakClose;
            isOpenSpeak = false;
        }
        else
        {
            ITMGContext.GetInstance().GetAudioCtrl().EnableSpeaker(true);
            print("OnSpeakButtonDown EnableSpeaker true");
            Btn_Speak.GetComponent<Image>().sprite = Spr_SpeakOpen;
            isOpenSpeak = true;
        }

    }

    public void OnExitButtonDown() {
        print("OnExitButtonDown");
        startRequireTime = 0;
        if (NetManager.IsClientConnected()) {
            NetManager.StopClient();
            NetManager.StopHost();
        }
        
        ITMGContext.GetInstance().ExitRoom();
        if (Setting_Script.EnterRoom_audio != null)
        {
            if (Setting_Script.EnterRoom_audio.isPlaying)
            {
                Setting_Script.EnterRoom_audio.Pause();
            }
        }

        Setting_Script.EnterRoom_audio = null;
    }


    private void OnExitRoomComplete() {
        print("OnExitRoomComplete");
        int ret = ITMGContext.GetInstance().SetRangeAudioTeamID(0);

        SceneManager.UnloadSceneAsync("GameScene");
        GameObject obj = Resources.Load<GameObject>("Prefabs/EnterRoomSenceV2");
        Instantiate(obj);
    }
    
    public void OnHelpButtonDown() {
        
        if (Panel_Help.activeSelf)
        {
            Panel_Help.SetActive(false);
        }
        else if (Panel_Help_chinese.activeSelf)
        {
            Panel_Help_chinese.SetActive(false);
        }
        else {
            if (LanguageDataManager.Instancce.currentLanguage == Language.Chinese)
            {
                Panel_Help_chinese.SetActive(true);
            }
            else
            {
                Panel_Help.SetActive(true);
            }
        }
    }
    
    public void OnSetButtonDown()
    {
        if (Panel_Help.activeSelf)
        {
            Panel_Help.SetActive(false);
        }
        else if (Panel_Help_chinese.activeSelf)
        {
            Panel_Help_chinese.SetActive(false);
        }
        
        if (Panel_Setting.activeSelf)
        {
            Panel_Setting.SetActive(false);
        }
        else
        {
            Panel_Setting.SetActive(true);
        }
    }

    public void OnPlayMusicButtonDown() {

        if (isPlayMusic)
        {
            ITMGContext.GetInstance().GetAudioEffectCtrl().StopAccompany(1);
            isPlayMusic = false;
        }
        else {
            int ret = 0;
            string filename = "111.ogg";
            filename = Application.persistentDataPath + "/" + filename;
            ret = ITMGContext.GetInstance().GetAudioEffectCtrl().StartAccompany(filename,
                                                      true, 0, 0);
            Debug.Log("acc  ret:" + ret);
            isPlayMusic = true;
        }
        
    }

    void OnFetchVoiceChangerList(int code, string listJson)
    {
        Debug.Log("OnFetchVoiceChangerVoiceList = " + listJson);
        voiceChangeList = JsonUtility.FromJson<VoiceList>(listJson);

        Setting_Script.SetVoiceList(voiceChangeList);
    }

    private void OnDestroy()
    {
        ITMGContext.GetInstance().OnEnterRoomCompleteEvent -= new QAVEnterRoomComplete(OnEnterRoomComplete);
        ITMGContext.GetInstance().OnExitRoomCompleteEvent -= new QAVExitRoomComplete(OnExitRoomComplete);
        ITMGContext.GetInstance().OnEndpointsUpdateInfoEvent -= new QAVEndpointsUpdateInfo(OnEndpointsUpdateInfo);
        ITMGContext.GetInstance().GetAudioEffectCtrl().OnFetchVoiceChangerListCallback -= new QAVFetchVoiceChangerListCallback(OnFetchVoiceChangerList);

    }
    
    void OnEndpointsUpdateInfo(int eventID, int count, string[] openIdList)
    {
        RefreshMemberList(openIdList, eventID);
    }

    public void Add3DBlackList(string userid)
    {
        ITMGContext.GetInstance().GetAudioCtrl().AddSpatializerBlacklist(userid);
    }

    void RefreshMemberList(string[] memberList, int eventID)
    {
        if (eventID == ITMGContext.EVENT_ID_ENDPOINT_ENTER)
        {
            // Add
            foreach (string m in memberList)
            {
                if (m != UserConfig.GetUserID())
                {
                    GameObject go = Instantiate(Itme_Openid, Content.transform, false);
                            LanguageUIText.SetEnterRoomText("UserID: " + m, 1);
                    go.GetComponent<Text>().text = "UserID:" + m + " have entered";
                    if (ThirdPersonCharacter.IsGmePersonUin(m))
                    {
                        Add3DBlackList(m);
                    }
                }
               
            }
        }
        else if (eventID == ITMGContext.EVENT_ID_ENDPOINT_EXIT)
        {
            // Remove
            foreach (string m in memberList)
            {
                GameObject go = Instantiate(Itme_Openid, Content.transform, false);
                go.GetComponent<Text>().text = "UserID:" + m + " have exited";
                LanguageUIText.SetEnterRoomText("UserID: " + m, 2);
            }
        }
    }
    
    public void SetGuideActive(bool active)
    {
        if (_maskImage != null)
        {
            _maskImage.gameObject.SetActive(active);
        }
    }

    private void SetImageWH()
    {
        float width = Screen.width;
        float height = Screen.height;

        RectTransform parentRectTransform = _maskImage.transform.parent.gameObject.GetComponent<RectTransform>();
        if(parentRectTransform == null)
        {
            print("parentRectTransform null: ");
            return;
        }

        RectTransform rectTransform = _maskImage.GetComponent<RectTransform>();

        rectTransform.transform.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1);
        rectTransform.transform.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1);
        rectTransform.anchoredPosition3D = new Vector3(-parentRectTransform.rect.width / 2.0f, -parentRectTransform.rect.height / 2.0f, 1); ;
        
        print("w: " + width + " h: " + height);
        rectTransform.sizeDelta = new Vector2(parentRectTransform.rect.width, parentRectTransform.rect.height);
        
    }

    private long GetCurrentTimeMS()
    {
        long currentTicks=DateTime.Now.Ticks;
        DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        long currentMillis = (currentTicks - dtFrom.Ticks) / 10000;
        return currentMillis;
    }
     
    // arg0: 提示信息 
    public void ShowToastUI(string str, Vector3 position, Transform trans)
    {
        if (mToastObject && mToastObject.active)
        {
            GameObject.Destroy(mToastObject);
        }

        GameObject toastObject = Resources.Load<GameObject>("Prefabs/ToastCanvas");
        mToastObject = Instantiate(toastObject);

        mToastObject.transform.parent = trans;
        mToastObject.transform.localScale = Vector3.one;

        RectTransform rectTransform = mToastObject.transform.GetComponent<RectTransform>();
        Image imageTip = mToastObject.transform.Find("TipImage").GetComponent<Image>();
        
         if (imageTip)
         {
             imageTip.color = new Color(0, 0, 0, 0.6f);
            RectTransform imageRectTransform = imageTip.transform.GetComponent<RectTransform>();
            Text tips = imageTip.transform.Find("TipText").GetComponent<Text>();
            if (tips == null)
            {
                Debug.Log("tip s is null");
                return;
            }
            if (LanguageDataManager.Instancce.currentLanguage == Language.Chinese)
            {
                imageRectTransform.sizeDelta = new Vector2(str.Length * tips.fontSize + 20, tips.fontSize + 20);
            }
            else {
                imageRectTransform.sizeDelta = new Vector2(str.Length * tips.fontSize / 2.22f + 30.0f, tips.fontSize + 20);
            }
            
            if(tips)
            {
                tips.text = str;
            }
            rectTransform.transform.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1);
            rectTransform.transform.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1);

            rectTransform.anchoredPosition3D = position;
         }
         GameObject.Destroy(mToastObject, 3);
    }

}
