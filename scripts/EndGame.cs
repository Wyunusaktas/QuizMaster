using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Linq;
public class EndGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private void Start()
    {
       
       

        string xmlFilePath = "veri.xml";

        // XML belgesini yükle
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFilePath);

        // "IntValue" öðesini seç
        XmlNode intNode = xmlDoc.SelectSingleNode("/Root/IntValue");
       int sayi = int.Parse(intNode.InnerText);
        scoreText.text = "YOUR SCORE:" + sayi.ToString();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}