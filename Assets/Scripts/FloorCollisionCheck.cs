using UnityEngine;

public class FloorCollisionCheck : MonoBehaviour
{
    [SerializeField]
    PlatformMove m_PlatformMove;

    void OnTriggerEnter(Collider collider)
    {
        MoveController moveController = collider.GetComponent<MoveController>();
        if ( moveController )
        {
            m_PlatformMove.AddCharacter(moveController.transform);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        MoveController moveController = collider.GetComponent<MoveController>();
        if ( moveController )
        {
            m_PlatformMove.RemoveCharacter(moveController.transform);
        }
    }
}

