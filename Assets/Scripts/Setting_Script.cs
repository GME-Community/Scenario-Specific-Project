using System.Collections;
using System.Collections.Generic;
using System;
using GME;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public delegate void CallbackDelegate(bool enable);

public class VoiceList
{
    public int result;
    public string[] voice_list;
    public VoiceList()
    {
        bAdd = false;
        result = -1;
        voice_list = new string[] { };
    }

    public bool bAdd;
}

public class Setting_Script : MonoBehaviour
{
    private Toggle[] sel_Language;
    private Toggle[] threeD_voice;
    private Toggle[] change_voice;
    private Toggle[] howlingsuppression;
   // private Toggle[] change_which_voice;
    private Toggle[] range_voice;
    private Toggle[] backGroundMusic;
    private bool bEnableRange = true;
    public static CallbackDelegate m_callback;
    public static VoiceList sVoiceList;
    public static AudioSource EnterRoom_audio = null;

    public static void SetCallback(CallbackDelegate callback)
    {
        m_callback = callback;
    }

    void Awake()
    {
        if (sVoiceList == null)
        {
            sVoiceList = new VoiceList();
        }
        
        EnterRoom_audio = BackGroundMusicHelper.getInstanceGroundMusicHelper().getAudioSoucre();
        //init language toogle[]
        sel_Language = new Toggle[2];
        sel_Language[0] = GameObject.Find("select_language_English").GetComponent<Toggle>();
        sel_Language[1] = GameObject.Find("select_language_Chinese").GetComponent<Toggle>();

        setToggleColor(sel_Language[0], sel_Language[1]);
        //add listener
        sel_Language[0].onValueChanged.AddListener((isOn) => Lan_ToggleOnValueChanged("en"));
        sel_Language[1].onValueChanged.AddListener((isOn) => Lan_ToggleOnValueChanged("cn"));


        //init threeD_voice toogle[]
        threeD_voice = new Toggle[2];
        threeD_voice[0] = GameObject.Find("btn_3D_sound_on").GetComponent<Toggle>();
        threeD_voice[1] = GameObject.Find("btn_3D_sound_off").GetComponent<Toggle>();

        bool enable3d = ITMGContext.GetInstance().GetAudioCtrl().IsEnableSpatializer();
        Debug.Log("enable3d: " + enable3d);
        threeD_voice[0].isOn = enable3d;
        threeD_voice[1].isOn = !enable3d;

        if(threeD_voice[0].isOn)
        {
            setToggleColor(threeD_voice[0], threeD_voice[1]);
        }
        else
        {
            setToggleColor(threeD_voice[1], threeD_voice[0]);
        }

        //add listener
        threeD_voice[0].onValueChanged.AddListener((isOn) => threeD_ToggleOnValueChanged(true));
        threeD_voice[1].onValueChanged.AddListener((isOn) => threeD_ToggleOnValueChanged(false));

        backGroundMusic = new Toggle[2];
        backGroundMusic[0] = GameObject.Find("btn_music_on").GetComponent<Toggle>();
        backGroundMusic[1] = GameObject.Find("btn_music_off").GetComponent<Toggle>();

        setToggleColor(backGroundMusic[1], backGroundMusic[0]);

        //add listener
        backGroundMusic[0].onValueChanged.AddListener((isOn) => BackGroundMusic_ToggleOnValueChanged(true));
        backGroundMusic[1].onValueChanged.AddListener((isOn) => BackGroundMusic_ToggleOnValueChanged(false));
        
        //howling suppression 
        howlingsuppression = new Toggle[2];
        howlingsuppression[0] = GameObject.Find("btn_howling_on").GetComponent<Toggle>();
        howlingsuppression[1] = GameObject.Find("btn_howling_off").GetComponent<Toggle>();
        //add listener
        howlingsuppression[0].onValueChanged.AddListener((isOn) => howlingSuppression_ToggleOnValueChanged(true));
        howlingsuppression[1].onValueChanged.AddListener((isOn) => howlingSuppression_ToggleOnValueChanged(false));
        setToggleColor(howlingsuppression[0], howlingsuppression[1]);
        howlingsuppression[0].isOn = true;
        howlingsuppression[1].isOn = false;
        if(howlingsuppression[0].isOn)
        {
            setToggleColor(howlingsuppression[0], howlingsuppression[1]);
        }
        else
        {
            setToggleColor(howlingsuppression[1], howlingsuppression[0]);
        }
        
        //init Range_voice toogle[]
        range_voice = new Toggle[2];
        range_voice[0] = GameObject.Find("btn_Range_sound_on").GetComponent<Toggle>();
        range_voice[1] = GameObject.Find("btn_Range_sound_off").GetComponent<Toggle>();
        //add listener
        range_voice[0].onValueChanged.AddListener((isOn) => RangeVoice_ToggleOnValueChanged(true));
        range_voice[1].onValueChanged.AddListener((isOn) => RangeVoice_ToggleOnValueChanged(false));
        setToggleColor(range_voice[0], range_voice[1]);

        // range_voice[0]
        switch_VT_state(false);
          //init language toogle[]
        change_voice = new Toggle[2];
        change_voice[0] = GameObject.Find("change_voice_on").GetComponent<Toggle>();
        change_voice[1] = GameObject.Find("change_voice_off").GetComponent<Toggle>();
        //add listener
        change_voice[0].onValueChanged.AddListener((isOn) => changeVoice_ToggleOnValueChanged(true));
        change_voice[1].onValueChanged.AddListener((isOn) => changeVoice_ToggleOnValueChanged(false));
        setToggleColor(change_voice[1], change_voice[0]);

        transform.Find("tmpDropdown").GetComponent<Dropdown>().itemText.fontSize = 29;

        if (ThirdPersonCharacter.IsGmePersonUin(UserConfig.GetUserID()))
        {
            threeD_voice[0].interactable = false;
            threeD_voice[1].interactable = false;
            range_voice[0].interactable = false;
            range_voice[1].interactable = false;
        }

        //transform.Find("tmpDropdown").GetComponent<Dropdown>().transform.
        //voiceChanger
        transform.Find("tmpDropdown").GetComponent<Dropdown>().onValueChanged.AddListener(delegate (int value)
        {
            string voiceName = "";
            if (value < sVoiceList.voice_list.Length + 1 /*for None*/)
            {
                int ret = 0;
               if (value == 0)
                {
                    ret = ITMGContext.GetInstance().GetAudioEffectCtrl().SetVoiceChangerName(voiceName);
                }
                else
                {
                    voiceName = sVoiceList.voice_list[value - 1];
                    ret = ITMGContext.GetInstance().GetAudioEffectCtrl().SetVoiceChangerName(voiceName);
                }
                
                print("voice type: " + value + " ret: " + ret);
            }
        });


    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SetVoiceList(VoiceList voiceList)
    {
        if(sVoiceList == null)
        {
            sVoiceList = new VoiceList();
        }
        sVoiceList = voiceList;

    }

    private void setToggleColor(Toggle selectToggle, Toggle unSelectToggle)
    {
        if(selectToggle == null || unSelectToggle == null)
        {
            return;
        }

        ColorBlock drumColors = unSelectToggle.colors;
        drumColors.normalColor = new Color(1, 1, 1, 1f);
        drumColors.highlightedColor = new Color(1, 1, 1, 1f);
        unSelectToggle.colors = drumColors;


        ColorBlock drumColors1 = selectToggle.colors;
        drumColors1.normalColor = new Color(18.0f / 255.0f, 175.0f / 255.0f, 215.0f / 255.0f, 1);
        drumColors1.highlightedColor = new Color(18.0f / 255.0f, 175.0f / 255.0f, 215.0f / 255.0f, 1f);
        selectToggle.colors = drumColors1;
    }

    private void Lan_ToggleOnValueChanged(string language) {
        print("language: " + language);
          //change language to english
          if( language == "en" ){
            setToggleColor(sel_Language[0], sel_Language[1]);
            SetLanguage(Language.English);
        }
          else if(language == "cn"){ //change language to chinese

            setToggleColor(sel_Language[1], sel_Language[0]);
            SetLanguage(Language.Chinese);
        }
         
    }

    private void BackGroundMusic_ToggleOnValueChanged(bool enable) {
        if (EnterRoom_audio)
        {
            if (enable)
            {
                setToggleColor(backGroundMusic[0], backGroundMusic[1]);
                if (!EnterRoom_audio.isPlaying)
                {
                    EnterRoom_audio.Play();
                }
            }
            else
            {
                setToggleColor(backGroundMusic[1], backGroundMusic[0]);
                if (EnterRoom_audio.isPlaying)
                {
                    EnterRoom_audio.Pause();
                }
            }
        }
    }

    private void threeD_ToggleOnValueChanged(bool enable)
    {
        //get current 3D sound state
        Debug.Log("threeD_ToggleOnValueChanged enable: " + enable);

        bool state = ITMGContext.GetInstance().GetAudioCtrl().IsEnableSpatializer();
        if (enable) {
            setToggleColor(threeD_voice[0], threeD_voice[1]);
            if (!state){
                string filePath = Application.persistentDataPath + "/3d_model";
                Debug.Log(filePath);
                int ret = QAVError.OK;
                ret = ITMGContext.GetInstance().GetAudioCtrl().InitSpatializer(filePath);
                Debug.Log("open3d: " + ret);
                int ret2 = ITMGContext.GetInstance().GetAudioCtrl().EnableSpatializer(true, false);
                if (ret2 != QAVError.OK)
                {
                    return;
                }

                if (!bEnableRange)
                {
                    ret = ITMGContext.GetInstance().GetRoom().UpdateAudioRecvRange(5000);
                }
                else
                {
                    ret = ITMGContext.GetInstance().GetRoom().UpdateAudioRecvRange(500);
                }

            }
        }
        else {
            setToggleColor(threeD_voice[1], threeD_voice[0]);
            if (state) {
                int ret = ITMGContext.GetInstance().GetAudioCtrl().EnableSpatializer(false, false);
                Debug.Log("close 3d: " + ret);
            }
        }
         
    }

    private void RangeVoice_ToggleOnValueChanged(bool enable)
    {
        Debug.Log("RangeVoice_ToggleOnValueChanged enable: " + enable);

        if (enable)
        {
            setToggleColor(range_voice[0], range_voice[1]);
            if (bEnableRange)
            {
                return;
            }

            System.Random rd = new System.Random();
            int teamId = rd.Next();
            int ret = ITMGContext.GetInstance().SetRangeAudioTeamID(teamId);
            if (ret != QAVError.OK)
            {
                Debug.Log("SetRangeAudioTeamID error: " + ret);
                return;
            }

            //如果打开了范围语音，，则修改衰减范围为5
            ret = ITMGContext.GetInstance().GetRoom().UpdateAudioRecvRange(500);
            Debug.Log("UpdateAudioRecvRange 5 code: " + ret);

            bEnableRange = true;
        }
        else
        {
            setToggleColor(range_voice[1], range_voice[0]);

            int ret = ITMGContext.GetInstance().SetRangeAudioTeamID(0);
            ITMGContext.GetInstance().SetRangeAudioMode(ITMGRangeAudioMode.ITMG_RANGE_AUDIO_MODE_WORLD);
            Debug.Log("CANCLE SetRangeAudioTeamID error: " + ret);
            if (ret != QAVError.OK)
            {
                return;
            }

            //如果关闭范围语音，3d语音还处于打开状态，则修改3d语音衰减范围为5000
            ret = ITMGContext.GetInstance().GetRoom().UpdateAudioRecvRange(5000);
            Debug.Log("UpdateAudioRecvRange 50 code: " + ret);

            bEnableRange = false;
        }

        if (m_callback != null)
        {
            m_callback(bEnableRange);
        }

    }

    private void howlingSuppression_ToggleOnValueChanged(bool enable)
    {
        Debug.Log("howlingSuppression_ToggleOnValueChanged enable: " + enable);
    
        if (enable)
        {
            setToggleColor(howlingsuppression[0], howlingsuppression[1]);
            ITMGContext.GetInstance().SetAdvanceParams("HowlingFlag","1");
        }
        else
        {
            setToggleColor(howlingsuppression[1], howlingsuppression[0]);
            ITMGContext.GetInstance().SetAdvanceParams("HowlingFlag","0");
    
        }
    }
    
    
    private void changeVoice_ToggleOnValueChanged(bool enable)
    {
        print("changeVoice_ToggleOnValueChanged " + enable);
        if (!enable)
        {
            setToggleColor(change_voice[1], change_voice[0]);

            print(" voice change false");
            switch_VT_state(false);
            sVoiceList.bAdd = false;
            int ret = ITMGContext.GetInstance().GetAudioEffectCtrl().SetVoiceChangerName("");
            print("changeVoice_ToggleOnValueChanged ret: " + ret);

            // ITMGContext.GetInstance().GetAudioEffectCtrl().SetVoiceType(0);
        }
        else
        {
            setToggleColor(change_voice[0], change_voice[1]);
            if (sVoiceList != null && !sVoiceList.bAdd)
            {
                List<Dropdown.OptionData> listOptions = new List<Dropdown.OptionData>();
                //listOptions.Add(new Dropdown.OptionData("None"));
                //listOptions.Add(new Dropdown.OptionData("None1"));
                listOptions.Add(new Dropdown.OptionData("None"));
                foreach (string opt in sVoiceList.voice_list)
                {
                    listOptions.Add(new Dropdown.OptionData(opt));
                }

                Dropdown menu = transform.Find("tmpDropdown").GetComponent<Dropdown>();
                if (menu == null)
                {
                    print(" transform.Find tmpDropdown is null");
                    return;
                }
                menu.ClearOptions();
                menu.AddOptions(listOptions);
                sVoiceList.bAdd = true;
            }

            switch_VT_state(true);
        }

    }

    private void switch_VT_state(bool swittch)
    {
        Dropdown voiceChange_switch = transform.Find("tmpDropdown").GetComponent<Dropdown>();
        if (voiceChange_switch == null)
        {
            return;
        }
        voiceChange_switch.value = 0;
        if (voiceChange_switch)
        {
            voiceChange_switch.interactable = swittch;
        }
        
    }

    private void SetLanguage(Language language)
    {
        Debug.Log("SetLanguage :" + language);
        LanguageDataManager.Instancce.SetCurrentLanguageValue(language);
    }
}