using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappFall : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        MoveController moveController = collider.GetComponent<MoveController>();
        if ( moveController && moveController.m_IsDie == false )
        {
            GamePlay.m_Life = 0;
            moveController.DoDie();
        }

        MonsterMove monsterMove = collider.GetComponent<MonsterMove>();
        if ( monsterMove )
        {
            Destroy(monsterMove.gameObject);
        }
    }
}
