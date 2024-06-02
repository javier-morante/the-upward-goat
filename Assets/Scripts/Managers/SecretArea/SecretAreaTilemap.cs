using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class SecretAreaTilemap : MonoBehaviour
{
    // Duration of the fade effect
    [SerializeField] private float fadeDuration = 1f;
    // Reference to the Tilemap component
    private Tilemap tilemap;
    // Color of the hidden tiles
    private Color hiddenColor;
    // Reference to the current coroutine
    private Coroutine currentCoroutine;

    void Start()
    {
        // Get the Tilemap component attached to this GameObject
        tilemap = GetComponent<Tilemap>();
        // Store the original color of the tilemap
        hiddenColor = tilemap.color;
    }

    // Triggered when another collider enters the trigger zone
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Check if the entering collider belongs to the player
        if (collider2D.gameObject.CompareTag("Player"))
        {
            // Stop the current coroutine if it's already running
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            // Start the fade coroutine
            currentCoroutine = StartCoroutine(FadeTilemap(true));
        }
    }

    // Triggered when another collider exits the trigger zone
    void OnTriggerExit2D(Collider2D collider2D)
    {
        // Check if the exiting collider belongs to the player
        if (collider2D.gameObject.CompareTag("Player"))
        {
            // Stop the current coroutine if it's already running
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            // Start the fade coroutine
            currentCoroutine = StartCoroutine(FadeTilemap(false));
        }
    }

    // Coroutine to fade the tilemap in or out
    private IEnumerator FadeTilemap(bool fadeOut)
    {
        // Get the starting color based on the current color of the tilemap
        Color startColor = tilemap.color;
        // Define the target color based on whether it's fading in or out
        Color targetColor = fadeOut ? new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 0f) : hiddenColor;
        // Initialize the time elapsed during the fading process
        float timeFading = 0f;

        // Loop until the fading duration is reached
        while (timeFading < fadeDuration)
        {
            // Calculate the current color based on the interpolation between start and target colors
            tilemap.color = Color.Lerp(startColor, targetColor, timeFading / fadeDuration);
            // Increment the time elapsed
            timeFading += Time.deltaTime;
            // Wait for the next frame
            yield return null;
        }

        // Ensure that the tilemap color is set to the target color after the fading is complete
        tilemap.color = targetColor;
    }
}

