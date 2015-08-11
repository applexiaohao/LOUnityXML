using UnityEngine;
using System.Collections;
using System.Xml;

public class LOXMLScript : MonoBehaviour {

	/// <summary>
	/// the xml url
	/// </summary>
	public string xml_url = "http://192.168.203.114/test.xml";

	private string xml_content = null;
	/// <summary>
	/// Downloads the XML
	/// </summary>
	/// <returns>The XML</returns>
	IEnumerator DownloadXML()
	{
		WWW www = new WWW(xml_url);

		yield return www;

		xml_content = www.text;
	}
	
	// Use this for initialization
	void Start () 
	{
		//start coroutine method
		StartCoroutine("DownloadXML");
	}


	#region xml property

	private XmlDocument xml_doc = null;
	private XmlDocument Doc{
		set{
			xml_doc = value;
		}
		get{
			return xml_doc;
		}
	}

	#endregion

	#region load xml

	private void LoadXML()
	{
		//if xml_content has not been download suceessed...
		if (xml_content == null) {
			return;
		}


		//else...
		//if this.Doc is not null...
		if (this.Doc != null) {
			return;
		}

		//else...

		//1.create xml document object
		this.Doc = new XmlDocument();

		//2.put xml string into the object
		this.Doc.LoadXml(xml_content);

		//3.start get value...
		XmlNode node = this.Doc.FirstChild;

		Debug.Log(node.Name + " : " + node.InnerText);

		//4.create other gender key
		XmlNode gender = this.Doc.CreateNode(XmlNodeType.Element,"gender",null);
		gender.InnerText = "male";

		//5.give a node to parent
		node.AppendChild(gender);

		//6.save...
		this.Doc.Save(Application.persistentDataPath + "/test.xml");

		Debug.Log(Application.persistentDataPath);
	}

	#endregion

	// Update is called once per frame
	void Update () 
	{
		//we are not sure what's the downloaded time..
		this.LoadXML();
	}





}
