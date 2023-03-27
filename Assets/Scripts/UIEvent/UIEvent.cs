using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvent : MonoBehaviour
{

    private Image mFacebookImage;
    private Image mLinkedInImage;
    private Image mTwitterImage;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Awake()
    {
        mFacebookImage = GameObject.Find("facebookImage").GetComponent<Image>();
        mLinkedInImage = GameObject.Find("linkedInImage").GetComponent<Image>();
        mTwitterImage = GameObject.Find("twitterImage").GetComponent<Image>();

        if(mFacebookImage)
        {
            ClickImageEventListener.Get(mFacebookImage.gameObject).onClick = OnFacebookImageClick;
        }

        if (mLinkedInImage)
        {
            ClickImageEventListener.Get(mLinkedInImage.gameObject).onClick = OnLinkedInImageClick;
        }

        if (mTwitterImage)
        {
            ClickImageEventListener.Get(mTwitterImage.gameObject).onClick = OnTwitterImageClick;
        }
    }

    void OnFacebookImageClick(GameObject obj)
    {
        Application.OpenURL("https://www.facebook.com/TencentGME");
    }

    void OnLinkedInImageClick(GameObject obj)
    {
        Application.OpenURL("https://www.linkedin.com/company/tencentgme");
    }

    void OnTwitterImageClick(GameObject obj)
    {
        Application.OpenURL("https://twitter.com/TencentGME");
    }

}
