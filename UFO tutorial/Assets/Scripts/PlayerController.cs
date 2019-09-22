using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed;
    private Rigidbody2D rb2d;
    private int count;
    public Text countText;
    public Text winText;
    public Text nextLevelText;
    public Text livesText;
    private int levelCounter;
    public int level;
    public int maxLevel;
    public int lives;
    public Transform nextLevel;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        levelCounter = 14;
        count = 0;
        winText.text = "";
        SetCount();
        SetLivesText();
    }
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count += 1;
            SetCount();
        }
        if (other.gameObject.CompareTag("BadPickUp")) {
            lives -= 1;
            other.gameObject.SetActive(false);
            SetLivesText();
            if (lives <1) {
                SetLivesText();
                winText.text = "you lose! press R to restart.";
                gameObject.SetActive(false);
            }
        } 
    }
    void SetLivesText() {
        livesText.text = "Lives: " + lives.ToString();
    }
    void SetCount() {
        countText.text = "Count: " + count.ToString();
        if (count == levelCounter) {
            level += 1;
            nextLevelText.text = "Level: " + level.ToString();
            transform.position = nextLevel.transform.position;
            count = 0;
        }
        if (level == maxLevel) {
            winText.text = "You win! Game by Alex Smith";
            gameObject.SetActive(false);
        }
        if (level == 2) {
            levelCounter = 9;
        }
    }
    
}
