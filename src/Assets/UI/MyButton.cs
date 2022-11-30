using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MyButton : MonoBehaviour
{
  public GameObject Button;
  public DialogueOption Option;
  public DialoguePlayer DialoguePlayer;

  void Start()
  {
    Button btn = Button.GetComponent<Button>();
    btn.onClick.AddListener(ClickOnOption);
  }

  void ClickOnOption()
  {
    DialogueNode nextNode = this.DialoguePlayer.Dialogue.nodes.Find(n => n.Id == Option.LinksFromMe.ToId);
    this.DialoguePlayer.SetNextNode(nextNode);
  }
}
