using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Numbers : MonoBehaviour
{
    public GameObject[] NumberPre;
    public bool Ini = false;
    private GameObject numberItem;
    public GameObject EnglishPanel;
    public GameObject ChinesePanel;
    public AudioSource english;
    public AudioSource chinese;
    public AudioSource congra;
    public AudioSource tryAgain;
    private int iniNumber = 0;
    private int repeatNum = 0;
    private int lastStage = 0;
    private int gameStage = 1;
    private bool playing = false;
    private bool language = true;
    private GameObject activePanel;
    // Start is called before the first frame update
    void Start()
    {
        activePanel = EnglishPanel;
        
    }

    public void changeLanguage()
    {
        Destroy(numberItem);
        if(language)
        {
            EnglishPanel.SetActive(true);
            ChinesePanel.SetActive(false);
            activePanel = EnglishPanel;
            language = !language;
            lastStage = 0;
            gameStage = 1;
            playing = false;
}
        else
        {
            EnglishPanel.SetActive(false);
            ChinesePanel.SetActive(true);
            activePanel = ChinesePanel;
            language = !language;
            lastStage = 0;
            gameStage = 1;
            playing = false;
        }
    }
    public void changeIni()
    {
        Ini = !Ini;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(gameStage == 1&&lastStage!=1)
        {
            Transform startButton = activePanel.transform.Find("StartButton");
            startButton.gameObject.SetActive(true);
            Transform read = activePanel.transform.Find("Read");
            read.gameObject.SetActive(false);
            Transform buttons = activePanel.transform.Find("buttons");
            buttons.gameObject.SetActive(false);
            lastStage = 1;
            Debug.Log(gameStage);
        }
        else if(gameStage == 2 && lastStage !=2)
        {
            Transform startButton = activePanel.transform.Find("StartButton");
            startButton.gameObject.SetActive(false);
            Transform read = activePanel.transform.Find("Read");
            read.gameObject.SetActive(false);
            Transform buttons = activePanel.transform.Find("buttons");
            buttons.gameObject.SetActive(false);
            lastStage = 2;
            Debug.Log(gameStage);
        }
        else if (gameStage == 3 && lastStage != 3)
        {
            Transform startButton = activePanel.transform.Find("StartButton");
            startButton.gameObject.SetActive(false);
            Transform read = activePanel.transform.Find("Read");
            
            read.gameObject.SetActive(true);
            Transform buttons = activePanel.transform.Find("buttons");
            buttons.gameObject.SetActive(true);
            lastStage = 3;
            if (language)
                english.Play();
            else
                chinese.Play();
            Debug.Log(gameStage);
        }
        else if(gameStage == 4 && lastStage !=4 )
        {
            Debug.Log(iniNumber);
            Debug.Log(repeatNum);
            if(iniNumber == repeatNum)
            {
                Transform startButton = activePanel.transform.Find("StartButton");
                startButton.gameObject.SetActive(false);
                Transform read = activePanel.transform.Find("Read");
                read.gameObject.SetActive(false);
                Transform buttons = activePanel.transform.Find("buttons");
                buttons.gameObject.SetActive(false);
                Transform tips = activePanel.transform.Find("tips");
                tips.gameObject.SetActive(true);
                tips.gameObject.GetComponent<Text>().text = "CONGRATULATIONS!";
                
                if (!playing)
                {   tips.gameObject.GetComponent<Animation>().Play();
                    congra.Play();
                    playing = true;
                }
                if (!tips.gameObject.GetComponent<Animation>().isPlaying)
                {   
                    Destroy(numberItem);
                    tips.gameObject.SetActive(false);
                    Debug.Log("end");
                    gameStage = 1;
                    playing = false;
                }
            }
            else
            {
                Transform startButton = activePanel.transform.Find("StartButton");
                startButton.gameObject.SetActive(false);
                Transform read = activePanel.transform.Find("Read");
                read.gameObject.SetActive(false);
                Transform buttons = activePanel.transform.Find("buttons");
                buttons.gameObject.SetActive(false);
                Transform tips = activePanel.transform.Find("tips");
                tips.gameObject.SetActive(true);
                tips.gameObject.GetComponent<Text>().text = "Try Again!";
                if (!playing)
                {
                    tips.gameObject.GetComponent<Animation>().Play();
                    tryAgain.Play();
                    playing = true;
                }
                if (!tips.gameObject.GetComponent<Animation>().isPlaying)
                {
                    tips.gameObject.SetActive(false);
                    Debug.Log("end");
                    gameStage = 3;
                    lastStage = 4;
                    playing = false;
                }
            }
        }
        


        if(Ini)
        {
            RandIniNum();
            Ini = false;
            if(gameStage == 1)
            gameStage = 2;
        }
        
    }

    public void startGameSwitch()
    {
        if(gameStage == 2)
        gameStage = 3;
    }
    

    public void RandIniNum()
    {
        iniNumber = Random.Range(0, 10);
        Vector3 newPosi = new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y, transform.position.z + Random.Range(-3, 3));
        Vector3 newRota = new Vector3(transform.rotation.x + Random.Range(-30, 30), transform.rotation.y + Random.Range(-30, 30), transform.rotation.z + Random.Range(-30, 30));
        Quaternion dir = Quaternion.AngleAxis(Random.Range(0, 360), newRota);
        numberItem = Instantiate(NumberPre[iniNumber], newPosi, dir);
    }

    
    public void repeatNumber(int i)
    {
        repeatNum = i;
        if(gameStage == 3)
        gameStage = 4;
        Debug.Log(repeatNum);
    }
}
