using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// possible states the player can be in
public enum State
{Forest,Wild,Sand,Sea,DeepSea,Mountain,Berry,Camp,Water,Build,Walk,None}

public class PlayerState : MonoBehaviour
{
    public State currentState = State.Camp;
    int walkLayer = 1 << 9;

    /// <summary>
    /// Updates the current state the player is in.
    /// </summary>
    public void UpdateState()
    {
        // create ray cast to check the tile standing on
        Vector2 position = transform.position;

        float distance = 1.0f;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, distance, walkLayer);

        // hit something
        if (hit.collider != null)
        {
            // get tag name of hit object
            string tagName = hit.transform.gameObject.tag;

            // find the fitting state for the object; objects are tagged with state name
            if (System.Enum.TryParse<State>(tagName, out currentState)){
                // do nothing
            }
            else
            {
                // no fitting state found
                currentState = State.None;
            }
        }
        else
        {
            // no object beneath player found
            currentState = State.None;
        }
    }

    /// <summary>
    /// Change the current state to walking.
    /// </summary>
    public void IsMoving()
    {
        currentState = State.Walk;
    }

    /// <returns>Returns current state the player is in.</returns>
    public State GetCurrentState()
    {
        return currentState;
    }
   
}
