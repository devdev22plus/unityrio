using System.Collections.Generic;
using UnityEngine;


public class PlatformMove : MonoBehaviour
{
    [SerializeField]
    WayPoint m_WayPoint;

    int m_MoveWayPointIndex = -1;

    float m_IdleTIme = 0f;

    [SerializeField]
    float m_MoveSpeed = 5f;

    Vector3 m_MoveVelocity = Vector3.zero;
    Vector3 m_LastPosition = Vector3.zero;

    internal List<Transform> m_CharacterTransform = new List<Transform>();

    void Start()
    {
        if ( m_WayPoint != null )
        {
            m_MoveWayPointIndex = 0;
        }
    }

    public void AddCharacter(Transform ts)
    {
        if (m_CharacterTransform.Exists(x => x == ts) == false)
        {
            m_CharacterTransform.Add(ts);

            Debug.Log("AddCharacter:" + ts,ts);
        }
    }

    public void RemoveCharacter(Transform ts)
    {
        Debug.Log("RemoveCharacter:" + ts,ts);
        m_CharacterTransform.Remove(ts);
    }

    void Update()
    {
        m_IdleTIme -= Time.deltaTime;

        if ( m_IdleTIme <= 0f )
        {
            if ( m_MoveWayPointIndex != -1 && m_WayPoint != null )
            {
                Vector3 toPos = m_WayPoint.GetPositionOfWayPoint(m_MoveWayPointIndex);

                if ( Vector3.Distance(new Vector3(toPos.x , 0f, toPos.z), new Vector3(transform.position.x, 0f, transform.position.z)) < 0.01f )
                {
                    float idleTime = m_WayPoint.GetIdleTime(m_MoveWayPointIndex);

                    m_MoveWayPointIndex++;

                    if ( m_MoveWayPointIndex >= m_WayPoint.GetSizeOfWayPoint() )
                    {
                        m_MoveWayPointIndex = 0;
                    }

                    if ( idleTime > 0f )
                    {
                        m_IdleTIme = idleTime;
                    }

                    transform.position = toPos;
                }
                else
                {
                    Vector3 dir = toPos - transform.position;
                    dir.Normalize();
                    transform.position += (dir * m_MoveSpeed) * Time.deltaTime;

                    // transform.position = Vector3.Lerp(transform.position, toPos, m_MoveSpeed * Time.deltaTime);
                }

                m_MoveVelocity = transform.position - m_LastPosition;

                foreach(var i in m_CharacterTransform)
                {
                    i.position += m_MoveVelocity;
                }

                m_LastPosition = transform.position;
            }
        }
    }
}
