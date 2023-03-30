using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public Dictionary<int, List<GameObject>> floorBricks = new Dictionary<int, List<GameObject>>();

    public static BrickSpawner ins;

    private float timer;

    public List<GameObject> floorList;
    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        GetOriginalBrick();
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= GameConstants.BRICK_SPAWN_TIME)
        {
            SpawnBrick();
            timer = 0;
        }
    }
    

    public void SpawnBrick()
    {
        foreach(int key in floorBricks.Keys)
        {
            List<GameObject> brickList = floorBricks[key];

            foreach (GameObject brickObject in brickList)
            {
                if(!brickObject.activeInHierarchy)
                {
                    brickObject.GetComponent<Brick>().SetRandomColor();
                    brickObject.SetActive(true);
                }
            }
        }
    }

    private void GetOriginalBrick()
    {
        List<GameObject> brickList = new List<GameObject>();
        for (int floorNumber = 0; floorNumber < floorList.Count; floorNumber++)
        {
            foreach (Transform child in floorList[floorNumber].transform)
            {
                child.GetComponent<Brick>().SetRandomColor();
                brickList.Add(child.gameObject);
            }
            floorBricks.Add(floorNumber, brickList);
        }
    }
}
