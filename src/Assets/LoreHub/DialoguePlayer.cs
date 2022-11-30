using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePlayer : MonoBehaviour
{
  public Dialogue Dialogue;
  public DialogueNode CurrentNode;

  public TMP_Text DialogueText;
  public GameObject OptionButtonPrefab;
  public GameObject PlaceForOptionButtons;

  public List<GameObject> buttons = new List<GameObject>();
  public void Start()
  {
    // Find Start Node;
    DialogueLink linkFromDialogue = this.Dialogue.links.Find(l => l.FromId == this.Dialogue.Id);
    this.CurrentNode = this.Dialogue.nodes.Find(n => n.Id == linkFromDialogue.ToId);
    RenderNode(this.CurrentNode);
  }

  void Update()
  {
    // click anywhere to show next node if no options
    if (Input.GetMouseButtonDown(0) && this.CurrentNode.Options.Count == 0)
    {
      DialogueNode nextNode = this.Dialogue.nodes.Find(n => n.Id == this.CurrentNode.LinksFromMe.ToId);
      if (nextNode != null)
      {
        this.SetNextNode(nextNode);
      }
    }
  }

  public void SetNextNode(DialogueNode node)
  {
    this.CurrentNode = node;
    RenderNode(this.CurrentNode);
  }

  private void RenderNode(DialogueNode node)
  {
    // clean

    foreach (GameObject go in this.buttons)
    {
      DestroyImmediate(go);
    }
    this.buttons = new List<GameObject>();

    // create
    this.DialogueText.text = node.ContentBlock.GetText();
    foreach (DialogueOption option in node.Options)
    {
      GameObject goOption = Instantiate(this.OptionButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
      RectTransform rectTransform = goOption.GetComponent<RectTransform>();
      goOption.transform.SetParent(this.PlaceForOptionButtons.transform);
      Transform buttonChild = goOption.transform.GetChild(0);
      Transform buttonTextGo = buttonChild.transform.GetChild(0);
      TMP_Text buttonText = buttonTextGo.gameObject.GetComponent<TMP_Text>();
      buttonText.text = option.Text;

      // Add my script to handle click
      MyButton myButton = goOption.AddComponent<MyButton>();
      myButton.Option = option;
      myButton.Button = buttonChild.gameObject;
      myButton.DialoguePlayer = this;

      buttons.Add(goOption);
    }
  }
}
