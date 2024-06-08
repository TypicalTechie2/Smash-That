using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public AudioSource playerAudio;
    public AudioClip ballSpawnClip;
    public AudioClip hitImpactClip;
    public Button homeButton;
    [SerializeField] NewHighScore newHighScoreScript;
    [SerializeField] HighScoreManager highScoreManagerScript;
    [SerializeField] GameManager gameManagerScript;
    private PlayerCameraShake playerCamera;
    [SerializeField] Image highScoreImage;
    public TMP_Text gameOverText;
    public Button restartButton;
    public TMP_Text currentBallCountText;
    public TMP_Text scoreText;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float ballSpeed = 2500f;
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.2f;
    public int initialBallCount = 25;
    public int currentBallCount;
    public int score;
    public bool hasShownHighScore;

    private void Awake()
    {
        playerCamera = GetComponent<PlayerCameraShake>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hasShownHighScore = false;
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

    // Method to spawn the ball upon Touch / Mouse Click
    private void SpawnTheBall()
    {
        // Check if the pointer is over a UI element
        if (IsPointerOverUIObject())
        {
            return;
        }

        bool isMouseClick = Input.GetMouseButtonDown(0);
        bool isTouch = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        // Check if mouse click or touch input is detected and game conditions are met
        if ((isMouseClick || isTouch) && currentBallCount > 0 && spawnManager.isGameActive && !gameManagerScript.isGamePaused)
        {
            playerAudio.PlayOneShot(ballSpawnClip, 0.1f);

            Vector3 inputPosition;
            if (isMouseClick)
            {
                inputPosition = Input.mousePosition;
            }
            else
            {
                inputPosition = Input.GetTouch(0).position;
            }

            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.main.transform.position.y));
            Vector3 direction = (clickPosition - transform.position).normalized;

            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            currentBallCount--;
            currentBallCountText.text = currentBallCount.ToString();

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

    // Method to check if pointer is over a UI object
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.touchCount > 0 ? (Vector2)Input.GetTouch(0).position : (Vector2)Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle") && currentBallCount > 0)
        {
            playerAudio.PlayOneShot(hitImpactClip, 1f);

            // Vibrate the device
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif

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

        if (score > highScoreManagerScript.highScore)
        {
            highScoreManagerScript.SaveHighScore(score);
            StartCoroutine(ShowNewHighScoreImage());
            hasShownHighScore = true;
        }
    }

    // Coroutine to show new high score image
    private IEnumerator ShowNewHighScoreImage()
    {
        if (!hasShownHighScore)
        {
            highScoreImage.gameObject.SetActive(true);
            StartCoroutine(newHighScoreScript.ZoomInHighScore());
            yield return new WaitForSeconds(3f);
            highScoreImage.gameObject.SetActive(false);
        }
    }

    // Coroutine to check game over condition
    private IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(3f);

        if (currentBallCount <= 0)
        {
            spawnManager.isGameActive = false;
            hasShownHighScore = false;
            if (!gameManagerScript.returnToMenu)
            {
                gameOverText.gameObject.SetActive(true);
                homeButton.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);
            }
            gameManagerScript.gamePauseImage.gameObject.SetActive(false);
        }
    }
}
