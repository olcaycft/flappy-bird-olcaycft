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
        else if (direction.y < 0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, -95f), 0.09f);
        }
    }

    private void BirdFallAnimation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, -95f), 0.1f);
    }
    private void BirdAnimation()
    {
        if (spriteIndex >= sprites.Length)
            spriteIndex++;
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

    }
}
