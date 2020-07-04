using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    bool m_IsWin = false;


    void OnTriggerEnter(Collider collider)
    {
        if ( m_IsWin == false ) return;
        
        MoveController moveController = collider.GetComponent<MoveController>();
        if ( moveController && moveController.m_IsDie == false )
        {
            UI.Instance.DoGameWin((int)moveController.transform.position.y);
            m_IsWin = true;
        }
    }
}
