
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class BaseViewController : MonoBehaviour
{
#if UNITY_PS4 || UNITY_XBOXONE
    private long[] mLastClickTimeArr;
    private static int LONG_CLICK_TIME = 200;  //ms 
#endif
    private Image mLoadingPanel;
    private Text mTipLabel;
    private int mDotcnt;
    private string mLoadingTips;
    private float mLoadingRefreshInterval;
    protected bool mIsExperientialDemo = false;
    // Use this for initialization    
    virtual public void Start()
    {
        mDotcnt = 0;
        mLoadingRefreshInterval = 0;

        Button closeAVBtn = transform.Find("HomeBtn").gameObject.GetComponent<Button>();
        if (closeAVBtn)
        {
            closeAVBtn.onClick.AddListener(delegate()
            {
                this.onClickCloseAVBtn();
            });
        }

#if UNITY_PS4 || UNITY_XBOXONE
        long currentTime = (long)(new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds);
        mLastClickTimeArr = new long[8] { currentTime, currentTime, currentTime, currentTime, currentTime, currentTime, currentTime, currentTime };
#endif
    }

    // Update is called once per frame
    public virtual void Update()
    {
        refreshLoadingText();

#if UNITY_PS4 || UNITY_XBOXONE
        long currentTime = (long)(new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds);
        //enter key
        if (Input.GetButton("Circle"))
        {
            if (currentTime - mLastClickTimeArr[(int)GamepadKeyCode.Circle]> LONG_CLICK_TIME)
            {
                OnKeyDown(GamepadKeyCode.Circle, GamepadKeyEvent.KeyEventDown);
                mLastClickTimeArr[(int)GamepadKeyCode.Circle] = currentTime;
            }
        }
        else
        {
            if (mLastClickTimeArr[(int)GamepadKeyCode.Circle] != 0)
            {
                OnKeyDown(GamepadKeyCode.Circle, GamepadKeyEvent.KeyEventUp);
            }
            mLastClickTimeArr[(int)GamepadKeyCode.Circle] = 0;
        }
        //escape key
        if (Input.GetButton("X"))
        {
            if (currentTime - mLastClickTimeArr[(int)GamepadKeyCode.X] > LONG_CLICK_TIME)
            {
                OnKeyDown(GamepadKeyCode.X, GamepadKeyEvent.KeyEventDown);
                mLastClickTimeArr[(int)GamepadKeyCode.X] = currentTime;
            }
        }
        else
        {
            if (mLastClickTimeArr[(int)GamepadKeyCode.X] != 0)
            {
                OnKeyDown(GamepadKeyCode.X, GamepadKeyEvent.KeyEventUp);
            }
            mLastClickTimeArr[(int)GamepadKeyCode.X] = 0;
        }
        //up key
        if (Input.GetAxis("UpDown") > 0)
        {
            if (currentTime - mLastClickTimeArr[(int)GamepadKeyCode.Up]> LONG_CLICK_TIME)
            {
                OnKeyDown(GamepadKeyCode.Up, GamepadKeyEvent.KeyEventDown);
                mLastClickTimeArr[(int)GamepadKeyCode.Up] = currentTime;
            }
        }
        else
        //down key
        if (Input.GetAxis("UpDown") < 0)
        {
            if (currentTime - mLastClickTimeArr[(int)GamepadKeyCode.Down]> LONG_CLICK_TIME)
            {
                OnKeyDown(GamepadKeyCode.Down, GamepadKeyEvent.KeyEventDown);
                mLastClickTimeArr[(int)GamepadKeyCode.Down] = currentTime;
            }
        }
        //reset up/down key 
        else
        {
            if (mLastClickTimeArr[(int)GamepadKeyCode.Up] != 0)
            {
                OnKeyDown(GamepadKeyCode.Up, GamepadKeyEvent.KeyEventUp);
            }
            if (mLastClickTimeArr[(int)GamepadKeyCode.Down] != 0)
            {
                OnKeyDown(GamepadKeyCode.Down, GamepadKeyEvent.KeyEventUp);
            }
            mLastClickTimeArr[(int)GamepadKeyCode.Up] = 0;
            mLastClickTimeArr[(int)GamepadKeyCode.Down] = 0;
        }

        //left key
        if (Input.GetAxis("RightLeft") < 0)
        {
            if (currentTime - mLastClickTimeArr[(int)GamepadKeyCode.Left] > LONG_CLICK_TIME)
            {
                OnKeyDown(GamepadKeyCode.Left, GamepadKeyEvent.KeyEventDown);
                mLastClickTimeArr[(int)GamepadKeyCode.Left] = currentTime;
            }
        }
        //right key
        else if (Input.GetAxis("RightLeft") > 0)
        {
            if (currentTime - mLastClickTimeArr[(int)GamepadKeyCode.Right] > LONG_CLICK_TIME)
            {
                OnKeyDown(GamepadKeyCode.Right, GamepadKeyEvent.KeyEventDown);
                mLastClickTimeArr[(int)GamepadKeyCode.Right] = currentTime;
            }
        }
        //reset left/right key 
        else
        {
            if (mLastClickTimeArr[(int)GamepadKeyCode.Left] != 0)
            {
                OnKeyDown(GamepadKeyCode.Left, GamepadKeyEvent.KeyEventUp);
            }
            if (mLastClickTimeArr[(int)GamepadKeyCode.Right] != 0)
            {
                OnKeyDown(GamepadKeyCode.Right, GamepadKeyEvent.KeyEventUp);
            }
            mLastClickTimeArr[(int)GamepadKeyCode.Left] = 0;
            mLastClickTimeArr[(int)GamepadKeyCode.Right] = 0;
        }

        //joystick right key 
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (currentTime - mLastClickTimeArr[(int)GamepadKeyCode.JoystickRight] > LONG_CLICK_TIME)
            {
                OnKeyDown(GamepadKeyCode.JoystickRight, GamepadKeyEvent.KeyEventDown);
                mLastClickTimeArr[(int)GamepadKeyCode.JoystickRight] = currentTime;
            }
        }
        //joystick left key 
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (currentTime - mLastClickTimeArr[(int)GamepadKeyCode.JoystickLeft] > LONG_CLICK_TIME)
            {
                OnKeyDown(GamepadKeyCode.JoystickLeft, GamepadKeyEvent.KeyEventDown);
                mLastClickTimeArr[(int)GamepadKeyCode.JoystickLeft] = currentTime;
            }
        }
        //reset joystick left/right key 
        else
        {
            if(mLastClickTimeArr[(int)GamepadKeyCode.JoystickLeft] != 0)
            {
                OnKeyDown(GamepadKeyCode.JoystickLeft, GamepadKeyEvent.KeyEventUp);
            }
            if (mLastClickTimeArr[(int)GamepadKeyCode.JoystickRight] != 0)
            {
                OnKeyDown(GamepadKeyCode.JoystickRight, GamepadKeyEvent.KeyEventUp);
            }
            mLastClickTimeArr[(int)GamepadKeyCode.JoystickLeft] = 0;
            mLastClickTimeArr[(int)GamepadKeyCode.JoystickRight] = 0;
        }
