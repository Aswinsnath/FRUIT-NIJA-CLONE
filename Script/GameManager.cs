using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverObject; // Reference to the Game Over object

    private int score;
    private Blade blade;
    private Spawner spawner;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        newGame();
    }

    public void IncreseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void newGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOverObject.SetActive(false); // Hide the Game Over object
    }

    public void Explosion()
    {
        blade.enabled = false;
        spawner.enabled = false;
        gameOverObject.SetActive(true); // Show the Game Over object
    }
}
