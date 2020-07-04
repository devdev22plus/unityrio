using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField]
    Text m_Life, m_Score, m_Time;

    [SerializeField]
    Text m_GameRestartTimeText;

    [SerializeField]
    GameObject m_Ready, m_GameOver, m_GameWin;

    [SerializeField]
    float m_ReadyCounter = 3;

    [SerializeField]
    GameObject[] m_GamePlayObject;

    bool m_GameStart = false;

    float m_GameRestartTime = 3f;

    [SerializeField]
    float m_TimeLimit = 60; // seconds

    [SerializeField]
    DisplayScore m_DisplayScore;

    void Awake()
    {
        m_DisplayScore.gameObject.SetActive(false);

        m_Ready.gameObject.SetActive(true);
        m_GameOver.SetActive(false);
        m_GameWin.SetActive(false);

        m_GameRestartTimeText.gameObject.SetActive(false);

        foreach(var i in m_GamePlayObject)
        {
            i.gameObject.SetActive(false);
        }

        Instance = this;
    }

    void Update()
    {
        m_Life.text = "Life: " + GamePlay.m_Life;

        m_ReadyCounter -= Time.deltaTime;

        if ( m_GameOver.activeSelf || m_GameWin.activeSelf )
        {
            m_GameRestartTime -= Time.deltaTime;
            m_GameRestartTimeText.text = "restart in " + Mathf.Round(m_GameRestartTime);

            if ( m_GameRestartTime < 0f )
            {
                // Game Restart
                GamePlay.m_Life = 3;
                GamePlay.m_Score = 0;

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            if ( m_ReadyCounter <= 0.0f )
            {
                m_TimeLimit -= Time.deltaTime;
                if ( m_TimeLimit <= 0f )
                {
                    DoGameOver();

                    m_TimeLimit = 0f;
                }

                m_Time.text = "Time Remain: " + Mathf.Round(m_TimeLimit);
            }
        }

        m_Score.text = "Score: " + GamePlay.m_Score;

        if ( m_ReadyCounter <= 0.0f )
        {
            if ( m_GameStart == false )
            {
                foreach(var i in m_GamePlayObject)
                {
                    i.gameObject.SetActive(true);
                }

                m_Ready.SetActive(false);
            }

            m_GameStart = true;
        }
    }

    public void CreateScore(Vector3 pos, int score)
    {
        GameObject newObj = GameObject.Instantiate(m_DisplayScore.gameObject, Vector3.zero, Quaternion.identity);
        newObj.transform.SetParent(transform, false);
        newObj.transform.SetAsFirstSibling();

        Vector3 cameraPos = Camera.main.WorldToScreenPoint(pos);
        newObj.transform.position = cameraPos;

        var a = newObj.GetComponent<DisplayScore>();
        a.SetText("+" + score);

        newObj.gameObject.SetActive(true);

        m_TimeLimit += 10;
    }

    public void DoGameOver()
    {
        m_GameOver.SetActive(true);
        m_GameRestartTimeText.gameObject.SetActive(true);
    }

    public void DoGameWin(int score)
    {
        GamePlay.m_Score += (int)m_TimeLimit;
        m_GameRestartTime = 6f;

        m_GameWin.SetActive(true);
        m_GameRestartTimeText.gameObject.SetActive(true);
    }
}

