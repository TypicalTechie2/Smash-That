using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private Player playerScript;
    private void Awake()
    {
        playerScript = FindAnyObjectByType<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Crystal"))
        {
            int addToCurrentBallCount = playerScript.currentBallCount += 5;
            playerScript.currentBallCountText.text = addToCurrentBallCount.ToString();
        }
    }
}
