using System;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer => _spriteRenderer ?? (_spriteRenderer = GetComponent<SpriteRenderer>());

    public Sprite[] sprites;
    private int spriteIndex;
    [SerializeField] private float birdAnimationDuration => SettingsManager.GameSettings.birdAnimationDuration;

    private Vector3 direction;
    private Vector3 position;

    private Quaternion rotation;
    [SerializeField] private float fallAnimationCounter = 0.60f;

    [SerializeField] private float gravity => SettingsManager.GameSettings.gravity;
    [SerializeField] private float strForUp => SettingsManager.GameSettings.strForUp;
    [SerializeField] private float strForDown => SettingsManager.GameSettings.strForDown;


    private void OnEnable()
    {
        ResetPositionAndDirection();
    }

    private void Awake()
    {
        InvokeRepeating(nameof(BirdAnimation), birdAnimationDuration, birdAnimationDuration);
    }

    private void Update()
    {
        InputChecker();
        RefreshPosition();
        ChangeRotation();
    }


    private void InputChecker()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.isBirdEnable)
        {
            direction = Vector3.up * strForUp;
            fallAnimationCounter = 0.60f;
        }
        else
        {
            DecreaseFallAnimationCounter();
        }
    }

    private void RefreshPosition()
    {
        direction.y += gravity * strForDown * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void ChangeRotation()
    {
        if (direction.y > 0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 20f), 0.5f);
        }
        else if (direction.y < 0f && fallAnimationCounter <= 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, -95f), 0.09f);


        }
    }

    private void BirdFallAnimation()
    {
        fallAnimationCounter = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, -95f), 0.1f);
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

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Objective"))
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.isBirdEnable = false;
            BirdFallAnimation();
            if (other.gameObject.CompareTag("Ground"))
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    private void ResetPositionAndDirection()
    {
        position = transform.position;
        position.y = 0f;
        position.z = 0f;
        transform.position = position;

        direction = Vector3.zero;

        transform.rotation = Quaternion.identity;
    }

    private void DecreaseFallAnimationCounter()
    {
        if (fallAnimationCounter >= 0)
        {
            fallAnimationCounter -= 1 * Time.deltaTime;
        }

    }
}
