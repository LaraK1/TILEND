using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Needs")]

    public float food;
    public float energy;
    public float water;

    [Header("Maximum Needs")]

    public float foodMax = 100;
    public float energyMax = 100;
    public float waterMax = 100;

    [Header("Stats consumed per second")]
    public const int foodConsumed = -1;
    public const int energyConsumed = -1;
    public const int waterConsumed = -1;

    [Header("UI Elements")]
    public Slider foodSlider;
    public Slider waterSlider;
    public Slider energySlider;

    /// <summary>
    /// Sets start values.
    /// </summary>
    void Start()
    {
        // start stats
        food = 90f;
        energy = 100f;
        water = 50f;

        // updates ui elements
        foodSlider.value = food;
        energySlider.value = energy;
        waterSlider.value = water;

    }

    /// <summary>
    /// Triggers a change in food.
    /// </summary>
    /// <param name="add">Int value added to the food need; default is standard consumed food per tick</param>
    /// <returns>Returns success bool.</returns>
    public bool FoodTrigger(int add = foodConsumed)
    {
        if (food <= foodMax)
        {
            food += add;
            if (food > foodMax)
            {
                food = foodMax;
                return false;
            }

            foodSlider.value = food;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Triggers a change in energy.
    /// </summary>
    /// <param name="add">Int value added to the energy need; default is standard consumed energy per tick</param>
    public void EnergyTrigger(int add = energyConsumed)
    {
        if (energy <= energyMax)
        {
            energy += add;
            if (energy > energyMax)
            {
                energy = energyMax;
            }

            energySlider.value = energy;
        }
    }

    /// <summary>
    /// Triggers a change in hydration.
    /// </summary>
    /// <param name="add">Int value added to the water need; default is standard consumed water per tick</param>
    /// <returns>Returns success bool.</returns>
    public bool WaterTrigger(int add = waterConsumed)
    {
        if (water <= waterMax)
        {
            water += add;
            if (water > waterMax)
            {
                water = waterMax;
                return false;
            }

            waterSlider.value = water;
            return true;
        }

        return false;
    }

    /// <summary>
    /// See if one of the needs is empty.
    /// </summary>
    /// <returns>Returns a boolean if the player is dead.</returns>
    public bool IsDead()
    {
        if(energy <= 0 || food <= 0 || water <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
