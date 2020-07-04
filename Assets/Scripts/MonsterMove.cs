using UnityEngine;


public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    public Animator m_Animator;

    [SerializeField]
    WayPoint m_WayPoint;

    [SerializeField]
    bool m_MoveReverb = false;

    int m_MoveWayPointIndex = -1;

    float m_IdleTIme = 0f;

    float m_DieTime = 0f;

    public enum EM_STATE
    {
        IDLE,
        MOVE,
        ATTACK,
        DIE,
    }

    internal EM_STATE m_State = EM_STATE.IDLE;

    // float m_LookSide = 90.0f; // right

    [SerializeField]
    float m_RotateSpeed = 5f;


    [SerializeField]
    float m_MoveSpeed = 5f;

    [SerializeField]
    bool m_MoveRight = false;

    [SerializeField]
    public int m_Score = 100;

    // [SerializeField]
    float m_Gravity = 3f;

    CapsuleCollider m_CapsuleCollider;

    Rigidbody m_Rig;


    void Awake()
    {
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
        m_Rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if ( m_WayPoint != null )
        {
            if ( m_MoveReverb )
            {
                m_MoveWayPointIndex = m_WayPoint.GetSizeOfWayPoint() - 1;
            }
            else
            {
                m_MoveWayPointIndex = 0;
            }
        }
    }

    public bool IsSideCheck(bool right)
    {
        Ray ray = new Ray(transform.position, right ? Vector3.right : -Vector3.right);
        RaycastHit hits;
        if ( Physics.Raycast(ray, out hits, m_CapsuleCollider.radius, (1 << 0) | (1 << 11) | (1 << 12)) )
        {
            return true;
        }

        return false;
    }

    public bool IsGround()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hits;
        if ( Physics.Raycast(ray, out hits, m_CapsuleCollider.height - m_CapsuleCollider.center.y, (1 << 0) | (1 << 11) | (1 << 12)) )
        {
            return true;
        }

        return false;
    }

    public void ClearCollider()
    {
        Destroy(m_CapsuleCollider);
        Destroy(m_Rig);
    }

    void Update()
    {
        m_IdleTIme -= Time.deltaTime;

        if ( m_State == EM_STATE.DIE )
        {
            m_DieTime += Time.deltaTime;

            if ( m_DieTime > 3f )
            {
                Destroy(gameObject);
            }
        }

        if ( m_IdleTIme <= 0f )
        {
            if ( m_WayPoint == null )
            {
                if ( m_State != EM_STATE.DIE )
                {
                    // if ( IsGround() == false )
                    // {
                    //     transform.position += (-Vector3.up * m_Gravity) * Time.deltaTime;
                    // }

                    if ( m_MoveRight )
                    {
                        if ( IsSideCheck(true) )
                        {
                            m_MoveRight = false;
                        }

                        transform.position += (Vector3.right * m_MoveSpeed) * Time.deltaTime;
                        transform.eulerAngles = new Vector3(0f, 90f, 0f);
                    }
                    else
                    {
                        if ( IsSideCheck(false) )
                        {
                            m_MoveRight = true;
                        }

                        transform.position -= (Vector3.right * m_MoveSpeed) * Time.deltaTime;
                        transform.eulerAngles = new Vector3(0f, 270f, 0f);
                    }
                    
                    m_Animator.Play("MOVE");
                }
            }
            else if ( m_MoveWayPointIndex != -1 && m_WayPoint != null )
            {
                Vector3 toPos = m_WayPoint.GetPositionOfWayPoint(m_MoveWayPointIndex);

                if ( Vector3.Distance(new Vector3(toPos.x , 0f, toPos.z), new Vector3(transform.position.x, 0f, transform.position.z)) < 0.01f )
                {
                    float idleTime = m_WayPoint.GetIdleTime(m_MoveWayPointIndex);

                    if ( m_MoveReverb )
                    {
                        m_MoveWayPointIndex--;
                        if ( m_MoveWayPointIndex < 0 )
                        {
                            m_MoveWayPointIndex = m_WayPoint.GetSizeOfWayPoint() - 1;
                        }
                    }
                    else
                    {
                        m_MoveWayPointIndex++;

                        if ( m_MoveWayPointIndex >= m_WayPoint.GetSizeOfWayPoint() )
                        {
                            m_MoveWayPointIndex = 0;
                        }

                    }

                    
                    if ( idleTime > 0f )
                    {
                        m_State = EM_STATE.IDLE;
                        m_IdleTIme = idleTime;
                    }

                    transform.position = toPos;

                    m_Animator.Play("IDLE");
                }
                else
                {
                    if ( m_State == EM_STATE.IDLE || m_State == EM_STATE.MOVE )
                    {
                        Vector3 dir = toPos - transform.position;
                        dir.Normalize();

                        // float angle = Vector3.Angle(Vector3.up, dir);
                        // transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, angle , 0f), m_RotateSpeed * Time.deltaTime);
                        transform.LookAt(new Vector3(toPos.x, transform.position.y, toPos.z));

                        // if ( angle == 90 || angle == 270 )
                        {
                            transform.position += (dir * m_MoveSpeed) * Time.deltaTime;
                            // transform.position = Vector3.Lerp(transform.position, toPos, m_MoveSpeed * Time.deltaTime);
                        }

                        m_State = EM_STATE.MOVE;

                        m_Animator.Play("MOVE");
                    }
                }
            }
        }
    }
}
