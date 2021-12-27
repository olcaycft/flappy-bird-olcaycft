using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer => _spriteRenderer ?? (_spriteRenderer = GetComponent<SpriteRenderer>());
    public Sprite[] sprites;
    private int spriteIndex;
    private float birdAnimationDuration = 0.12f;
    
    private Vector3 direction;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float str = 5f;

    private void Awake()
    {
        InvokeRepeating(nameof(BirdAnimation),birdAnimationDuration,birdAnimationDuration);
    }

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

    private void BirdAnimation()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
