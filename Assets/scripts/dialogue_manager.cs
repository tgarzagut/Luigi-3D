using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text speakerText;

    public string[] dialogueLines;
    public string[] speakers;

    private int currentLine = 0;

    void Start()
    {
        currentLine = 0;
        ShowDialogue();
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }

    void ShowDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];
        if (speakerText != null)
            speakerText.text = speakers[currentLine];
    }

    public void NextLine()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            ShowDialogue();
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
