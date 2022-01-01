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
        var mat = meshRenderer.material;
        var textureOffset = mat.mainTextureOffset;
        textureOffset.x += animationSpeed * Time.deltaTime;
        textureOffset.x = Mathf.Repeat(textureOffset.x, 1f);
        
        //if (meshRenderer.material.mainTextureOffset.x >= 100f) //if player do great job offset value will big. after 100 its will be turn 0
        //{
        //    meshRenderer.material.mainTextureOffset = new Vector2(0f,0f);
        //}

        mat.mainTextureOffset = textureOffset;
    }
}
