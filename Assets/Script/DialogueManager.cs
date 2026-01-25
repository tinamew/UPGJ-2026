using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkFile;
    public GameObject textBox;
    public CharacterData characterData;
    public Image portraitImage;

    private Story story;
    TMP_Text nametag;
    TMP_Text message;
    List<string> tags;
   

    private void Start()
    {
        story = new Story(inkFile.text);
        nametag = textBox.transform.Find("Name").GetComponent<TMP_Text>();
        message = textBox.transform.Find("Dialogue").GetComponent<TMP_Text>();
        tags = new List<string>();
     
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Is there more to the story?
            if (story.canContinue)
            {
                //nametag.text = "Zigga";
                AdvanceDialogue();
            }
            else
            {
                FinishDialogue();
            }
        }
    }

    // Finished the Story (Dialogue)
    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");
    }

    // Advance through the story 
    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }
        yield return null;
    }

    /*** Tag Parser ***/
    /// In Inky, you can use tags which can be used to cue stuff in a game.
    /// This is just one way of doing it. Not the only method on how to trigger events. 
    void ParseTags()
    {
        tags = story.currentTags;
        Debug.Log("goon");
        Debug.Log(tags.ToString());
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(':');
            Debug.Log(splitTag.Length);

            if (splitTag.Length != 2)
                continue;

            string key = splitTag[0].Trim();
            Debug.Log(key);
            string param = splitTag[1].Trim();

            switch (key)
            {
                case "speaker":
                    nametag.text = param;
                    break;
                case "sprite":
                    SetPortrait(param);
                    break;
            }
        }
    }

    void SetPortrait(string portraitId)
    {
        //error exception
        if (!portraitId.Contains("_"))
        {
            Debug.LogWarning("Portrait ID missing underscore: " + portraitId);
            return;
        }

        //make sure when labelling sprites, use Firstname_Expression
        //Elena_Pissed
        var charName = portraitId.Split('_')[0];

        //finds the reference of the character
        var character = characterData.characters
            .Find(c => c.characterName == charName);

        Debug.Log(character);

        if (character == null) return;

        var portrait = character.portraits
            .Find(p => p.id == portraitId);

        if (portrait == null) return;

        portraitImage.sprite = portrait.sprite;
    }


}
