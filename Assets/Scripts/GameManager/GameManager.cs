using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public static GameManager Instance;
    private bool isPaused = false;
    public bool IsPaused => isPaused;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetCursorVisible(true);
        SetPause(false);
    }

    public void SetCursorVisible(bool condition)
    {
        Cursor.visible = condition;
    }

    public void SetPause(bool condition)
    {
        isPaused = condition;
    }
}