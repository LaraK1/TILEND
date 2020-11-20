using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Items")]
    public int wood;
    public int stone;

    public int build = 0;
    public int buildWoodNeed = 20;

    [Header("UI Elements")]
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    private Slider buildSlider;
    public ChangeBuild changeBuild;

    private float normalFloat;

    void Start()
    {
        // start values for inventory
        build = 0;
        wood = 15;
        stone = 10;

        // update UI
        woodText.text = wood.ToString();
        stoneText.text = stone.ToString();

        // search build tile and components of it
        buildSlider = GameObject.Find("BuildSlider").GetComponent<Slider>();
        changeBuild = GameObject.Find("Build(Clone)").GetComponent<ChangeBuild>();

        // check if everything was found
        if (!buildSlider || !changeBuild)
        {
            Debug.LogError("There is no place to build the raft.");
        }
        buildSlider.maxValue = buildWoodNeed;

        // for the range of different states of the raft
        normalFloat = (1f/buildWoodNeed) * (changeBuild.builds.Length-1f);
    }

    /// <summary>
    /// Adds an amount of wood to the inventory and updates UI.
    /// </summary>
    /// <param name="add">Amount of wood to add or substract.</param>
    public void ChangeWood(int add)
    {
        wood += add;
        woodText.text = wood.ToString();
    }

    /// <summary>
    /// Adds an amount of stone to the inventory and updates UI.
    /// </summary>
    /// <param name="add">Amount of stone to add or substract.</param>
    public void ChangeStone(int add)
    {
        stone += add;
        stoneText.text = stone.ToString();
    }

    /// <summary>
    /// Adds an amount to the build progress, updates UI slider and raft sprite.
    /// </summary>
    /// <param name="add">Amount of progress to add to the build.</param>
    public void ChangeBuild(int add)
    {
        build += add;
        buildSlider.value = build;

        int currentStateBuild = Mathf.RoundToInt(build * normalFloat);
        changeBuild.ChangeBuildSprite(currentStateBuild);
    }

    /// <returns>Returns an the amount of wood in inventory.</returns>
    public int GetWood()
    {
        return wood;
    }

    /// <returns>Returns an the amount of stone in inventory.</returns>
    public int GetStone()
    {
        return stone;
    }

    /// <returns>Returns an int value that represents the progress of raft building.</returns>
    public int GetBuild()
    {
        return build;
    }
}
