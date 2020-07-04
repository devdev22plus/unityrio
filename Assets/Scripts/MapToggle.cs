using UnityEngine;

public class MapToggle : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_EnableGameObjects;

    void Awake()
    {
        foreach(var i in m_EnableGameObjects)
        {
            i.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        MoveController moveController = collider.GetComponent<MoveController>();
        if ( moveController )
        {
            foreach(var i in m_EnableGameObjects)
            {
                i.SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
