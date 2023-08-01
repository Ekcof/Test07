using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.U2D;

public class SceneResourcesLoader : MonoBehaviour
{
    [SerializeField] private SpriteAtlas spriteAtlas; // Assign the SpriteAtlas reference in the Inspector
    private void Start()
    {
        SpriteHandler.Initialize(spriteAtlas);
    }
}
