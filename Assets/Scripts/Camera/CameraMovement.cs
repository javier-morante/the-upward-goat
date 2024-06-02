using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The CameraMovement class handles the movement of the camera in relation to the player and implements the IDataPersistence interface for saving/loading camera position data.
public class CameraMovement : MonoBehaviour, IDataPersistence<GameData>
{
    private Camera mainCamera; // Reference to the main camera component
    private Transform mCamera; // Reference to the transform component of the camera
    [SerializeField] private Transform player; // Reference to the player's transform, set in the inspector

    // Loads the camera position from the saved game data
    public void LoadData(GameData gameData)
    {
        this.transform.position = gameData.cameraPosition;
    }

    // Saves the current camera position to the game data
    public void SaveData(ref GameData gameData)
    {
        gameData.cameraPosition = this.transform.position;
    }

    // Initialize the camera references when the script starts
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        mCamera = GetComponent<Transform>();
    }

    // Called once per frame to update the camera's position
    void Update()
    {
        // Get the coordinates of the player on the screen
        Vector2 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);
        
        // Calculate the height and width of the camera's view in world units
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        // Get the camera's bounds in screen coordinates
        Rect cameraBounds = mainCamera.pixelRect;

        // Check if the player is outside the camera's bounds
        if (playerScreenPos.x < cameraBounds.xMin || playerScreenPos.x > cameraBounds.xMax ||
            playerScreenPos.y < cameraBounds.yMin || playerScreenPos.y > cameraBounds.yMax)
        {
            Vector3 newCameraPos = mCamera.position;

            // Adjust the camera position if the player is to the left or right of the camera's view
            if (playerScreenPos.x < cameraBounds.xMin)
            {
                // Player is to the left
                newCameraPos.x -= width;
            }
            else if (playerScreenPos.x > cameraBounds.xMax)
            {
                // Player is to the right
                newCameraPos.x += width;
            }

            // Adjust the camera position if the player is above or below the camera's view
            if (playerScreenPos.y < cameraBounds.yMin)
            {
                // Player is below
                newCameraPos.y -= height;
            }
            else if (playerScreenPos.y > cameraBounds.yMax)
            {
                // Player is above
                newCameraPos.y += height;
            }

            // Update the camera's position
            mCamera.position = newCameraPos;
        }
    }
}

