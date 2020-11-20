using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public PlayerState playerState;
    PlayerInventory inventory;
    PlayerStats playerStats;

    float startTime;

    public bool isPlaying;
    public float triggerTact = 1f;

    public GameObject deadUI;

    /// <summary>
    /// Sets start time and starts the coroutine that triggers changes in stats.
    /// </summary>
    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>();

        startTime = Time.time;

        isPlaying = true;
        StartCoroutine(TriggerSecond());

    }

    /// <returns>Returns a float value of the time used since the start.</returns>
    float GetTime()
    {
        float timeUsed = Time.time - startTime;
        return timeUsed;
    }

    /// <summary>
    /// Triggers changes in the stats at certain intervals.
    /// </summary>
    IEnumerator TriggerSecond()
    {
        while (isPlaying)
        {
            // wait a second
            yield return new WaitForSeconds(triggerTact);

            // trigger changes
            TriggerChanges();
        }

    }

    // This basicly is the heart of the game - and yes it could be pretier.

    /// <summary>
    /// Changes the stats depending on the location of the player.
    /// </summary>
    void TriggerChanges()
    {
        // get the location of the player
        State currentState = playerState.GetCurrentState();

        // change inventory and needs according to tile
        if (currentState == State.Berry) // berry bushes give 3 food per tick and need 1 wood
        {
            int woodNeed = 1;
            if (inventory.GetWood() >= woodNeed)
            {
                playerStats.EnergyTrigger();
                playerStats.WaterTrigger();
                if(playerStats.FoodTrigger(3)) // successfully added food
                    inventory.ChangeWood(-woodNeed);
            }
            else
            {
                StandardLoss();
            }
        }
        else if (currentState == State.Wild) // hunting gives 5 food per tick and needs 2 wood
        {
            int woodNeed = 2;
            if (inventory.GetWood() >= woodNeed)
            {
                playerStats.EnergyTrigger();
                playerStats.WaterTrigger();
                if (playerStats.FoodTrigger(5)) // successfully added food
                    inventory.ChangeWood(-woodNeed);
            }
            else
            {
                StandardLoss();
            }
        }
        else if(currentState == State.Camp) // the camp gives 5 energy per tick and needs no resources
        {
            playerStats.EnergyTrigger(5);
            playerStats.WaterTrigger();
            playerStats.FoodTrigger();
        }
        else if (currentState == State.Water) // water sources give 5 hydration per tick and need one stone
        {
            int stoneNeed = 1;
            if (inventory.GetStone() >= stoneNeed)
            {
                playerStats.EnergyTrigger();
                playerStats.FoodTrigger();
                if(playerStats.WaterTrigger(5)) // successfully added water
                    inventory.ChangeStone(-stoneNeed); // change resources
            }
            else
            {
                StandardLoss();
            }
        }
        else if (currentState == State.Sea) // the sea gives 5 food per tick and needs 1 wood and 1 stone
        {
            int woodNeed = 1;
            int stoneNeed = 1;
            if (inventory.GetWood() >= woodNeed && inventory.GetStone() >= stoneNeed)
            {
                playerStats.EnergyTrigger();
                playerStats.WaterTrigger();
                if (playerStats.FoodTrigger(5)) // successfully added food
                {
                    inventory.ChangeWood(-woodNeed);
                    inventory.ChangeStone(-stoneNeed);
                }
            }
            else
            {
                StandardLoss();
            }
        }
        else if (currentState == State.Forest) // the forest gives one wood per tick
        {
            StandardLoss();
            inventory.ChangeWood(1);
        }
        else if (currentState == State.Mountain) // the mountains give one stone per tick
        {
            StandardLoss();
            inventory.ChangeStone(1);
        }
        else if (currentState == State.Walk) // while walking you use 5 food
        {
            playerStats.EnergyTrigger();
            playerStats.WaterTrigger();
            playerStats.FoodTrigger(-5);
        }
        else if (currentState == State.Build) // for building the raft you need wood, it takes one wood per tick
        {
            int woodNeed = 1;
            if (inventory.GetWood() >= woodNeed)
            {
                StandardLoss();
                inventory.ChangeWood(-woodNeed);
                inventory.ChangeBuild(1);
            }
            else
            {
                StandardLoss();
            }
        }
        else
        {
            StandardLoss();
        }

        // check game end
        if (playerStats.IsDead()) // checks if needs are too low
        {
            isPlaying = false;
            deadUI.SetActive(true);
        } 
        // is build ready
        else if(inventory.GetBuild() >= inventory.buildWoodNeed) // checks if raft is build and game won
        {
            isPlaying = false;
            Debug.Log("You won");
        }
    }

    /// <summary>
    /// Reduces all needs by the default value.
    /// </summary>
    void StandardLoss()
    {
        playerStats.EnergyTrigger();
        playerStats.WaterTrigger();
        playerStats.FoodTrigger();
    }
}
