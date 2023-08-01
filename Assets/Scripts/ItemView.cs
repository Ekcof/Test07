using System;
using UnityEngine;

[Serializable]
public class ItemView
{
    [SerializeField] private string _id;
    [SerializeField] private Sprite _sprite;
    //[SerializeField] private SkeletonData _skeletonData;

    public string Id => _id;
    public Sprite Sprite => _sprite;
    //public SkeletonData SkeletonData => _skeletonData;

}
