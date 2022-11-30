using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TMPro;

public class Dialogue : MonoBehaviour
{
  public TextAsset json;

  public string Id;
  private GameObject nodesContainer;
  private GameObject linksContainer;
  private GameObject unassignedContainer;

  public List<DialogueLink> links;
  public List<DialogueNode> nodes;
  public List<DialogueOption> options;
  public List<ContentBlock> contentBlocks;
  public List<Document> documents;

  public void GenerateDialogue()
  {
    // Clean up
    DestroyImmediate(this.nodesContainer);
    DestroyImmediate(this.linksContainer);
    DestroyImmediate(this.unassignedContainer);

    // Create Unassigned Container - for items without parent
    this.unassignedContainer = new GameObject();
    this.unassignedContainer.name = "Unassigned Container";
    this.unassignedContainer.transform.SetParent(this.transform);

    // Create Nodes container
    this.nodesContainer = new GameObject();
    this.nodesContainer.name = "Nodes Container";
    this.nodesContainer.transform.SetParent(this.transform);

    // Create Links container
    this.linksContainer = new GameObject();
    this.linksContainer.name = "Links Container";
    this.linksContainer.transform.SetParent(this.transform);

    JObject exportData = JObject.Parse(json.ToString());


    // extract resources from JSON
    JArray resources = (JArray)exportData["resources"];

    this.links = new List<DialogueLink>();
    this.nodes = new List<DialogueNode>();
    this.contentBlocks = new List<ContentBlock>();
    this.options = new List<DialogueOption>();
    this.documents = new List<Document>();

    // sort resources
    foreach (JObject item in resources)
    {
      string type = (string)item["type"];
      if (type == "@lorehub/dialogue-node")
      {
        DialogueNode node = DialogueNode.Create(item, this.nodesContainer);
        this.nodes.Add(node);
      }
      if (type == "@lorehub/dialogue-link")
      {
        DialogueLink link = DialogueLink.Create(item, this.linksContainer);
        this.links.Add(link);
      }
      if (type == "@lorehub/dialogue-node-option")
      {
        DialogueOption option = DialogueOption.Create(item);
        options.Add(option);
        option.gameObject.transform.SetParent(this.unassignedContainer.transform);
      }
      if (type == "@lorehub/document")
      {
        Document document = Document.Create(item);
        documents.Add(document);
      }
      if (type == "@lorehub/dialogue")
      {
        this.Id = (string)item["id"];
      }
      if (type == "@lorehub/content-block")
      {
        ContentBlock contentBlock = ContentBlock.Create(item);
        this.contentBlocks.Add(contentBlock);
        contentBlock.gameObject.transform.SetParent(this.unassignedContainer.transform);
      }
    }

    AttachLinks();
    AssignOptionsToNodes();
    AttachContentToNodes();
    UpdateContentNodeWithReferencesToDocuments();

    // Add Dialogue Player Script
    DialoguePlayer player = this.gameObject.GetComponent<DialoguePlayer>();
    if (player == null)
    {
      player = this.gameObject.AddComponent<DialoguePlayer>();
      player.Dialogue = this;
    }
  }

  private void AttachLinks()
  {
    foreach (DialogueLink link in this.links)
    {
      DialogueNode neededNode = this.nodes.Find(n => n.Id == link.FromId);
      if (neededNode != null)
      {
        neededNode.LinksFromMe = link;
      }
      DialogueOption neededOption = this.options.Find(n => n.Id == link.FromId);
      if (neededOption != null)
      {
        neededOption.LinksFromMe = link;
      }
    }
  }

  private void AssignOptionsToNodes()
  {
    foreach (DialogueNode node in this.nodes)
    {
      foreach (string optionId in node.OptionIds)
      {
        DialogueOption neededOption = this.options.Find(o => o.Id == optionId);
        if (neededOption != null)
        {
          node.AddOption(neededOption);
        }
      }

    }
  }

  private void AttachContentToNodes()
  {
    foreach (DialogueNode node in this.nodes)
    {
      ContentBlock contentBlock = this.contentBlocks.Find(c => c.Id == node.ContentBlockId);
      if (contentBlock != null)
      {
        node.AddContent(contentBlock);
      }
    }
  }

  private void UpdateContentNodeWithReferencesToDocuments()
  {
    foreach (ContentBlock contentBlock in this.contentBlocks)
    {
      foreach (Content content in contentBlock.Contents)
      {
        if (content.Text == null & content.DocumentId != null)
        {
          Document neededDocument = this.documents.Find(d => d.Id == content.DocumentId);
          if (neededDocument != null)
          {
            content.Text = neededDocument.Name;
          }
        }
      }
    }
  }
}
