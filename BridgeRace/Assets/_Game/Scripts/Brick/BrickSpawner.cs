using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public Dictionary<int, List<GameObject>> floorBricks = new Dictionary<int, List<GameObject>>();

    public static BrickSpawner ins;

    public List<GameObject> brickList = new List<GameObject>();
    private float timer;
    private List<int> floorIndex = new List<int>();
    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        GetOriginalBrick();
        SetUp(0);
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= GameConstants.BRICK_SPAWN_TIME)
        {
            SpawnBrick();
            timer = 0;
        }
    }
    public void SetUp(int floorNumber)
    {
        floorIndex.Add(floorNumber);
        SpawnBrick();
    }

    public void SpawnBrick()
    {
        foreach (int index in floorIndex)
        {
            List<GameObject> bricks = floorBricks[index];
            int count = 0;
            foreach (GameObject brickObject in bricks)
            {
                if (!brickObject.activeInHierarchy)
                {
                    count++;
                    if (count <= 20)
                    {
                        brickObject.GetComponent<Brick>().ChangeColor(brickList[index].GetComponent<Floor>().colorList[Random.Range(0, brickList[index].GetComponent<Floor>().colorList.Count)]);
                        brickObject.SetActive(true);
                    }
                }
            }
        }
    }

    private void GetOriginalBrick()
    {
        for (int floorNumber = 0; floorNumber < brickList.Count; floorNumber++)
        {
            List<GameObject> bricks = new List<GameObject>();
            foreach (Transform child in brickList[floorNumber].transform)
            {
                bricks.Add(child.gameObject);
                child.GetComponent<Brick>().ChangeColor((ColorType)Random.Range(0, 3));
            }
            floorBricks.Add(floorNumber, bricks);
        }
    }
}
