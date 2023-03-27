using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainControlScript : MonoBehaviour {
    private Image Img_fps;
    public GameObject mainCamera;

    // Use this for initialization
    void Start () {
        GameObject obj = Resources.Load<GameObject>("Prefabs/EnterRoomSenceV2");
        Instantiate(obj);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        
    }
	
	// Update is called once per frame
	void Update () {
        //Img_fps.transform.localPosition = Vector3.Lerp(new Vector3(958, -504, 0), new Vector3(958, 504, 0), Mathf.PingPong(Time.time * 0.1f, 1));
      // RotateSkybox();
    }
}
