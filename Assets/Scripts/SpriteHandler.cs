using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public static class SpriteHandler
{
    private static SpriteAtlas spriteAtlas;

    public static void Initialize(SpriteAtlas atlas)
    {
        spriteAtlas = atlas;
    }

    public static Sprite GetSpriteByName(string spriteName)
    {
        if (spriteAtlas == null)
        {
            Debug.LogError("SpriteAtlas has not been initialized. Call Initialize() first.");
            return null;
        }

        return spriteAtlas.GetSprite(spriteName);
    }
}
