using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBuild : MonoBehaviour
{
    public SpriteRenderer buildSprite;
    public Sprite[] builds;

    /// <summary>
    /// Changes sprite of the raft on build tile.
    /// </summary>
    /// <param name="i">Int value that represents the building progress.</param>
    /// A rounded value between 0 and the number of available images.
    public void ChangeBuildSprite(int i)
    {
        buildSprite.sprite = builds[i];
    }
}
