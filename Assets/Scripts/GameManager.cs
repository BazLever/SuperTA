using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int nextLevelToLoad;


    public GameObject player;
    public Transform playerPos;
    public GameObject deathBody;

    Text timerUI;
    Text scoreUI;
    Text bonusUI;

    //Text rankUI;

    public Animator levelEndAnim;

    GameObject endOfLevelUI;


    public int score;
    public float levelTimer;
    public int timeBonus;
    public float timeBonusDepricator;
    float timeBonusDepTime;
    string rank;

    public int enemiesToKillTotal;
    public int enemiesToKill;

    float timer;
    double timerRounded;

    int minutes;

    bool isLevelEnd;

    public bool timerIsOn;

    bool playerIsDead;

    public Image rankImage;
    public Sprite d, c, b, a, s, ss, sss;


    
    void Start()
    {
        timerIsOn = false;
        playerIsDead = false;
        timer = levelTimer;

        isLevelEnd = false;

        levelEndAnim = GameObject.Find("EndOfLevel").GetComponent<Animator>();
        rankImage = GameObject.Find("RankImage").GetComponent<Image>();

        timerUI = GameObject.Find("Timer").GetComponent<Text>();
        scoreUI = GameObject.Find("Score").GetComponent<Text>();
        bonusUI = GameObject.Find("TimeBonus").GetComponent<Text>();
        //rankUI = GameObject.Find("Rank").GetComponent<Text>();
        endOfLevelUI = GameObject.Find("EndOfLevel");

        enemiesToKillTotal = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesToKill = enemiesToKillTotal;

    }

    void Update()
    {
        if (playerIsDead == true && isLevelEnd == false)
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        } else if (playerIsDead == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                FailState();
            }
        }

        if (timerIsOn == true && playerIsDead == false)
        {
            timer += Time.deltaTime;
            timerRounded = System.Math.Round(timer, 2);
            if (timer >= 60)
            {
                timer -= timer;
                minutes += 1;
            }

            timeBonusDepTime += Time.deltaTime;
            if (timeBonusDepTime >= timeBonusDepricator)
            {
                timeBonus -= 1;
                timeBonusDepTime -= timeBonusDepTime;
            }

            

        }

        //if (timer > levelTimer)
        //{
        //    FailState();
        //}





        bonusUI.text = "Time Bonus:" + timeBonus;
        //timerUI.text = "" + minutes + ":" + timerRounded;

        if(isLevelEnd == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextLevel();
            }
        }
        


        UpdateScore(0);
    }

    public void StartTimer()
    {
        timerIsOn = true;
    }


    public void FailState()
    {
        //When the player dies/fails, send them here.
        playerIsDead = true;
        GameObject GO = Instantiate(deathBody, (new Vector3(playerPos.position.x, (playerPos.position.y + 1f), playerPos.position.z)), Quaternion.identity) as GameObject;
        Destroy(player);
    }

    public void UpdateScore(int scoreAddition)
    {
        score += scoreAddition;
        scoreUI.text = "SCORE:" + score;
    }


    public void RankCalculation()
    {
        if (enemiesToKill == 0 && timeBonus >= 700)
        {
            rank = "SUPER SHOOTER SUPERSTAR!";
            rankImage.sprite = sss;
        }
        else if (enemiesToKill == 0 && timeBonus >= 600)
        {
            rank = "SUPER SHOOTER!";
            rankImage.sprite = ss;
        }
        else if (enemiesToKill == 0 && timeBonus >= 500)
        {
            rank = "SHOOTER!";
            rankImage.sprite = s;
        }
        else if (enemiesToKill == 0 && timeBonus >= 450)
        {
            rank = "AWESOME!";
            rankImage.sprite = a;
        }
        else if (enemiesToKill == (3 * enemiesToKill / 4) && timeBonus >= 400)
        {
            rank = "BLAM!";
            rankImage.sprite = b;
        }
        else if (enemiesToKill == (2 * enemiesToKill / 4) && timeBonus >= 300)
        {
            rank = "CLASSY!";
            rankImage.sprite = c;
        } 
        else
        {
            rank = "DEMOTED!";
            rankImage.sprite = d;
        }

    }

    public void LevelEnd()
    {
        levelEndAnim.SetBool("LevelEnd", true);
        RankCalculation();
        timerIsOn = false;
        endOfLevelUI.SetActive(true);
        timerUI.text = "TIME: " + minutes + ":" + timerRounded;
        isLevelEnd = true;
        Time.timeScale = 0.3f;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextLevelToLoad);
    }


}
