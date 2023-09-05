using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [Header("Components")]
    public AudioSource typingSound;
    public GameObject dialogueObjeto;
    public Image profile;
    public Text speechText;
    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    [HideInInspector] public string[] sentences;
    [HideInInspector] public int index;

    public void Speech(Sprite p , string[] txt, string actorName)
    {
        dialogueObjeto.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentences());
    }

    public IEnumerator TypeSentences()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            typingSound.Play();
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentences()
    {
        if(speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentences());
            }
            else
            {
                index = 0;
                speechText.text = "";
                dialogueObjeto.SetActive(false);
                Dialogue.onTyping = false;
                Gun.instance.canShot();
                Player.instance.canMove();
            }
        }
    }
}
