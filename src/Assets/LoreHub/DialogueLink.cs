using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class DialogueLink : MonoBehaviour
{
  public string Id;
  public string FromId;
  public string ToId;

  public static DialogueLink Create(JObject link, GameObject container)
  {
    GameObject goLink = new GameObject();

    goLink.transform.SetParent(container.transform);
    goLink.AddComponent<DialogueLink>();

    DialogueLink dialogueLinkScript = goLink.GetComponent(typeof(DialogueLink)) as DialogueLink;
    dialogueLinkScript.Id = (string)link["id"];
    dialogueLinkScript.FromId = (string)link["from"];
    dialogueLinkScript.ToId = (string)link["to"];

    goLink.name = dialogueLinkScript.GetName();

    return dialogueLinkScript;
  }

  public string GetName()
  {
    string[] split = this.Id.Split("/");
    return "link/" + split[split.Length - 1];
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
