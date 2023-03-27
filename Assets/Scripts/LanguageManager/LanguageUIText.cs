using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LanguageUIText : MonoBehaviour
{
    [SerializeField]
    private LanguageTextName languageTextName;
    // Use this for initialization
    void Start()
    {
        SetLanguageTextName();
    }

    private static string userIdContext = "";
    private static int EnterType = 1;
    Language currentLanguage = Language.Chinese;

    public static void SetEnterRoomText(string text, int enterType)
    {
        userIdContext = text;
        EnterType = enterType;
    }

    /// 根据当前语言设置 Text 文本
    internal void SetLanguageTextName()
    {
        string value = LanguageDataManager.Instancce.GetLanguageText(languageTextName.ToString());
        if (languageTextName.ToString() == "enterRoom_tip")
        {
            SetEnterRoomLanguageTextName(gameObject.GetComponent<Text>().text);
            return;

        }
        if (string.IsNullOrEmpty(value) != true)
        {
            gameObject.GetComponent<Text>().text = value;
        }
    }


    internal void SetEnterRoomLanguageTextName(string str)
    {
        if (str == "您已退出语音房间" || str == "您已进入语音房间" && LanguageDataManager.Instancce.currentLanguage != Language.Chinese)
        {
            gameObject.GetComponent<Text>().text = LanguageDataManager.Instancce.GetEnterRoomLanguageText(str);
        }
        else if (str == "You have exited"
        || str == "You have entered" && LanguageDataManager.Instancce.currentLanguage != Language.English)
        {
            gameObject.GetComponent<Text>().text = LanguageDataManager.Instancce.GetEnterRoomLanguageText(str);
        }
        else if (str != "您已退出语音房间" && str != "您已进入语音房间" && str != "You have exited" && str != "You have entered")
        {

            string[] strArray = str.Split(new string[] { "have entered" }, System.StringSplitOptions.RemoveEmptyEntries);
            if (strArray[0].Length == str.Length)
            {
                strArray[0] = "";
            }
            string[] strArray1 = str.Split(new string[] { "have exited" }, System.StringSplitOptions.RemoveEmptyEntries);
            if (strArray1[0].Length == str.Length)
            {
                strArray1[0] = "";
            }
            string[] strArray2 = str.Split(new string[] { "已退出语音房间" }, System.StringSplitOptions.RemoveEmptyEntries);
            if (strArray2[0].Length == str.Length)
            {
                strArray2[0] = "";
            }
            string[] strArray3 = str.Split(new string[] { "已进入语音房间" }, System.StringSplitOptions.RemoveEmptyEntries);
            if (strArray3[0].Length == str.Length)
            {
                strArray3[0] = "";
            }

            if (string.IsNullOrEmpty(strArray[0]) != true && LanguageDataManager.Instancce.currentLanguage != Language.English)
            {
                gameObject.GetComponent<Text>().text = strArray[0] + LanguageDataManager.Instancce.GetEnterRoomLanguageText("have entered");
            }
            if (string.IsNullOrEmpty(strArray1[0]) != true && LanguageDataManager.Instancce.currentLanguage != Language.English)
            {
                gameObject.GetComponent<Text>().text = strArray1[0] + LanguageDataManager.Instancce.GetEnterRoomLanguageText("have exited");
            }

            if (string.IsNullOrEmpty(strArray2[0]) != true && LanguageDataManager.Instancce.currentLanguage != Language.Chinese)
            {
                gameObject.GetComponent<Text>().text = strArray2[0] + LanguageDataManager.Instancce.GetEnterRoomLanguageText("已退出语音房间");
            }

            if (string.IsNullOrEmpty(strArray3[0]) != true && LanguageDataManager.Instancce.currentLanguage != Language.Chinese)
            {
                gameObject.GetComponent<Text>().text = strArray3[0] + LanguageDataManager.Instancce.GetEnterRoomLanguageText("已进入语音房间");
            }

        }
    }

}
