using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    [SerializeField]
    Text m_Text;

    [SerializeField]
    float m_FadeSpeed = 3f;

    void Update()
    {
        //slideup here


        // fade
        m_Text.color = Color.Lerp(m_Text.color, new Color(m_Text.color.r, m_Text.color.g, m_Text.color.b, 0f), m_FadeSpeed * Time.deltaTime);
        
        if ( m_Text.color.a <= 0f )
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string txt)
    {
        m_Text.text = txt;
    }
}
