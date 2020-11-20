using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public GameObject player;
    PlayerState playerState;
    bool isMoving;

    // bit shift layer mask
    // int layerMask = 1 << 9;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    void Update() // touch inputs
    {
        // check for touch inputs
        if (Input.touchCount > 0)
        {
            // get the first finger touch
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // when touch ends / finger up
            if (touch.phase == TouchPhase.Ended)
            {
                // check if touched on collider
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (touchedCollider)
                {
                    // ceck if collider is walkable
                    if (touchedCollider.gameObject.layer == 9)
                    {
                        // start to go to the touched place
                        StartCoroutine(GoToPosition(touchPosition));
                    }
                }
            }
        }
        
    }

    /// <summary>
    /// Moves the player to the target position in a certain time.
    /// </summary>
    /// <param name="target">Touched target, represented by a vector with two coordinates.</param>
    IEnumerator GoToPosition(Vector2 target)
    {            
        // character nees half a second to move.
        float moveTime = 0.5f;

        // set times
        var startTime = Time.time;
        var endTime = Time.time + moveTime;

        // new move position 
        Vector3 moveToPosition = new Vector3(target.x, target.y, 0);
        isMoving = true;

        // change the state the player is in to moving
        playerState.IsMoving();

        // move and give time to move
        while (Time.time < endTime)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, moveToPosition, (Time.time - startTime) / moveTime);
            yield return new WaitForSeconds(0.05f);
        }

        isMoving = false;

        // change the state the player to the tile he is on
        playerState.UpdateState();
    }

}
