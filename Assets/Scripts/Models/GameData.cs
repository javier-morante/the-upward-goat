using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The GameData class is used to store the game's data, which can be serialized and saved.
[System.Serializable]
public class GameData
{
    public float cronometer; // Stores the elapsed time in the game
    public int jumpCount; // Stores the number of times the player has jumped
    public int coinsCollected; // Stores the number of coins the player has collected
    public Vector3 playerPosition; // Stores the player's position in the game world
    public Vector3 cameraPosition; // Stores the camera's position in the game world

    // A dictionary to keep track of coins and their collection status
    // The key is a string (e.g., coin ID) and the value is a boolean indicating if the coin has been collected
    public SerializableDictionary<string, bool> coins;

    // Constructor to initialize the game data with default values
    public GameData()
    {
        jumpCount = 0; // Initialize jump count to 0
        cronometer = 0f; // Initialize cronometer to 0
        coinsCollected = 0; // Initialize coins collected to 0
        playerPosition = Vector3.zero; // Initialize player position to (0, 0, 0)
        cameraPosition = new Vector3(0, 0, -10); // Initialize camera position to (0, 0, -10)
        coins = new SerializableDictionary<string, bool>(); // Initialize the coins dictionary
    }  
}

