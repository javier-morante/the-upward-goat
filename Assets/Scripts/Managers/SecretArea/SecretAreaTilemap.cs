using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class SecretAreaTilemap : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f ;
    private Tilemap tilemap;
    private Color hiddenColor;
    private Coroutine currentCorunite;

    void Start(){
        tilemap = GetComponent<Tilemap>();
        hiddenColor = tilemap.color;
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        
        if(collider2D.gameObject.CompareTag("Player")){

            if (currentCorunite != null)
            {
                StopCoroutine(currentCorunite);
            }
           currentCorunite = StartCoroutine(FadeTilemap(true));
        }
    }

    void OnTriggerExit2D(Collider2D collider2D){
        
        if(collider2D.gameObject.CompareTag("Player")){
            if (currentCorunite != null)
            {
                StopCoroutine(currentCorunite);
            }
           currentCorunite = StartCoroutine(FadeTilemap(false));
        }
    }

    private IEnumerator FadeTilemap(bool fadeOut){
        Color startColor = tilemap.color;
        Color targetColor = fadeOut ? new Color(hiddenColor.r,hiddenColor.g,hiddenColor.b,0f): hiddenColor;
        float timeFading = 0f;

        while (timeFading < fadeDuration)
        {
            tilemap.color = Color.Lerp(startColor,targetColor,timeFading/fadeDuration);
            timeFading += Time.deltaTime;
            yield return null;
        }
        tilemap.color = targetColor;
    }
}
