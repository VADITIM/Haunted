using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public int selectedPlayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectPlayer();

    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedPlayer = selectedPlayer;

        if (Input.GetAxis("Mouse ScrollWheell") > 0f)
        {
            if (selectedPlayer >= transform.childCount - 1)
                selectedPlayer = 0;
            else
            selectedPlayer++;
        }
        if (Input.GetAxis("Mouse ScrollWheell") < 0f)
        {
            if (selectedPlayer <= 0)
                selectedPlayer = transform.childCount - 1;
            else
                selectedPlayer--;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (selectedPlayer >= transform.childCount - 1)
                selectedPlayer = 0;
            else
                selectedPlayer++;
        }
        else if (Input.GetKeyDown(KeyCode.X)) // Alternative key to switch backwards
        {
            if (selectedPlayer <= 0)
                selectedPlayer = transform.childCount - 1;
            else
                selectedPlayer--;
        }

        if (previousSelectedPlayer != selectedPlayer)
        {
            SelectPlayer();
        }
    }

    void SelectPlayer()
    {
        int i = 0;
        foreach (Transform player in transform)
        {
            if (i == selectedPlayer)
                player.gameObject.SetActive(true);
            else
                player.gameObject.SetActive(false);
            i++;
        }

    }
}
