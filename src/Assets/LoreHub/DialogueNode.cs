using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class DialogueNode : MonoBehaviour
{
  public string Id;
  public GameObject GameObject;
  public DialogueLink LinksFromMe = null;
  public List<DialogueOption> Options = new List<DialogueOption>();

  public List<string> OptionIds = new List<string>();
  public string ContentBlockId;
  public ContentBlock ContentBlock;
  public static DialogueNode Create(JObject node, GameObject container)
  {
    GameObject goNode = new GameObject();

    goNode.transform.SetParent(container.transform);
    goNode.AddComponent<DialogueNode>();

    DialogueNode dialogueNodeScript = goNode.GetComponent(typeof(DialogueNode)) as DialogueNode;
    dialogueNodeScript.Id = (string)node["id"];
    dialogueNodeScript.ContentBlockId = (string)node["contentBlockId"];

    // OptionsIds
    JArray optionsIds = (JArray)node["optionsIds"];
    foreach (string id in optionsIds)
    {
      dialogueNodeScript.OptionIds.Add(id);
    }

    goNode.name = dialogueNodeScript.GetName();
    dialogueNodeScript.GameObject = goNode;
    return dialogueNodeScript;
  }

  public string GetName()
  {
    string[] split = this.Id.Split("/");
    return "node/" + split[split.Length - 1];
  }

  public void AddOption(DialogueOption option)
  {
    option.GameObject.transform.SetParent(this.GameObject.transform);
    this.Options.Add(option);
  }

  public void AddContent(ContentBlock content)
  {
    content.gameObject.transform.SetParent(this.GameObject.transform);
    this.ContentBlock = content;
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

}
