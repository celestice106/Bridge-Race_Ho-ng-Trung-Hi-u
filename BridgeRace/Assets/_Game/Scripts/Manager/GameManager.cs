using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isWin = false;
    public bool isLose = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        UIManager.Ins.OpenUI<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Pause();
        }
        if (isWin)
        {
            UIManager.Ins.OpenUI<Win>();
        }
        if (isLose)
        {
            UIManager.Ins.OpenUI<Lose>();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        UIManager.Ins.OpenUI<GamePlay>();
    }
}
