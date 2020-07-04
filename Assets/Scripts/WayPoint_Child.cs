using UnityEngine;

public class WayPoint_Child : MonoBehaviour
{
    public float m_IdleTime = 0f;

#if UNITY_EDITOR
    internal virtual void OnDrawGizmos()
    {
    }
#endif
}

