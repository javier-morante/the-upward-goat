using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsDetector : MonoBehaviour
{
    private Camera mainCamera;
    private Transform mCamera;
    [SerializeField] private Transform player; 
    [SerializeField] private BoxCollider2D playerC; 

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        mCamera = GetComponent<Transform>();
    }

    void Update()
    {
        Debug.Log(playerC.size.y / 2f);

        // Obtener las coordenadas de pantalla del jugador
        Vector2 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);
        Vector2 cameraCenterScreenPos = new Vector2(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2);
        Vector2 offset = playerScreenPos - cameraCenterScreenPos;
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        float a = playerC.size.y / 2f;
        float b = playerC.size.x / 2f;

        Rect cameraBounds = mainCamera.pixelRect;


        if (playerScreenPos.x-b < cameraBounds.xMin || playerScreenPos.x+b > cameraBounds.xMax ||
            playerScreenPos.y-a < cameraBounds.yMin || playerScreenPos.y+a > cameraBounds.yMax)
        {
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
            {
                if (offset.x > 0)
                {
                    mCamera.position = new Vector3(mCamera.position.x + width, mCamera.position.y,mCamera.position.z);
                }
                else
                {
                    mCamera.position = new Vector3(mCamera.position.x - width, mCamera.position.y, mCamera.position.z);
                }
            }
            else
            {
                if (offset.y > 0)
                {
                    mCamera.position = new Vector3(mCamera.position.x, mCamera.position.y + height, mCamera.position.z);
                }
                else
                {
                    mCamera.position = new Vector3(mCamera.position.x, mCamera.position.y - height, mCamera.position.z);
                }
            }
        }
    }
}
