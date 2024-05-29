using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro dialogText;
    [SerializeField] private TextMeshProUGUI dialogTextGUI;
    [SerializeField] private float letterPerSecond;
    [SerializeField] private float TimeBetweenSentencesInSeconds;
    [SerializeField] private UnityEvent dialogEnd;

    private Queue<string> sentences;
    
    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(string[] strArray){

        sentences.Clear();

        foreach (string sentence in strArray)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if(sentences.Count == 0){
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence){
        if(dialogText != null) dialogText.text = "";
        if(dialogTextGUI != null) dialogTextGUI.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if(dialogText != null) dialogText.text += letter;
             if(dialogTextGUI != null) dialogTextGUI.text += letter;
            yield return new WaitForSeconds(letterPerSecond);
        }
        yield return new WaitForSeconds(TimeBetweenSentencesInSeconds);
        DisplayNextSentence();
    }

    private void EndDialog()
    {
        dialogEnd?.Invoke();
    }
}
