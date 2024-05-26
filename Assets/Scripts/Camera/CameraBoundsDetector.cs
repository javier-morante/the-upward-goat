using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsDetector : MonoBehaviour
{
    private Camera mainCamera;
    private Transform mCamera;
    [SerializeField] private Transform player; 

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        mCamera = GetComponent<Transform>();
    }

    void Update()
    {
        

        // Obtener las coordenadas de pantalla del jugador
        Vector2 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        Rect cameraBounds = mainCamera.pixelRect;


         if (playerScreenPos.x < cameraBounds.xMin || playerScreenPos.x > cameraBounds.xMax ||
            playerScreenPos.y < cameraBounds.yMin || playerScreenPos.y > cameraBounds.yMax)
        {
            // Calcular la posición de la cámara centrada en el jugador
            Vector3 newCameraPos = mCamera.position;

            // Verificar si el jugador está a la izquierda o derecha de la cámara
            if (playerScreenPos.x < cameraBounds.xMin)
            {
                // Jugador a la izquierda de la cámara
                newCameraPos.x -= width;
            }
            else if (playerScreenPos.x > cameraBounds.xMax)
            {
                // Jugador a la derecha de la cámara
                newCameraPos.x += width;
            }

            // Verificar si el jugador está arriba o abajo de la cámara
            if (playerScreenPos.y < cameraBounds.yMin)
            {
                // Jugador abajo de la cámara
                newCameraPos.y -= height;
            }
            else if (playerScreenPos.y > cameraBounds.yMax)
            {
                // Jugador arriba de la cámara
                newCameraPos.y += height;
            }

            // Mover la cámara a la nueva posición
            mCamera.position = newCameraPos;
        }
    }
}
