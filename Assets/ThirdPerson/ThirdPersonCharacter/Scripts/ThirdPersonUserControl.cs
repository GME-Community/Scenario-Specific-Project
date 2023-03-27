using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private MobileInputController Joystick;
    private Vector2 tmp;                      //get ruturn val from MobileInputController.sc
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.


    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        Joystick = GameObject.Find("MobileJoyStick/Panel/LeftJoyStick").GetComponent<MobileInputController>();//get MobileInputController.sc
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
        float RandomX = UnityEngine.Random.Range(-10.0f, 10.0f);

        float RandomZ = UnityEngine.Random.Range(-2.0f, 3.0f);

        m_Character.transform.position = new Vector3(55.0f + RandomX, -38.31f, 53.0f + RandomZ);
    }

    private void Update()
    {
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        tmp = Joystick.Coordinate();
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
        // walk speed multiplier
#if !MOBILE_INPUT
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif
        if (tmp.x != 0f || tmp.y != 0f)
        {
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = tmp.y * m_CamForward + tmp.x * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = tmp.y * Vector3.forward + tmp.x * Vector3.right;
            }

        }

        // pass all parameters to the character control script
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;
    }
}


