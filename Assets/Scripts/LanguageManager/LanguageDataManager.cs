using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//保存可设置语言
public enum Language
{
    Chinese,        //中文
    English,        //英文
}


//文本内容，对应Resources/Language 下的文件内容

//Seeting界面所设计的文本
public enum LanguageTextName
{
    _setitle,
    _back,
    Language_Text,
    threeD_sound,
    CN_Toggle,
    EN_Toggle,
    change_voice_Text,
    voice_type_Text,
    _VT_lolita,
    _VT_uncle,
    _VT_INTANGIBLE,
    _VT_child,
    btn_on,
    btn_off,
    Range_sound,
    guide_text,
    guide_text1,
    guide_text2,
    guide_text3,
    guide_text4,
    guide_text5,
    guide_text6,
    guide_text7,
    guide_text8,
    guide_text2_1,
    guide_text3_1,
    guide_text4_1,
    exitRoom_tip,
    enterRoom_tip,
    bkgroundmusic,
    guide_text3_2,
    guide_text4_2,
    guide_text3_3,
    set_speakerMic,
    set_functionStatus,
    set_viewGuide,
    set_roomLogInfo,
    set_joystick,
    set_exit,
    set_micOn,
    set_micOff,
    set_speakerOn,
    set_speakerOff,
    gme_staff,
    set_guidButton,
    totast_micOn,
    totast_micOff,
    totast_speakOn,
    totast_speakOff,
    howling_suppression,
    Text_voiceChat,
    Text_ProximityVoice,
    Text_3DSpatialVoice,
    Text_VoiceChanging,
}

public class LanguageDataManager : MonoBehaviour
{
    [SerializeField]
    internal Language currentLanguage = Language.English;      //当前设置的语言
    // 中英文字典
    private Dictionary<string, string> ChineseDictionary = new Dictionary<string, string>();
    private Dictionary<string, string> EnglishDictionary = new Dictionary<string, string>();
    private static LanguageDataManager instancce = null;

    public Dictionary<string, string> ChineseEnterRoomDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> EnglishEnterRoomDictionary = new Dictionary<string, string>();


    private void InitEnterRoomDictionary()
    {
        ChineseEnterRoomDictionary.Add("已退出语音房间", "have exited");
        ChineseEnterRoomDictionary.Add("已进入语音房间", "have entered");
        ChineseEnterRoomDictionary.Add("您已退出语音房间", "You have exited");
        ChineseEnterRoomDictionary.Add("您已进入语音房间", "You have entered");

        EnglishEnterRoomDictionary.Add("have exited", "已退出语音房间");
        EnglishEnterRoomDictionary.Add("have entered", "已进入语音房间");
        EnglishEnterRoomDictionary.Add("You have exited", "您已退出语音房间");
        EnglishEnterRoomDictionary.Add("You have entered", "您已进入语音房间");
    }

    public static LanguageDataManager Instancce
    {
        get
        {
            return instancce;
        }
    }

    private void Awake()
    {
        instancce = this;
        // 加载语言资源
        LoadLanguageTxt(Language.Chinese);
        LoadLanguageTxt(Language.English);

        InitEnterRoomDictionary();
    }

    /// <summary>
    /// 设置语言
    /// </summary>
    public void SetCurrentLanguageValue(Language language)
    {
        currentLanguage = language;
        //获取场景中所有类型脚本
        LanguageUIText[] languageUITexts = Resources.FindObjectsOfTypeAll<LanguageUIText>();
        for (int i = 0; i < languageUITexts.Length; i++)
        {
            Debug.Log("SetCurrentLanguageValue:" + languageUITexts.Length);
            languageUITexts[i].SetLanguageTextName();
        }
       // Debug.Log("languageUITexts:" + languageUITexts.Length);
    }
    /// <summary>
    /// 加载对应的语言资源txt，保存到字典中
    /// </summary>
    /// <param name="language"></param>
    internal void LoadLanguageTxt(Language language)
    {
        TextAsset ta = Resources.Load<TextAsset>("Language/" + language.ToString());
        if (ta == null)
        {
            Debug.LogWarning("没有该语言的文本文件" + "Language/" + language.ToString());
            return;
        }
        string[] lines = ta.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
            {
                continue;
            }
            string[] kv = lines[i].Split(':');
            if (language == Language.Chinese)
            {
                ChineseDictionary.Add(kv[0], kv[1]);
            }
            else if (language == Language.English)
            {
                EnglishDictionary.Add(kv[0], kv[1]);
            }

            //Debug.Log(string.Format("key:{0},value:{1}", kv[0], kv[1]));
        }
    }

    /// <summary>
    /// 获得对应语言字典中的对应的 key 值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    internal string GetLanguageText(string key)
    {
        if (currentLanguage == Language.Chinese)
        {
            if (ChineseDictionary.ContainsKey(key))
            {
                return ChineseDictionary[key];
            }
            else
            {
                Debug.Log("!ChineseDictionary.ContainsKey(key)");
            }
        }
        else if (currentLanguage == Language.English)
        {
            if (EnglishDictionary.ContainsKey(key))
            {
                return EnglishDictionary[key];
            }
            else
            {
                Debug.Log("!EnglishDictionary.ContainsKey(key)");
            }
        }
        return string.Empty;
    }


    internal string GetEnterRoomLanguageText(string key)
    {
        if (currentLanguage == Language.English)
        {
            if (ChineseEnterRoomDictionary.Equals(key))
            {
                return ChineseEnterRoomDictionary[key];
            } 
            else if (ChineseEnterRoomDictionary.ContainsKey(key))
            {
                return ChineseEnterRoomDictionary[key];
            }
            else
            {
                Debug.Log("!ChineseDictionary.ContainsKey(key)" + key);
            }
        }
        else if (currentLanguage == Language.Chinese)
        {
            if (EnglishEnterRoomDictionary.Equals(key))
            {
                return EnglishEnterRoomDictionary[key];
            }
            else if (EnglishEnterRoomDictionary.ContainsKey(key))
            {
                return EnglishEnterRoomDictionary[key];
            }
            else
            {
                Debug.Log("!EnglishDictionary.ContainsKey(key)" + key);
            }
        }
        return string.Empty;
    }

    public string GetTotastTip(string key)
    {
        return GetLanguageText(key);
    }
}
