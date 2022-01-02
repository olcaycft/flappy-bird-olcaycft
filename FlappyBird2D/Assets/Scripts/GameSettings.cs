using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    public float pipeSpeed = 2.5f;
    public float gravity = -9.8f;
    public float strForUp = 5.5f;
    public float strForDown = 1.5f;
}
