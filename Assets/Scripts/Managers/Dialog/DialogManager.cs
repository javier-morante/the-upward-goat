using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    // TextMeshPro component for dialogue text (for 3D objects)
    [SerializeField] private TextMeshPro dialogText;
    // TextMeshProUGUI component for dialogue text (for UI objects)
    [SerializeField] private TextMeshProUGUI dialogTextGUI;
    // Number of letters displayed per second
    [SerializeField] private float letterPerSecond;
    // Time delay between sentences
    [SerializeField] private float TimeBetweenSentencesInSeconds;
    // UnityEvent triggered when the dialogue ends
    [SerializeField] private UnityEvent dialogEnd;

    // Queue to store dialogue sentences
    private Queue<string> sentences;
    
    void Awake()
    {
        sentences = new Queue<string>();
    }

    // Start the dialogue with an array of sentences
    public void StartDialog(string[] strArray){

        // Clear the queue of sentences
        sentences.Clear();

        // Enqueue each sentence from the array
        foreach (string sentence in strArray)
        {
            sentences.Enqueue(sentence);
        }

        // Display the first sentence
        DisplayNextSentence();
    }

    // Display the next sentence in the dialogue
    private void DisplayNextSentence()
    {
        // If there are no more sentences, end the dialogue
        if(sentences.Count == 0){
            EndDialog();
            return;
        }

        // Get the next sentence from the queue
        string sentence = sentences.Dequeue();
        // Stop any existing coroutines (typing animation)
        StopAllCoroutines();
        // Start typing the sentence
        StartCoroutine(TypeSentence(sentence));
    }

    // Coroutine for typing out the sentence letter by letter
    IEnumerator TypeSentence(string sentence){
        // Clear the dialogue text components
        if(dialogText != null) dialogText.text = "";
        if(dialogTextGUI != null) dialogTextGUI.text = "";
        // Iterate through each letter in the sentence
        foreach (char letter in sentence.ToCharArray())
        {
            // Append the letter to the dialogue text components
            if(dialogText != null) dialogText.text += letter;
            if(dialogTextGUI != null) dialogTextGUI.text += letter;
            // Wait for a short duration before displaying the next letter
            yield return new WaitForSeconds(letterPerSecond);
        }
        // Wait for a longer duration before displaying the next sentence
        yield return new WaitForSeconds(TimeBetweenSentencesInSeconds);
        // Display the next sentence
        DisplayNextSentence();
    }

    // Method to handle the end of the dialogue
    private void EndDialog()
    {
        // Invoke the dialogEnd event
        dialogEnd?.Invoke();
    }
}
