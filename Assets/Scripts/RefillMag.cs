using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;

public class ButtonControlManager : MonoBehaviour
{
    public static ButtonControlManager Instance;

    public Transform[] magSpawnPos;
    public GameObject magPrefab;
    [Space(20)]
    public GameObject targetPrefab;
    public Transform slideTarget;
    public BoxCollider[] spawnArea;
    public Text scoreText;
    public Text timerText;
    public float minSpawnInterval;
    public float maxSpawnInterval;
    private List<GameObject> targetList;
    private int score;


    private void Awake()
    {
        Instance = this;
        targetList = new List<GameObject>();
    }

    public void Update()
    {

    }

    public void SpawnMag()
    {
        for (int i = 0; i < magSpawnPos.Length; i++)
        {
            GameObject newMag = Instantiate(magPrefab);
            newMag.transform.position = magSpawnPos[i].position;
        }
    }

    public void OnPressCancelButton()
    {
        EndGame();
    }

    public void OnPressButton(int areaIndex)
    {
        StartCoroutine(GameStartCoroutine(areaIndex));
    }

    private IEnumerator GameStartCoroutine(int areaIndex)
    {
        slideTarget.gameObject.SetActive(false);

        score = 0;
        float timer = 30f;

        while (timer > 0f)
        {

            //timerText.text = $"Time: {timer:F1}";

            Bounds bounds = spawnArea[areaIndex].bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y + 0.5f, bounds.max.y);
            float z = Random.Range(bounds.min.z, bounds.max.z);

            Vector3 spawnPos = new Vector3(x, y, z);

            GameObject target = Instantiate(targetPrefab, spawnPos, Quaternion.identity);
            targetList.Add(target);
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            timer -= spawnInterval;
        }

        EndGame();
    }


    private void EndGame()
    {
        foreach (GameObject target in targetList)
        {
            if (target != null)
                Destroy(target);
            if (target == null)
                continue;
            //target.SetActive(false);
        }
        targetList.Clear();
        //timerText.text = "Time: 0.0";
        StopAllCoroutines();
        slideTarget.gameObject.SetActive(true);
    }

    public void UpdateScore()
    {
        //scoreText.text = $"Score : {score}";
    }
}
