using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawing : MonoBehaviour
{
    public static BrickSpawing ins;

    public int numberBricksTaken = 0;

    public Vector3[] blankPos;


    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        blankPos = new Vector3[100];
    }


    private void Update()
    {

    }
    
    public void GetTakenBricksPos(GameObject brick)
    {
        blankPos[numberBricksTaken] = brick.transform.position;
        numberBricksTaken++;
    }

    public IEnumerator SpawnBrick()
    {

        yield return new WaitForSeconds(GameConstants.BRICK_SPAWN_TIME);

        for (int i = numberBricksTaken - 1; i > -1; i--)
        {
            GameObject brick = ObjectPool.ins.GetPooledObject();
            if (blankPos[i] != Vector3.zero)
            {
                if (brick != null)
                {
                    brick.transform.position = blankPos[i];
                    brick.SetActive(true);
                    blankPos[i] = Vector3.zero;
                    numberBricksTaken--;
                }
            }
        }
    }

}
