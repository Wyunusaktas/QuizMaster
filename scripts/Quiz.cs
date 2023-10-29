using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Xml.Linq;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] QuestionSO CurretnQuestion;
    [SerializeField] List<QuestionSO> ques = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI score;
     int scr;

    [Header("Answers")]
    [SerializeField] GameObject[] Answers;
    int correctAsnwer;
    bool isAnsweredEarly;


    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite CorrectAnswerSprite;
    bool selected;


    [Header("Timer")]
    [SerializeField] Image image;
    Timer time;

    [SerializeField] AudioClip Correct;
    [SerializeField] AudioClip Wrong;
    [SerializeField] AudioClip bck;
    AudioSource CorrectVoice;
    AudioSource WrongVoice;
    AudioSource BckVoice;
    int counter =0;




    void Start()
    {
        CorrectVoice = GetComponent<AudioSource>();
        WrongVoice = GetComponent<AudioSource>();
        BckVoice = GetComponent<AudioSource>();
        time = FindObjectOfType<Timer>();
        BckVoice.PlayOneShot(bck);
        

    }
   
    void Update()
    {
        image.fillAmount = time.FillFraction;
        if(time.loadNextQuestion)
        {
            isAnsweredEarly = false; ;
            newQuestion();
            time.loadNextQuestion = false;
        }
        else if(!isAnsweredEarly && !time.isAnswerShowing)
        {
            DispalAnswer(-1);
            selectedbutton(false);
        }
    }
    public  void newQuestion()
    {
        counter++;
        CorrectVoice.Stop();
        WrongVoice.Stop();
        BckVoice.PlayOneShot(bck);
        if(ques.Count>0)
        {
            selectedbutton(true);
            getRandomQuestions();
            displayedquestion();
            SetDefaultButtonSprites();
            
        }
        if(counter==5)
        {
            Invoke("loadScene", 3.4f);
            
        }
    
    }
   public int getRandomQuestions()
    {
        
        int index = Random.Range(0, ques.Count);
        CurretnQuestion = ques[index];
        if(ques.Contains(CurretnQuestion))
        {
            ques.Remove(CurretnQuestion);
        }
        return index;
    }
    private void loadScene()
    {
        SceneManager.LoadScene(1);
    }


    public void CorrectAnswer(int index)
    {
        isAnsweredEarly = true;
        DispalAnswer(index);
        selectedbutton(false);
        time.CancelTimer();


    }
    void DispalAnswer(int index)
    {
        if (index == CurretnQuestion.GetAnswerIndex())
        {
            BckVoice.Stop();
            CorrectVoice.PlayOneShot(Correct);
            
            questionText.text = "CORRECT";
            Image buttonImage = Answers[index].GetComponent<Image>();
            buttonImage.sprite = CorrectAnswerSprite;
            scr=scr + 20;
            string srco = scr.ToString();
            score.text = "Score" +":"+srco;
            if(counter==5)
            {
                // XML dosyasýnýn adýný belirleyin
                string xmlFilePath = "veri.xml";

                // XML belgesini oluþturun
                XmlDocument xmlDoc = new XmlDocument();

                // Kök öðeyi oluþturun
                XmlElement root = xmlDoc.CreateElement("Root");
                xmlDoc.AppendChild(root);

                // Tamsayý deðerini ekleyin
              
                XmlElement intElement = xmlDoc.CreateElement("IntValue");
                intElement.InnerText = scr.ToString();
                root.AppendChild(intElement);

                // XML dosyasýný kaydedin
                xmlDoc.Save(xmlFilePath);

            }
         
        }
        else
        {
            BckVoice.Stop();
            WrongVoice.PlayOneShot(Wrong);
            correctAsnwer = CurretnQuestion.GetAnswerIndex();
            questionText.text = "Sorry The Correct Answer Was " + CurretnQuestion.GetAnswer(correctAsnwer);

        }


    }
    void displayedquestion()
    {
        questionText.text = CurretnQuestion.GetQuestion();
        for (int i = 0; i<Answers.Length; i++)
        {
            TextMeshProUGUI buttonText = Answers[i].GetComponentInChildren<TextMeshProUGUI>();
             buttonText.text = CurretnQuestion.GetAnswer(i);
        }

    }
   
    
     void selectedbutton(bool select)
    {
        for (int i = 0; i < Answers.Length; i++)
        {
            Button button = Answers[i].GetComponent<Button>();
            button.interactable = select;

        }

    }
   
    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < Answers.Length; i++)
        {
            Image buttonImage = Answers[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;

        }

    }
    
    public int ReturnScore()
    {
        return scr;
    }
   
}
