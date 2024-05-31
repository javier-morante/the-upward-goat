using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour,IDataPersistence<GameData>
{
    private Camera mainCamera;
    private Transform mCamera;
    [SerializeField] private Transform player;

    public void LoadData(GameData gameData)
    {
        this.transform.position = gameData.cameraPosition;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.cameraPosition = this.transform.position;
    }

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        mCamera = GetComponent<Transform>();
    }

    void Update()
    {
        // Get cordinates of player in the screen
        Vector2 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        Rect cameraBounds = mainCamera.pixelRect;


         if (playerScreenPos.x < cameraBounds.xMin || playerScreenPos.x > cameraBounds.xMax ||
            playerScreenPos.y < cameraBounds.yMin || playerScreenPos.y > cameraBounds.yMax)
        {
            Vector3 newCameraPos = mCamera.position;

            // Check if player is an right or left of the camera
            if (playerScreenPos.x < cameraBounds.xMin)
            {
                // Player is on right
                newCameraPos.x -= width;
            }
            else if (playerScreenPos.x > cameraBounds.xMax)
            {
                // Player is on left
                newCameraPos.x += width;
            }

            // Check if player is up or down of the camera
            if (playerScreenPos.y < cameraBounds.yMin)
            {
                // Player is down
                newCameraPos.y -= height;
            }
            else if (playerScreenPos.y > cameraBounds.yMax)
            {
                // Player is up
                newCameraPos.y += height;
            }

            mCamera.position = newCameraPos;
        }
    }
}
