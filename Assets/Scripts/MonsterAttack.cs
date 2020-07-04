using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    MonsterMove m_MonsterMove;

    void OnTriggerEnter(Collider collider)
    {
        if ( m_MonsterMove.m_State == MonsterMove.EM_STATE.DIE ) return;

        MoveController moveController = collider.GetComponent<MoveController>();
        if ( moveController && moveController.m_IsDie == false )
        {
            moveController.DoDie();
        }
    }
}
