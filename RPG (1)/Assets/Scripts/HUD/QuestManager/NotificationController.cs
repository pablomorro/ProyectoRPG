using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationController : MonoBehaviour
{
    public TextMeshProUGUI questNameText, questStatusText;
    public float secondsToHide;

    public Animator animator;


    public void ShowQuestStatus(string questName, int status)
    {
        questNameText.text = questName;

        switch (status)
        {
            case 0:
                //added status
                questStatusText.text = "Quest Added";
                break;
            case 1:
                //completed status
                questStatusText.text = "Quest Completed";
                break;
        }

        StartCoroutine("HideAferSeconds");
    }

    IEnumerator HideAferSeconds() {
        yield return new WaitForSeconds(secondsToHide);
        animator.SetTrigger("Exit");
        
    }

    public void SetInactive() {
        this.gameObject.SetActive(false);
    }
}
