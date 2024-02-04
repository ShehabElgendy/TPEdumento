using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    private GameObject Dialogue;

    [SerializeField]
    private Image characterIcon;

    [SerializeField]
    private TextMeshProUGUI characterName;

    [SerializeField]
    private TextMeshProUGUI dialogueArea;

    [SerializeField]
    private Queue<DialogueLine> lines;

    [SerializeField]
    private float typingSpeed = 0.5f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string[] DialougePhraseKey;

    private bool isDialogueActive;

    private int currentIndex;

    private int maxIndex;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        lines = new Queue<DialogueLine>();
        maxIndex = DialougePhraseKey.Length;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!isDialogueActive)
        {
            Dialogue.SetActive(true);
            isDialogueActive = true;

            animator.SetBool("Active", true);

            lines.Clear();

            foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
            {
                lines.Enqueue(dialogueLine);
            }
        }


        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        if (currentIndex < maxIndex)
        {
            currentLine.line = Translator.Instance[DialougePhraseKey[currentIndex]];
            currentIndex++;
        }

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        animator.SetBool("Active", false);
    }
}