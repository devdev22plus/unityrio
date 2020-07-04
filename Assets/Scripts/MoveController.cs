using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public static MoveController Instance;

    [SerializeField]
    float m_RotateSpeed = 5f;

    [SerializeField]
    float m_MoveSpeed = 10f;

    [SerializeField]
    float m_JumpPower = 300f;

    [SerializeField]
    SiteCheck m_LeftBox, m_RightBox;

    // [SerializeField]
    // CharacterController m_CharacterController;

    [SerializeField]
    CapsuleCollider m_Collider;

    [SerializeField]
    Animator m_Animator;

    [SerializeField]
    Rigidbody m_RigidBody;

    float m_LookSide = 90.0f; // right

    internal Vector3 m_CameraOffset = Vector3.zero;

    [SerializeField]
    float m_CameraMoveOffsetSpeed = 10.0f;

    internal bool m_IsDie = false;

    CapsuleCollider m_CapsuleCollider;

    Vector3 m_LastPosition = Vector3.zero;


    void Awake()
    {
        Instance = this;

        m_CapsuleCollider = GetComponent<CapsuleCollider>();
    }

    public bool IsGround()
    {
        Ray ray = new Ray(transform.position + m_Collider.center, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, m_Collider.center.y + 0.1f, (1 << 0));
        if (hits != null)
        {
            return hits.ToList().FindAll(x => x.collider.gameObject != gameObject).Count > 0;
        }
        
        return true;

        // return m_CharacterController.isGrounded;
    }

    public bool IsSideFree(bool right)
    {
        return right ? m_RightBox.enter : m_LeftBox.enter;
    }

    public bool IsCanMoveSide(bool right)
    {
        Transform ts = right ? m_RightBox.transform : m_LeftBox.transform;

        Ray ray = new Ray(ts.position, right ? Vector3.right : -Vector3.right);
        RaycastHit hits;
        if ( Physics.Raycast(ray, out hits, m_CapsuleCollider.radius + 0.1f, (1 << 11) | (1 << 12)) )
        {
            return false;
        }

        return true;
    }

    public void DoDie()
    {
        --GamePlay.m_Life;

        DoJump();

        if ( GamePlay.m_Life >= 0 )
        {
        }
        else
        {
            UI.Instance.DoGameOver();
            
            m_Collider.isTrigger = true;

            m_Animator.SetBool("Die", true);
            m_IsDie = true;
        }
    }

    void Update()
    {
        bool run = false;

        Vector3 cameraOffset = Vector3.zero;

        if ( m_IsDie == false )
        {
            if ( Input.GetKey(KeyCode.A)
                // && ((IsSideFree(false) && IsGround() == false) || IsGround())
                && IsSideFree(false) == false
                && IsCanMoveSide(false)
                )
            {
                m_LookSide = 270.0f;
                cameraOffset -= (Vector3.right * 1.5f);

                // if ( m_RigidBody.velocity.x > -3 )
                //     m_RigidBody.AddForce(transform.forward * m_MoveSpeed);

                Vector3 pos = transform.position + transform.forward * m_MoveSpeed * Time.deltaTime;
                pos.z = 0f;

                transform.position = pos;

                run = true;
            }
            else if ( Input.GetKey(KeyCode.D)
                // && ((IsSideFree(true) && IsGround() == false) || IsGround())
                && IsSideFree(true) == false
                && IsCanMoveSide(true)
                )
            {
                m_LookSide = 90.0f;
                cameraOffset += (Vector3.right * 1.5f);


                // if ( m_RigidBody.velocity.x < 3 )
                //     m_RigidBody.AddForce(transform.forward * m_MoveSpeed);

                Vector3 pos = transform.position + transform.forward * m_MoveSpeed * Time.deltaTime;
                pos.z = 0f;

                transform.position = pos;

                run = true;
            }
            
            if ( Input.GetKey(KeyCode.W) )
            {
                // cameraOffset += (transform.forward * 1.5f) + (transform.up * 1f);
                cameraOffset += (Vector3.up * 1f);
            }
            else if ( Input.GetKey(KeyCode.S) )
            {
                // cameraOffset += (transform.forward * 1.5f) - (transform.up * 2f);
                cameraOffset += -(Vector3.up * 2f);
            }


            if ( Input.GetKeyDown(KeyCode.Space) && IsGround() )
            {
                m_RigidBody.AddForce(new Vector3(0f, m_JumpPower, 0f));
            }
        }

        
        m_CameraOffset = Vector3.Lerp(m_CameraOffset, cameraOffset, m_CameraMoveOffsetSpeed * Time.deltaTime);


        m_LastPosition = transform.position;


        m_Animator.SetBool("Run", run);
    }

    public void DoJump()
    {
        m_RigidBody.velocity = Vector3.zero;
        m_RigidBody.AddForce(new Vector3(0f, m_JumpPower, 0f));
    }

    void LateUpdate()
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, m_LookSide, 0f), m_RotateSpeed * Time.deltaTime);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
    }
#endif
}
