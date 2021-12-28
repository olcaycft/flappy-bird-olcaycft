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
    private Vector3 position;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float strForUp = 5.5f;
    [SerializeField] private float strForDown = 1.5f;

    private void OnEnable()
    {
        ResetPositionAndDirection();
    }

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
            direction=Vector3.up * strForUp;
        }
    }

    private void RefreshPosition()
    {
        direction.y += gravity * strForDown * Time.deltaTime;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Objective"))
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
        else if (col.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    private void ResetPositionAndDirection()
    {
        position = transform.position;
        position.y = 0f;
        transform.position = position;
        
        direction=Vector3.zero;
    }
}
