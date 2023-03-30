using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager ins;

    private void Awake()
    {
        ins = this;
    }

    public int score = 0;
}
