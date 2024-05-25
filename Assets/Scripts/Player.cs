using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerCameraShake playerCamera;
    public TMP_Text currentBallCountText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float ballSpeed = 50f;
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.2f;
    private int initialBallCount = 25;
    public int currentBallCount;
    public int score;



    private void Awake()
    {
        playerCamera = GetComponent<PlayerCameraShake>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentBallCount = initialBallCount;
        currentBallCountText.text = currentBallCount.ToString();
        score = 0;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTheBall();
    }

    private void SpawnTheBall()
    {
        if (Input.GetMouseButtonDown(0) && currentBallCount > 0 && spawnManager.isGameActive)
        {
            // Get the mouse click position in world space
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));

            // Calculate the direction towards the mouse position
            Vector3 direction = (clickPosition - transform.position).normalized;

            // Instantiate the ball at the player's position
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            currentBallCount--;
            currentBallCountText.text = currentBallCount.ToString();

            // Add force to the ball in the calculated direction
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction * ballSpeed, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("Rigidbody component not found on the ball prefab!");
            }

            Destroy(ball, 5f);
        }

        else if (currentBallCount <= 0)
        {
            StartCoroutine(CheckGameOver());
            currentBallCountText.text = 0.ToString();
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle") && currentBallCount > 0)
        {
            Debug.Log("Collided with Obstacle");
            currentBallCount -= 10;
            currentBallCountText.text = currentBallCount.ToString();

            if (playerCamera != null)
            {
                StartCoroutine(playerCamera.cameraShake(shakeDuration, shakeMagnitude));
            }
        }
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
        scoreText.text = "Score: " + score;
    }

    private IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(3f);

        if (currentBallCount <= 0)
        {
            spawnManager.isGameActive = false;
        }
    }
}
