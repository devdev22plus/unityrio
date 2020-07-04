using UnityEngine;


public class SiteCheck : MonoBehaviour
{
    public bool enter = false;

    void OnTrigerEnter()
    {
        enter = true;
    }

    void OnTrigerLeave()
    {
        enter = false;
    }
}