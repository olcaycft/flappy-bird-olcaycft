using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float str = 5f;

    private void Update()
    {
        InputChecker();
        RefreshPosition();
    }


    private void InputChecker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            direction=Vector3.up*str;
        }
    }

    private void RefreshPosition()
    {
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }
}
