using UnityEngine;

public class DeadPoint : MonoBehaviour
{
    [SerializeField]
    MonsterMove m_MonsterMove;

    void OnTriggerEnter(Collider collider)
    {
        if ( m_MonsterMove.m_State == MonsterMove.EM_STATE.DIE ) return;

        MoveController moveController = collider.GetComponent<MoveController>();
        if ( moveController )
        {
            // var playerCollider = moveController.GetComponent<CapsuleCollider>();
            // var monsterCollider = GetComponent<CapsuleCollider>();

            // if ( (moveController.transform.position.y - playerCollider.center.y) > (transform.position.y + monsterCollider.center.y) )
            // if ( moveController.transform.position.y > transform.position.y )
            {
                m_MonsterMove.m_State = MonsterMove.EM_STATE.DIE;

                m_MonsterMove.m_Animator.Play("DIE");

                moveController.DoJump();

                UI.Instance.CreateScore(moveController.transform.position + new Vector3(0f, 1f, 0f), m_MonsterMove.m_Score);

                m_MonsterMove.ClearCollider();
            }
        }
    }
}