#endif

    }

#if UNITY_PS4 || UNITY_XBOXONE
    public virtual void UpdateInput(){}

    public virtual void OnKeyDown(GamepadKeyCode key, GamepadKeyEvent keyEvent) {}
#endif

    void refreshLoadingText()
    {
        mLoadingRefreshInterval += Time.deltaTime;
        if (mLoadingRefreshInterval < 0.2)
        {
            return;
        }

        mLoadingRefreshInterval = 0;
        if (mTipLabel == null || mLoadingPanel.gameObject.activeSelf == false)
        {
            return;
        }

        mDotcnt += 1;
        if (mDotcnt >= 6)
        {
            mDotcnt = 0;
        }
        string displayText = mLoadingTips;
        for (int i = 0; i < mDotcnt; i++)
        {
            displayText = string.Format("{0}.", displayText);
        }
        mTipLabel.text = displayText;
    }

    void onClickCloseAVBtn()
    {
        closeScene();
    }

    public virtual void closeScene()
    {
        if (mIsExperientialDemo)
        {
            SceneManager.LoadScene("ExHomeScene");
            
        }
        else
        {
           if (SceneManager.GetActiveScene().name.Equals("HomeScene"))
            {
                SceneManager.LoadScene("Menu");
            }
            else if (SceneManager.GetActiveScene().name.Equals("ChatScene")) {
                GameObject.Find("PttScene(Clone)").GetComponent<Canvas>().enabled = false;
                GameObject.Find("ChatScene/PttPanel").gameObject.SetActive(false);
            }
            else
            {
                SceneManager.LoadScene("HomeScene");
            }
        }
    }


    public virtual void showLoadingView(bool isShow, string tip)
    {
        if (!isShow && mLoadingPanel == null)
        {
            return;
        }
        if (!isShow)
        {
            mLoadingPanel.gameObject.SetActive(false);
            return;
        }

        mLoadingTips = tip;
        if (mLoadingPanel == null)
        {
            Canvas cv = transform.GetComponent<Canvas>();

            GameObject loadingPanelObj = new GameObject();
            loadingPanelObj.layer = 5;
            loadingPanelObj.name = "loadingPanelObj";
            mLoadingPanel = loadingPanelObj.AddComponent<Image>();
            mLoadingPanel.transform.SetParent(cv.transform);
            mLoadingPanel.rectTransform.anchorMin = new Vector2(0f, 0f);
            mLoadingPanel.rectTransform.anchorMax = new Vector2(1f, 1f);
            mLoadingPanel.rectTransform.offsetMax = new Vector2(0f, 0f);
            mLoadingPanel.rectTransform.offsetMin = new Vector2(0f, 0f);
            mLoadingPanel.rectTransform.localScale = Vector3.one;
            mLoadingPanel.color = new Color(0.8f, 0.8f, 0.8f, 0.9f);

            GameObject tipLabelObj = new GameObject();
            tipLabelObj.layer = 5;
            tipLabelObj.name = "tipLabelObj";
            mTipLabel = tipLabelObj.AddComponent<Text>();
            mTipLabel.transform.SetParent(mLoadingPanel.transform);
            mTipLabel.rectTransform.sizeDelta = Vector2.zero;
            mTipLabel.rectTransform.anchorMin = Vector2.zero;
            mTipLabel.rectTransform.anchorMax = Vector2.one;
            mTipLabel.rectTransform.anchoredPosition = new Vector2(.5f, .5f);
            mTipLabel.rectTransform.localScale = Vector3.one;
            mTipLabel.text = mLoadingTips;
            mTipLabel.font = Resources.FindObjectsOfTypeAll<Font>()[0];
            mTipLabel.fontSize = 28;
            mTipLabel.color = Color.black;

            mTipLabel.alignment = TextAnchor.MiddleCenter;
        }
        mLoadingPanel.gameObject.SetActive(true);
    }


    public void CopyVoiceDataFromAssetsToPersistent()
    {
        string[] fileNames = {
            "alien-vocoder.dat",
            "cave.dat",
            "magic-chords-vocoder.dat",
            "punk-vocoder.dat",
            "robot-background.dat",
            "robot-vocoder.dat",
            "sleepyhead.dat",
            "walkie-counter-1.dat",
            "walkie-counter-2.dat",
            "walkie-counter-3.dat",
            "walkie-counter-4.dat",
            "walkie-terror-1.dat",
            "walkie-terror-2.dat",
            "walkie-terror-3.dat",
            "walkie-terror-4.dat"
        };

        string fromFolder = Path.Combine(Application.streamingAssetsPath, "VoiceData");
#if UNITY_PS4
        String toFolder = Path.Combine("/data/GMECacheDir/", "VoiceData");
#elif UNITY_PS5
        String toFolder = Path.Combine("/devlog/app/GMECacheDir/", "VoiceData");
#elif UNITY_SWITCH
        String toFolder = Path.Combine(Application.streamingAssetsPath, "VoiceData");
#else
        String toFolder = Path.Combine(Application.persistentDataPath, "VoiceData");
#endif

        if (!Directory.Exists(toFolder))
        {
            Directory.CreateDirectory(toFolder);
        }

        foreach (string name in fileNames)
        {
            string fromPath = Path.Combine(fromFolder, name);
            string toPath = Path.Combine(toFolder, name);
            StartCoroutine(CopyFile(fromPath, toPath));
        }
    }

    public IEnumerator CopyFile(string fromPath, string toPath)
    {
        if (!File.Exists(toPath))
        {
            Debug.Log("copying from " + fromPath + " to " + toPath);
#if UNITY_ANDROID && !UNITY_EDITOR
            WWW www1 = new WWW(fromPath);
            yield return www1;
            File.WriteAllBytes(toPath, www1.bytes);
            Debug.Log("file copy done");
            www1.Dispose();
            www1 = null;
#else
            File.WriteAllBytes(toPath, File.ReadAllBytes(fromPath));
#endif
        }
        yield return null;

    }


    public IEnumerator copyFileFromAssetsToPersistent(string fileName)
    {
        String fromPath = Application.streamingAssetsPath + "/" + fileName;
#if UNITY_PS4
        String toPath = "/data/GMECacheDir/" + fileName;
#elif UNITY_SWITCH
        String toPath = Application.streamingAssetsPath + "/" + fileName;
#else
        String toPath = Application.persistentDataPath + "/" + fileName;
#endif

        if (!File.Exists(toPath)) {
            Debug.Log("copying from " + fromPath + " to " + toPath);
#if UNITY_ANDROID && !UNITY_EDITOR
            WWW www1 = new WWW(fromPath);
            yield return www1;
            File.WriteAllBytes(toPath, www1.bytes);
            Debug.Log("file copy done");
            www1.Dispose();
            www1 = null;
#else
            File.WriteAllBytes(toPath,File.ReadAllBytes(fromPath));    
#endif

        }
        yield return null;
    }

#if UNITY_PS4 || UNITY_XBOXONE
    public enum GamepadKeyCode
    {
        Circle = 0,
        X = 1,
        Up = 2,
        Down = 3,
        Left = 4,
        Right = 5,
        JoystickLeft = 6,
        JoystickRight = 7,
    }

    public enum GamepadKeyEvent
    {
        KeyEventDown = 0,
        KeyEventUp = 1,
    }
#endif
}