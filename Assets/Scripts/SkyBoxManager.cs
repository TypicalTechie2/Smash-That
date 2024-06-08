using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxManager : MonoBehaviour
{
    public Material[] skyBoxes; // Array to hold your SkyBox materials
    public float transitionDuration = 2.0f; // Duration of the fade in/out

    private int currentSkyBoxIndex = 0;
    private float transitionProgress = 0.0f;
    public bool isTransitioning = false;

    private float transitionTimer = 0.0f;
    public float timeBetweenTransitions = 10.0f;

    private Material blendSkyBox;
    private Material fromSkyBox;
    private Material toSkyBox;
    public SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        if (skyBoxes.Length == 0)
        {
            Debug.LogError("No SkyBoxes assigned.");
            return;
        }

        // Create a new material for blending
        blendSkyBox = new Material(Shader.Find("Custom/SkyboxBlended"));
        fromSkyBox = skyBoxes[currentSkyBoxIndex];
        RenderSettings.skybox = fromSkyBox;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.isGameActive)
        {
            transitionTimer += Time.deltaTime;

            if (transitionTimer >= timeBetweenTransitions)
            {
                StartCoroutine(TransitionToNextSkyBox());
                transitionTimer = 0.0f;
            }

            if (transitionProgress < 1.0f)
            {
                transitionProgress += Time.deltaTime / transitionDuration;
                blendSkyBox.SetFloat("_Blend", transitionProgress);
                DynamicGI.UpdateEnvironment();
            }
        }

    }

    IEnumerator TransitionToNextSkyBox()
    {
        isTransitioning = true;
        transitionProgress = 0.0f;
        currentSkyBoxIndex = (currentSkyBoxIndex + 1) % skyBoxes.Length;
        toSkyBox = skyBoxes[currentSkyBoxIndex];

        // Setup blending skybox
        blendSkyBox.SetTexture("_Tex1", fromSkyBox.GetTexture("_Tex"));
        blendSkyBox.SetTexture("_Tex2", toSkyBox.GetTexture("_Tex"));
        RenderSettings.skybox = blendSkyBox;

        yield return new WaitForSeconds(transitionDuration);

        fromSkyBox = toSkyBox;
        RenderSettings.skybox = fromSkyBox;
        DynamicGI.UpdateEnvironment();
        isTransitioning = false;
    }
}
