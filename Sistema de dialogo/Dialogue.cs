using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("Reading")]
    [SerializeField]private Sprite profile;
    [SerializeField] private string actorName;
    [SerializeField] private string[] speechTxt;

    [Header("Components")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Text textInteract;

    public static bool onTyping;
    private DialogueController dc;
    private bool onRadius;

    void Start()
    {
        dc = FindObjectOfType<DialogueController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&onRadius&&!onTyping)
        {
            dc.Speech(profile, speechTxt, actorName);
            onTyping = true;
            Gun.instance.noCanShot();
            Player.instance.noCanMove();
        }
        if(Input.GetKeyDown(KeyCode.E) && onRadius && onTyping)
        {
            dc.NextSentences();
        }

        Interact();
        TextInteract();
    }

    void TextInteract()
    {
        if (onRadius&&!onTyping)
        {
            textInteract.enabled = true;
            textInteract.text = "pressione [E] para ler";
        }
        else if(onRadius&&onTyping)
        {
            textInteract.enabled = false;
            textInteract.text = "";
        }
        else
        {
            textInteract.enabled = false;
            textInteract.text = "";
        }
    }

    void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if(hit != null)
        {
            onRadius = true;
        }
        else
        {
            onRadius = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}