using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class Document
{
  public string Id;
  public string Name;

  public static Document Create(JObject item)
  {
    return new Document()
    {
      Id = (string)item["id"],
      Name = (string)item["name"]
    };
  }
}
