using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WayPoint : WayPoint_Child
{
    WayPoint_Child[] m_AllWayPoint;

    void Awake()
    {
        m_AllWayPoint = GetComponentsInChildren<WayPoint_Child>();
    }

    public int GetSizeOfWayPoint() => m_AllWayPoint.Length;

    public Vector3 GetPositionOfWayPoint(int index) => m_AllWayPoint[index].transform.position;

    public float GetIdleTime(int index) => m_AllWayPoint[index].m_IdleTime;

#if UNITY_EDITOR
    internal override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, 0.1f);


        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;

        Handles.Label(transform.position, transform.name, style);

        WayPoint_Child[] tss = GetComponentsInChildren<WayPoint_Child>();
        if ( tss != null )
        {
            for(int i = 0 ; i < tss.Length ; ++i)
            {
                WayPoint_Child ts = tss[i];
                if ( i == 0 )
                {
                    Gizmos.DrawLine(transform.position, ts.transform.position);
                }
                else
                {
                    Gizmos.DrawLine(tss[i-1].transform.position, ts.transform.position);
                }

                Gizmos.DrawWireSphere(ts.transform.position, 0.1f);
                Handles.Label(ts.transform.position, ts.name, style);
            }
        }
    }
#endif
}
