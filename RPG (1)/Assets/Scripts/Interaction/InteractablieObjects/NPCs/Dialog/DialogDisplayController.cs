using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(NPC))]
public class DialogDisplayController : MonoBehaviour
{

  

    public TextMeshProUGUI textDisplay, nameDisplay;
    public float typingDelay;
    private float originalTypingDelay;
    public GameObject continueButton;
    public Animator animator;

    public string[] sentences;

    private int index;
    private Dialog d;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = null;
        index = 0;
        originalTypingDelay = typingDelay;
    }

    IEnumerator Type(string text) {

        foreach (var letter in text.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingDelay);

        }

        index++;
    }

    private void Update()
    {
        if (sentences != null && index <= sentences.Length - 1 && index != sentences.Length - 1 && textDisplay.text == sentences[index]) {
            continueButton.SetActive(true);
        }

    }

    public void Continue()
    {
        typingDelay = originalTypingDelay;
        continueButton.SetActive(false);
        animator.SetTrigger("change");

        if (index < sentences.Length - 1)
        {
            //index++;
            textDisplay.text = "";
            StartCoroutine("Type", sentences[index]);
        }
        else{
            //hemos llegado al final del dialogo
            textDisplay.text = "";
            StartCoroutine("Type", sentences[index]);
            continueButton.SetActive(false);

            //añadir quest a la lista de misiones activas 
            if (d.questId != -1) {
                QuestManager.sharedInstance.SetQuestAsActive(d.questId);
            }
        }
    }

    public void DisplayText(Dialog d)
    {
        this.d = d;
        this.sentences = d.sentences;

        nameDisplay.text = d.npcName;
        //muestra la primera frase
        StartCoroutine("Type", sentences[0]);
    }

    public void ResetDisplayPanel()
    {
        textDisplay.text = "";
        nameDisplay.text = "";
        sentences = null;
        index = 0;
        StopCoroutine("Type");
        continueButton.SetActive(false);
    }

    public void OnPointerDown() {
        typingDelay = 0f;
    }
}
