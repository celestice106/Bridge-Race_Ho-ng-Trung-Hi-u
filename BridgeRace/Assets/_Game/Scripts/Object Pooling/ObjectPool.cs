using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool ins;
    public List<GameObject> pooledObject;
    public GameObject objectToPool;
    public int amountToPool;



    // Start is called before the first frame update
    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {

        pooledObject = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObject.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i];
            }
        }
        return null;
    }
}
