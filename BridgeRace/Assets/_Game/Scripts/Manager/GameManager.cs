using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        UIManager.Ins.OpenUI<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("reached");
            Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        UIManager.Ins.OpenUI<GamePlay>();
    }
}
