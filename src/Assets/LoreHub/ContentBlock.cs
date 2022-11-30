using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class ContentBlock : MonoBehaviour
{
  public string Id;
  public List<Content> Contents = new List<Content>();

  public static ContentBlock Create(JObject block)
  {
    GameObject goOption = new GameObject();
    ContentBlock contentBlock = goOption.AddComponent<ContentBlock>();

    contentBlock.Id = (string)block["id"];
    goOption.name = contentBlock.GetName();

    JArray contents = (JArray)block["content"];
    for (int i = 0; i < contents.Count; i++)
    {
      JObject item = (JObject)contents[i];
      GameObject goContent = new GameObject();
      Content content = goContent.AddComponent<Content>();
      content.Text = (string)item["text"];
      content.DocumentId = (string)item["documentId"];
      content.Index = i;
      contentBlock.Contents.Add(content);
      goContent.transform.SetParent(goOption.transform);
      goContent.name = content.GetName();
    }

    return contentBlock;
  }

  public string GetName()
  {
    string[] split = this.Id.Split("/");
    return "content-block/" + split[split.Length - 1];
  }

  public string GetText()
  {
    string result = "";
    foreach (Content item in this.Contents)
    {
      result += item.Text;
      result += "<br>";
    }
    return result;
  }
}
