using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class Content : MonoBehaviour
{
  public string Text;
  public string DocumentId;
  public int Index;

  public string GetName()
  {
    if (this.Text != null)
    {
      return this.Text.Substring(0, System.Math.Min(this.Text.Length, 30));
    }
    else
    {
      return "content#" + this.Index;
    }
  }
}
