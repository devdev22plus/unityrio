using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        MoveController moveController = collision.gameObject.GetComponent<MoveController>();
        if ( moveController )
        {
            GamePlay.m_Life++;
            Destroy(gameObject);
        }
    }
}
