using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    MoveController m_MoveController;

    [SerializeField]
    float m_MoveSpeed = 5f;

    [SerializeField]
    Vector3 m_Offset = Vector3.zero;

    void Update()
    {
        if ( m_MoveController.m_IsDie == false )
        {
            transform.position = Vector3.Lerp(transform.position, m_Offset + (new Vector3(m_MoveController.transform.position.x, m_MoveController.transform.position.y, transform.position.z) + m_MoveController.m_CameraOffset ), m_MoveSpeed * Time.deltaTime);
        }
    }
}