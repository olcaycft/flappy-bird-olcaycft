using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private MeshRenderer meshRenderer => _meshRenderer ?? (_meshRenderer = GetComponent<MeshRenderer>());

    [SerializeField] private float animationSpeed = 1f; //this will change for background and ground.

    private void Update()
    {
        OffSetChanger();
    }

    private void OffSetChanger()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0f);
        
        if (meshRenderer.material.mainTextureOffset.x >= 100f) //if player do great job offset value will big. after 100 its will be turn 0
        {
            meshRenderer.material.mainTextureOffset = new Vector2(0f,0f);
        }
    }
}
