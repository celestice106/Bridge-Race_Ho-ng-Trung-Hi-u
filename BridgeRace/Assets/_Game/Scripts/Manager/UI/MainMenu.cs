using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    { 
        Time.timeScale = 1;
        Close(0);
    }
}
