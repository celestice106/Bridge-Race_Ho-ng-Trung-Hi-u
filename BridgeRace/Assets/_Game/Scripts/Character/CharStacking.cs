using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStack : Character
{
    public static PlayerStack ins;

    private void Awake()
    {
        ins = this;
    }
   
}
