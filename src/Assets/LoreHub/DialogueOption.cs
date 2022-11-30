using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class DialogueOption : MonoBehaviour
{
  public string Id;
  public string Text;
  public GameObject GameObject;

  public DialogueLink LinksFromMe = null;

  public static DialogueOption Create(JObject option)
  {
    GameObject goOption = new GameObject();

    goOption.AddComponent<DialogueOption>();

    DialogueOption dialogueOptionScript = goOption.GetComponent(typeof(DialogueOption)) as DialogueOption;
    dialogueOptionScript.Id = (string)option["id"];
    dialogueOptionScript.Text = (string)option["text"];

    goOption.name = dialogueOptionScript.GetName();
    dialogueOptionScript.GameObject = goOption;

    return dialogueOptionScript;
  }

  public string GetName()
  {
    string[] split = this.Id.Split("/");
    return "option/" + split[split.Length - 1];
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
