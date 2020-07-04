using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDynamic : MonoBehaviour
{
    Collider m_Collider;

    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    void LateUpdate()
    {
        m_Collider.isTrigger = MoveController.Instance.transform.position.y + 0.1f < transform.position.y;
    }
}
