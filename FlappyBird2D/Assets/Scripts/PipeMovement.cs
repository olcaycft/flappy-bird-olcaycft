using System;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] private float pipeSpeed = 5f;
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;//left edge is vector3.zero 
    }

    private void Update()
    {
        MovePipe();
        DestroyPipe();
    }

    private void MovePipe()
    {
        transform.position+=Vector3.left*pipeSpeed*Time.deltaTime;
    }

    private void DestroyPipe()
    {
        if (transform.position.x<leftEdge)
        {
            Destroy(this.gameObject);
        }
    }
}
