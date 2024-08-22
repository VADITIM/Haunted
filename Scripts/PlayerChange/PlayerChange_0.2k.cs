using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public int selectedPlayer = 0;

    void Start()
    {
        SelectPlayer(); // Initialize the selected player at the start
    }

    void Update()
    {
        int previousSelectedPlayer = selectedPlayer;

        // Handle player selection input
        HandleInput();

        // If the selected player has changed, update the active player
        if (previousSelectedPlayer != selectedPlayer)
        {
            SelectPlayer();
        }
    }

    void HandleInput()
    {
        // Switch to the next player
        if (Input.GetKeyDown(KeyCode.Y))
        {
            selectedPlayer = (selectedPlayer >= transform.childCount - 1) ? 0 : selectedPlayer + 1;
            
        }
        // Switch to the previous player
        else if (Input.GetKeyDown(KeyCode.X))
        {
            selectedPlayer = (selectedPlayer <= 0) ? transform.childCount - 1 : selectedPlayer - 1;
        }
    }

    void SelectPlayer()
    {
        int i = 0;
        foreach (Transform player in transform)
        {
            player.gameObject.SetActive(i == selectedPlayer);
            i++;
        }
    }
}