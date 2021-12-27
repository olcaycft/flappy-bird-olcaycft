using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float minHeight = -1f;
    [SerializeField] private float maxHeight = 1f;
    
    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawner),spawnRate,spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawner));
    }

    private void Spawner()
    {
        GameObject pipes = Instantiate(prefab,transform.position,Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
