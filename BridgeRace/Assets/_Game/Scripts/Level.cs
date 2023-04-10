using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public Transform[] spawnPositions;

    public Vector3 levelPosition = Vector3.zero;

    [SerializeField] private NavMeshData meshData;
    private NavMeshDataInstance instance;
    public GameObject finalTarget;

    private void Awake()
    {
        NavMesh.RemoveAllNavMeshData();
        instance = NavMesh.AddNavMeshData(meshData);
    }
    public void SpawnChar(List<Character> characters)
    {
        ShuffleArray(spawnPositions);
        for(int i = 0; i < spawnPositions.Length; i++)
        {
            characters[i].gameObject.transform.position = spawnPositions[i].position;
        }
    }

    public void OnDespawn()
    {
        NavMesh.RemoveNavMeshData(instance);
    }
    private void ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            T temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }
    }
}
