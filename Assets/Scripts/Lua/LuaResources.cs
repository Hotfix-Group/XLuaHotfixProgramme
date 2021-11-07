using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 各类资源的引用,不同的资源用不同的列表储存
/// </summary>
public class LuaResources : MonoBehaviour
{
    public AudioClip[] AudioRef;
    public GameObject[] GameObjectRef;
    public UnityEngine.Video.VideoClip[] VideoClipRef;
    public UnityEngine.Sprite[] SpriteRef;
    public UnityEngine.Texture2D[] Texture2DRef;
    public UnityEngine.Skybox[] SkyBoxRef;
    public UnityEngine.Material[] MaterialRef;
}
