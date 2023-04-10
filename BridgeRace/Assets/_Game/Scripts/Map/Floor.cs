using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject bricks;
    [SerializeField] private int currentFloor;
    public List<ColorType> colorList = new List<ColorType>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameTag.CHARACTER))
        {
            if(!colorList.Contains(other.GetComponent<Character>().colorType))
            {
                colorList.Add(other.GetComponent<Character>().colorType);
            }
            BrickSpawner.ins.SetUp(currentFloor);
            if(other.GetComponent<Bot>() is Bot)
            {
                other.GetComponent<Bot>().currentFloor = currentFloor;
            }
        }
    }
}
