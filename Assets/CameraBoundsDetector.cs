using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsDetector : MonoBehaviour
{
    public Camera mainCamera; // Referencia a la cámara principal
    public Transform mCamera;
    public Transform player; // Referencia al transform del jugador
    public SpriteRenderer obj;

    void Update()
    {
        // Obtener las coordenadas de pantalla del jugador
        Vector2 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);
        Vector2 cameraCenterScreenPos = new Vector3(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0);
        Vector2 offset = playerScreenPos - cameraCenterScreenPos;

        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;


        // Obtener los límites de la cámara en píxeles
        Rect cameraBounds = mainCamera.pixelRect;

        // Verificar si el jugador está dentro de los límites de la cámara
        if (playerScreenPos.x < cameraBounds.xMin || playerScreenPos.x > cameraBounds.xMax ||
            playerScreenPos.y < cameraBounds.yMin || playerScreenPos.y > cameraBounds.yMax)
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
