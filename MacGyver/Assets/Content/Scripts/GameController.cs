using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool gameStarted;
    [Header("Gaming")]
    public bool canTapNextWaypont = true;
    public int kills;
    public Transform[] waypoints;
    public int currentWaypoint;
    public int[] waypointsNeedToKill;
    [Header("UI")]
    public GameObject[] windows;
    private bool paused;

    private void Start()
    {
        Application.targetFrameRate = 120;
        OpenWindow(0);
    }

    public void StartGame()
    {
        OpenWindow(1);
        gameStarted = true;
    }

    public void Win()
    {
        OpenWindow(2);
        gameStarted = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OpenWindow(int number)
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        windows[number].SetActive(true);
    }

    public void Pause()
    {
        paused = !paused;

        if (paused)
        {
            OpenWindow(3);
            Time.timeScale = 0;
        }
        else
        {
            OpenWindow(1);
            Time.timeScale = 1;
        }
    }

    public void OnTapNextWaypont()
    {
        canTapNextWaypont = false;
    }

    public void OnEnemyKill()
    {
        kills++;

        if (kills >= waypointsNeedToKill[currentWaypoint])
        {
            kills = 0;
            canTapNextWaypont = true;
        }
    }
}