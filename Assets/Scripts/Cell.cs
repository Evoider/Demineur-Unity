using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    public bool isMine;
    public bool isRevealed;
    public bool isMarked;
    public AudioClip[] explosion;
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    public int adjacentMines; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gameManager.CellMarked(this);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gameManager.CellClicked(this);
        }
    }

    public void Reveal()
    {
        isRevealed = true;
        spriteRenderer.sprite = gameManager.emptySprite;
        spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f);
    }

    public void RevealNumber()
    {
        isRevealed = true;
        spriteRenderer.sprite = gameManager.numberSprites[adjacentMines - 1];
        spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f);
    }
    
    public void RevealMine()
    {
        isRevealed = true;
        spriteRenderer.sprite = gameManager.mineSprite;
        spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f);
    }
    public void RevealExplodedMine()
    {
        isRevealed = true;
        audioSource.clip = explosion[Random.Range(0, explosion.Length)];
        audioSource.Play();
        spriteRenderer.sprite = gameManager.loseMineSprite;
        spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f);
    }

    public void ToggleMark()
    {
        isMarked = !isMarked;
        if (isMarked)
        {
            spriteRenderer.sprite = gameManager.flagSprite;
        }
        else
        {
            spriteRenderer.sprite = gameManager.defaultSprite;
        }
    }
}
