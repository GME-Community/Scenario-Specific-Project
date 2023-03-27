using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;
using GME;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class ThirdPersonCharacter : NetworkBehaviour
{
    public struct st_Personinfo
    {
        public bool isShoW;
        public bool bShowStatus;

        public bool bShowInfo;
        public string uin;
        public bool bGmeStff;
    }

    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_JumpPower = 12f;
    [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
    [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;
    [SerializeField] GameObject[] Quad_Speaker;
    [SerializeField] GameObject gme_stff_english;
    [SerializeField] GameObject gme_stff_chinese;

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    bool m_IsGrounded;
    float m_OrigGroundCheckDistance;
    const float k_Half = 0.5f;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;
    float m_CapsuleHeight;
    Vector3 m_CapsuleCenter;
    CapsuleCollider m_Capsule;
    bool m_Crouching;
    bool bCreate = false;
    [SerializeField] GameObject RangeCirclePrefab;
    GameObject mRangeObj = null;
    private static int rangeInstance = 5;
    public st_Personinfo mPersonInfo = new st_Personinfo();
    
    private static Dictionary<string, string> mPersonUinMap = new Dictionary<string, string>();
    const string PERSON_NAME = "202302320";
    GameObject mPersonInfoObj= null;
    private Language mcurrentLanguage = Language.Chinese;
    private static GameObject mCamer = null;
    private SkinnedMeshRenderer mSkinnedMeshRenderer = null;

    [SyncVar]
    bool isShow = false;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
        m_CapsuleHeight = m_Capsule.height;
        m_CapsuleCenter = m_Capsule.center;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        m_OrigGroundCheckDistance = m_GroundCheckDistance;
        Setting_Script.m_callback += new CallbackDelegate(HandleCallbackDelegate);
        mPersonInfo.isShoW = true;
        mPersonInfo.bShowStatus = true;
        mPersonInfo.bShowInfo = false;
        mPersonInfo.uin = UserConfig.GetUserID();
        InitPersonUin();

        if (isLocalPlayer)   //�ж��Ƿ��Ǳ��ؿͻ���
        {
            GameObject go = GameObject.Find("Cameras").transform.Find("FreeLookCameraRig").gameObject;
            go.GetComponent<FreeLookCam>().SetTarget(this.transform);
            mCamer = go;
        }

    }

    private static void InitPersonUin()
    {
        if (mPersonUinMap.Count > 0)
        {
            return;
        }
        for(int i = 0; i < 10; i++)
        {
            string uin = PERSON_NAME + Convert.ToString(i);
            mPersonUinMap.Add(uin, Convert.ToString(i));
        }
    }

    void HandleCallbackDelegate(bool show)
    {
        mPersonInfo.isShoW = show;
    }
    
    private void Update()
    {
        if (!isLocalPlayer)
        {
            Quad_Speaker[0].SetActive(isShow);
            Quad_Speaker[1].SetActive(isShow);               
        }

        if (!bCreate)
        { 
            CreateCircle();
            bCreate = true;
            return;
        }
        
        if(mPersonInfo.isShoW != mPersonInfo.bShowStatus)
        {
            mPersonInfo.bShowStatus = mPersonInfo.isShoW;
            CmdSend(mPersonInfo.isShoW);
        }
        if(isLocalPlayer && !mPersonInfo.bShowInfo && IsGmePersonUin(mPersonInfo.uin))
        {
            CreatePersonInfo(mPersonInfo.uin);
            CmdSendUin(mPersonInfo.uin);
            mPersonInfo.bGmeStff = true;
        }

        // 如果切换中英文时显示gme工作人员的图片也要切换
        if(LanguageDataManager.Instancce.currentLanguage != mcurrentLanguage && mPersonInfoObj)
        {
            CreatePersonInfo(mPersonInfo.uin);
        }

        if(isLocalPlayer && IsGmePersonUin(mPersonInfo.uin) && mPersonInfoObj)
        {
            CmdSendUin(mPersonInfo.uin);
        }


        if (mSkinnedMeshRenderer == null)
        {
            mSkinnedMeshRenderer = gameObject.transform.Find("EthanBody").GetComponent<SkinnedMeshRenderer>();
        }
        
        if (!isLocalPlayer && mPersonInfo.bGmeStff)
        {
            mSkinnedMeshRenderer.material.color = new Color(1, 1, 0, 0);
        } 
        if (!isLocalPlayer && !mPersonInfo.bGmeStff)
        {
            mSkinnedMeshRenderer.material.color = Color.white;
        }
        if (isLocalPlayer && !mPersonInfo.bGmeStff)
        {

            mSkinnedMeshRenderer.material.color = Color.white;
        }
        
    }

    public static bool IsGmePersonUin(string uin)
    {
        if(uin == null || uin == "")
        {
            return false;
        }
        if(mPersonUinMap.Count == 0)
        {
            InitPersonUin();
        }
        if (mPersonUinMap.Count !=0 && mPersonUinMap.ContainsKey(uin))
        {
            return true;
        }
        
        return false;
    }

    public float GetDistance(Vector3 startPoint, Vector3 endPoint)
    {
        float distance = (startPoint - endPoint).magnitude;
        return distance;
    }

    //  [Command]
    private void CreateCircle()
    {
        Transform tmp = gameObject.transform;
        tmp.position = new Vector3(tmp.position.x, gameObject.transform.position.y + 0.5f, tmp.position.z);
        print("tmp.position: " + tmp.position.x + " y:" + tmp.position.y + " z: " + tmp.position.z);
        mRangeObj = (GameObject)Instantiate(
            RangeCirclePrefab,
            tmp.position,
            Quaternion.identity);
        mRangeObj.transform.rotation = Quaternion.Euler(90f, 0.0f, 0.0f);

        var size = mRangeObj.transform.GetComponent<Renderer>().bounds.size;

        float scaleNum = (float)((float)rangeInstance / size.x) * 2.0f;

        scaleNum += 0.32f;
        print("scaleNum: " + scaleNum);
        mRangeObj.transform.localScale = new Vector3(scaleNum, scaleNum, scaleNum);

    }

    /// <summary>
    /// 使用Command修饰的函数表示在客户端调用，在服务端执行
    /// </summary>
    /// <param name="str"></param>
    [Command]
    void CmdSend(bool show)
    {
        RpcShowMessage(show);
    }
    /// <summary>
    /// ClientRpc修饰的函数 表示由服务端调用，在所有客户端执行
    /// </summary>
    /// <param name="str"></param>
    [ClientRpc]
    void RpcShowMessage(bool show)
    {
        if (mRangeObj)
        {
            mRangeObj.SetActive(show);
            //Destroy(this.mRangeObj);
        }
    }

    [Command]
    void CmdSendUin(string uin)
    {
        RpcShowUin(uin);
    }
    
    [ClientRpc]
    void RpcShowUin(string uin)
    {
        if (uin != UserConfig.GetUserID())
        {
            CreatePersonInfo(uin);
            mPersonInfo.bGmeStff = true;
            mPersonInfo.uin = uin;
        }
        
    }

    private void CreatePersonInfo(string uin)
    {
        if (LanguageDataManager.Instancce.currentLanguage != mcurrentLanguage || mPersonInfoObj == null)
        {
            if(mPersonInfoObj)
            {
                mPersonInfoObj.SetActive(false);
                Destroy(mPersonInfoObj);
                mPersonInfoObj = null;
            }
            mcurrentLanguage = LanguageDataManager.Instancce.currentLanguage;

            Transform tmp = gameObject.transform;
            tmp.position = new Vector3(tmp.position.x, gameObject.transform.position.y + 0.5f, tmp.position.z);
            if (LanguageDataManager.Instancce.currentLanguage == Language.Chinese)
            {
              
                mPersonInfoObj = (GameObject)Instantiate(
                    gme_stff_chinese,
                    tmp.position,
                    Quaternion.identity);
            }
            else
            {
                mPersonInfoObj = (GameObject)Instantiate(
                    gme_stff_english,
                    tmp.position,
                    Quaternion.identity);
            }
            mPersonInfo.bShowInfo = true;

            float rotationY = 0.0f;
            if (mCamer)
            {
                rotationY = mCamer.transform.localEulerAngles.y;
            }

            mPersonInfoObj.transform.rotation = Quaternion.Euler(-50.0f, rotationY, 0.0f);

            mPersonInfoObj.transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
        }

    }
    
    public static void SetPersonInfo(int rangle)
    {
        rangeInstance = rangle;
    }

    public override void OnNetworkDestroy()
    {
        print("OnNetworkDestroy");
        if (mRangeObj)
        {
            mRangeObj.SetActive(false);
            Destroy(this.mRangeObj);
            mRangeObj = null;
        }
        
        if (mPersonInfoObj)
        {
            mPersonInfoObj.SetActive(false);
            Destroy(this.mPersonInfoObj);
            mPersonInfoObj = null;
        }

        base.OnNetworkDestroy();
    }
    
    //public override void 

    private void OnDestroy()
    {
        if (mRangeObj)
        {
            mRangeObj.SetActive(false);
            Destroy(this.mRangeObj);
            mRangeObj = null;
        }

        if (mPersonInfoObj)
        {
            mPersonInfoObj.SetActive(false);
            Destroy(this.mPersonInfoObj);
            mPersonInfoObj = null;
        }
        
        Setting_Script.m_callback -= new CallbackDelegate(HandleCallbackDelegate);
        
        Destroy(this.gameObject);
     }

    public void Move(Vector3 move, bool crouch, bool jump)
    {
        if (!isLocalPlayer)   //�ж��Ƿ��Ǳ��ؿͻ���
        {
            if (mRangeObj)
            {
                mRangeObj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);

                if (mPersonInfoObj)
                {
                    mPersonInfoObj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.8f, gameObject.transform.position.z);
                    if (mCamer)
                    {
                        mPersonInfoObj.transform.rotation = Quaternion.Euler(-50, mCamer.transform.localEulerAngles.y - 180, 0);
                    }
                    else
                    {
                        mPersonInfoObj.transform.rotation = Quaternion.Euler(-50, 0, 0);

                    }
                }
            }
            return;
        }
        if (mRangeObj)
        {
            mRangeObj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);

            if (mPersonInfoObj)
            {
             mPersonInfoObj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.8f, gameObject.transform.position.z);
             mPersonInfoObj.transform.rotation = Quaternion.Euler(-50, mCamer.transform.localEulerAngles.y - 180, 0);
            }
        }

        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;

        ApplyExtraTurnRotation();

        // control and velocity handling is different when grounded and airborne:
        if (m_IsGrounded)
        {
            HandleGroundedMovement(crouch, jump);
        }
        else
        {
            HandleAirborneMovement();
        }

        ScaleCapsuleForCrouching(crouch);
        PreventStandingInLowHeadroom();

        // send input and other state parameters to the animator
        UpdateAnimator(move);

        int x = (int)(gameObject.transform.position.x * 100);
        int y = 0;
        int z = (int)(gameObject.transform.position.z * 100);
        float xRotate = gameObject.transform.eulerAngles.x;
        float yRotate = gameObject.transform.eulerAngles.y;
        float zRotate = gameObject.transform.eulerAngles.z;

        Quaternion rotation = Quaternion.Euler((float)xRotate, (float)yRotate, (float)zRotate);
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);


        int[] position = new int[3] { z, x, y };
        float[] axisForward = new float[3] { matrix.m22, matrix.m02, matrix.m12 };
        float[] axisRight = new float[3] { matrix.m20, matrix.m00, matrix.m10 };
        float[] axisUp = new float[3] { matrix.m21, matrix.m01, matrix.m11 };
        
        ITMGContext.GetInstance().GetRoom().UpdateSelfPosition(position, axisForward, axisRight, axisUp);

       // print("position: x:  " + gameObject.transform.position.x + " y: " + gameObject.transform.position.y + " z: " + gameObject.transform.position.z );

    }
    
    void ScaleCapsuleForCrouching(bool crouch)
    {
        if (m_IsGrounded && crouch)
        {
            if (m_Crouching) return;
            m_Capsule.height = m_Capsule.height / 2f;
            m_Capsule.center = m_Capsule.center / 2f;
            m_Crouching = true;
        }
        else
        {
            Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
            if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_Crouching = true;
                return;
            }
            m_Capsule.height = m_CapsuleHeight;
            m_Capsule.center = m_CapsuleCenter;
            m_Crouching = false;
        }
    }

    void PreventStandingInLowHeadroom()
    {
        // prevent standing up in crouch-only zones
        if (!m_Crouching)
        {
            Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
            if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_Crouching = true;
            }
        }
    }


    void UpdateAnimator(Vector3 move)
    {
        if (!isLocalPlayer)   //�ж��Ƿ��Ǳ��ؿͻ���
        {
            return;
        }
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", m_Crouching);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        float runCycle =
            Mathf.Repeat(
                m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
        if (m_IsGrounded)
        {
            m_Animator.SetFloat("JumpLeg", jumpLeg);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (m_IsGrounded && move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
        }
    }


    void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
        m_Rigidbody.AddForce(extraGravityForce);

        m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
    }


    void HandleGroundedMovement(bool crouch, bool jump)
    {
        // check whether conditions are right to allow a jump:
        if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            // jump!
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
            m_IsGrounded = false;
            m_Animator.applyRootMotion = false;
            m_GroundCheckDistance = 0.1f;
        }
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }


    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (m_IsGrounded && Time.deltaTime > 0)
        {
            Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = m_Rigidbody.velocity.y;
            m_Rigidbody.velocity = v;
        }
    }


    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            m_Animator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            m_Animator.applyRootMotion = false;
        }
    }

}