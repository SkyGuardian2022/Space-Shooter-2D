using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _scoreText;

    //[SerializeField]
    //private Image _LivesImg;
    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private TMP_Text _gameOverText;

    [SerializeField]
    private TMP_Text _RestartText;

    private bool _gameOverString;

    private bool _isGameOver;

    private GameManager _gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
       
       

    }

    public void UpdateScore(int playerScore)
    {

        _scoreText.text = "Score: " + playerScore.ToString();

    }

    public void UpdateLives(int currentLives)
    {
        
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives < 1)
        {
            GameOverSequence();
            
            if (transform.GetChild(2) != null)
            {

                StartCoroutine(GameOverTextTypeEffect());

            }
            
            if (transform.GetChild(3) != null)
            {

                StartCoroutine(RestartGameLevel());

            }
 

        }

    void GameOverSequence()
        {
            _gameManager.GameOver();
        }

    }

    private IEnumerator RestartGameLevel()
    {

        while (true)
        {

            transform.GetChild(3).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.75f);
            transform.GetChild(3).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.75f);

        }


    }

    private IEnumerator GameOverTextTypeEffect()
    {

        while (true)
        {

            transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.55f);
            transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.55f);


        }

    }

}
