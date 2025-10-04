using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

public class FruitGameManager : MonoBehaviour
{
    public static FruitGameManager Instance;


    [Header("--- UI Elements ---")]
    public Text playerScoreTxt1;
    public Text timerTxt;
    public GameObject playerScoreBox;

    public GameObject scoreGen;
    public GameObject scoreGenParent;
    public GameObject bladeObj;
    public GameObject difficultyPanel;

    public float secondsCount;
    int flag = 0;

    public int playerScore;
    public int milestoneStep = 50;
    public int nextMilestone = 50;

    [Header("--- Fruit Spawaner ----")]
    public List<FruitData> fruitList = new List<FruitData>();
    public GameObject fruitPrefab;
    public GameObject bombPrefab;
    public GameObject ticketPrefab;

    [Header("--- Easy Difficulty ---")]
    public FruitDifficulty easyDifficulty;

    [Header("--- Medium Difficulty ---")]
    public FruitDifficulty mediumDifficulty;

    [Header("--- Hard Difficulty ---")]
    public FruitDifficulty hardDifficulty;
    [Header("--- Current Difficulty ---")]
    public FruitDifficulty currentDifficulty;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }
    private void Update()
    {
        // Timer();

        if (Input.GetMouseButton(0))
        {
            if (!bladeObj.activeSelf)
            {
                bladeObj.SetActive(true);
            }
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bladeObj.transform.position = pos;
            Blade.Instance.bladeTrailPrefab.transform.position = pos;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            bladeObj.SetActive(false);
        }
    }

    public void SelectDifficulty(int difficulty)
    {
        if (difficulty == 0)
        {
            currentDifficulty = easyDifficulty;
        }
        else if (difficulty == 1)
        {
            currentDifficulty = mediumDifficulty;
        }
        else if (difficulty == 2)
        {
            currentDifficulty = hardDifficulty;
        }
        difficultyPanel.SetActive(false);
        playerScoreBox.gameObject.SetActive(true);


        SoundManager.Instance.StopBackgroundMusic();
        Invoke(nameof(StartSpawning), 1f);
    }

    public void StartGame()
    {
        difficultyPanel.SetActive(true);
        playerScoreBox.gameObject.SetActive(false);
    }

    public void PlayButtonClick()
    {
        difficultyPanel.SetActive(true);
    }

    void Timer()
    {
        secondsCount -= Time.deltaTime;
        float minutes = Mathf.Floor(secondsCount / 60);
        float seconds = secondsCount % 60;


        string Min = minutes.ToString();
        string Sec = Mathf.RoundToInt(seconds).ToString();
        if (Min.Length == 1)
        {
            Min = "0" + Min;
        }
        if (Sec.Length == 1)
        {
            Sec = "0" + Sec;
        }
        if (Min.Length != 1 && Sec.Length != 1)
        {
            Min = Min;
            Sec = Sec;
        }

        string timeValue = Min + ":" + Sec;
        if (timeValue.Equals("00:00"))
        {
            print("Time Over");
            timerTxt.text = "00:00";
            WinUserShow();
            flag = 1;
        }
        if (flag != 1)
        {
            timerTxt.text = timeValue;
        }
    }

    public void WinUserShow()
    {
        Debug.Log("--- User Win ---");
    }

    public void CurrentScoreManage(int score)
    {
        Debug.Log("CurrentScoreManage::::" + score);
        playerScore += score;
        playerScoreTxt1.text = playerScore.ToString();
        if (playerScore >= nextMilestone)
        {
            OnReachMilestone();
            nextMilestone += milestoneStep;
        }
    }

    private void OnReachMilestone()
    {
        Debug.Log("--- Reduse Bomb Spwan Time ---");

        if (currentDifficulty.minFruitSpawnTime > 0)
        {
            currentDifficulty.minFruitSpawnTime--;
        }
        if (currentDifficulty.maxFruitSpawnTime > 0)
        {
            currentDifficulty.maxFruitSpawnTime--;
        }

        if (currentDifficulty.minBombSpawnTime > 0)
        {
            currentDifficulty.minBombSpawnTime--;
        }
        if (currentDifficulty.maxBombSpawnTime > 0)
        {
            currentDifficulty.maxBombSpawnTime--;
        }
        if (currentDifficulty.maxBombSpawnCnt < 10)
        {
            currentDifficulty.maxBombSpawnCnt++;
        }

        if (currentDifficulty.bombSpawnChance < 100)
        {
            currentDifficulty.bombSpawnChance += 5;
        }
    }

    #region Fruit Spawaner Methods
    public void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnFruitGroup), currentDifficulty.minFruitSpawnTime, currentDifficulty.maxFruitSpawnTime);

        // Bomb spawn chance in %


        if (Random.Range(0f, 100f) < currentDifficulty.bombSpawnChance)
        {
            Debug.Log("Bomb spawn ");
            InvokeRepeating(nameof(SpawnBombGroup), currentDifficulty.minBombSpawnTime, currentDifficulty.maxBombSpawnTime);
        }
    }

    public void SpawnBombGroup()
    {
        StartCoroutine(nameof(SpwanBomb));
    }
    IEnumerator SpwanBomb()
    {
        int totalFruitSpawn = Random.Range(currentDifficulty.minBombSpawnCnt, currentDifficulty.maxBombSpawnCnt);
        for (int i = 0; i < totalFruitSpawn; i++)
        {
            SpawnOneBomb();
            // small random gap between items in the burst so they don't all overlap
            yield return new WaitForSeconds(Random.Range(0.05f, 0.25f));
        }
    }
    public void SpawnOneBomb()
    {
        float rendX = Random.Range(2, -2);                     // e.g. -MaxX .. MaxX
        float rendY = Random.Range(-6.44f, -5f);                     // spawn Y in your desired range
        Vector3 pos = new Vector3(rendX, rendY, 0f);

        Rigidbody2D b = Instantiate(bombPrefab, pos, Quaternion.identity).GetComponent<Rigidbody2D>();
        b.AddForce(new Vector2(0, 15f), ForceMode2D.Impulse);
        b.AddTorque(Random.Range(50f, -50f));

        if (Random.Range(0f, 100f) < currentDifficulty.ticketSpawnChance)
        {
            rendX += 0.5f;
            rendY += 0.5f;

            Vector3 ticketPos = new Vector3(rendX, rendY, 0f);
            Rigidbody2D rb = Instantiate(ticketPrefab, ticketPos, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, 13f), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(50f, -50f));
        }
    }


    public void SpawnFruitGroup()
    {
        StartCoroutine(nameof(SpawnFruits));
    }

    IEnumerator SpawnFruits()
    {
        int totalFruitSpawn = Random.Range(currentDifficulty.minFruitSpawnCnt, currentDifficulty.maxFruitSpawnCnt);          // 2..5

        for (int i = 0; i < totalFruitSpawn; i++)
        {
            SpawnOneFruit();
            // small random gap between items in the burst so they don't all overlap
            yield return new WaitForSeconds(Random.Range(0.05f, 0.25f));
        }
    }

    void SpawnOneFruit()
    {
        // pick random X correctly (min then max)
        float rendX = Random.Range(2, -2);                     // e.g. -MaxX .. MaxX
        float rendY = Random.Range(-6.44f, -5f);                     // spawn Y in your desired range
        Vector3 pos = new Vector3(rendX, rendY, 0f);

        if (fruitList == null || fruitList.Count == 0)
        {
            Debug.LogWarning("No fruits in fruitList!");
            return;
        }

        int randomIndex = Random.Range(0, fruitList.Count);
        FruitData fruitData = fruitList[randomIndex];

        GameObject go = Instantiate(fruitPrefab, pos, Quaternion.identity, transform); // parent to spawner (optional)
        FruitScript creator = go.GetComponent<FruitScript>();
        if (creator == null)
        {
            Debug.LogWarning("Fruit prefab missing FruitScript component!");
            return;
        }

        creator.UpdateFruitData(fruitData);

        // ensure the rigidbody is reset (important if you later use pooling)
        Rigidbody2D rb = creator.fruiteRB != null ? creator.fruiteRB : go.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("Spawned fruit missing Rigidbody2D.");
            return;
        }

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // randomized upward and small horizontal impulse + torque
        float upForce = Random.Range(10f, 15f);                     // your previous values; tune as needed
        float horizForce = Random.Range(-1.5f, 1.5f);               // small left/right variance
        rb.AddForce(new Vector2(horizForce, upForce), ForceMode2D.Impulse);

        float torque = Random.Range(-50f, 50f);
        rb.AddTorque(torque);

    }


    public void StopSpawning()
    {

        Debug.Log("--- Game Over ---");

        CancelInvoke(nameof(SpawnFruitGroup));
        StopCoroutine(SpawnFruits());


        CancelInvoke(nameof(SpawnBombGroup));
        StopCoroutine(SpwanBomb());

        if (playerScore > DataManager.Instance.GetBestScore())
        {
            DataManager.Instance.SetBestScore(playerScore);
        }

        StartGame();
    }
    #endregion
}


[System.Serializable]
public class FruitData
{
    public string fruitName;
    public Sprite fruitSprite;
    public Sprite fruitSprite1;
    public float fruitsize;

    public Color particaleColor;

}


[System.Serializable]
public class FruitDifficulty
{
    [Header("--- Total Item Spawn ---")]
    public int minFruitSpawnCnt;
    public int maxFruitSpawnCnt;

    public int minBombSpawnCnt;
    public int maxBombSpawnCnt;

    [Header("---  Time Spawn ---")]
    public float minFruitSpawnTime;
    public float maxFruitSpawnTime;
    public float minBombSpawnTime;
    public float maxBombSpawnTime;

    [Header("---  Bomb Spawn Chance (1 - 100) %  ---")]
    public float bombSpawnChance;


    [Header("---  Ticket Spawn Chance (1 - 100) %  ---")]
    public float ticketSpawnChance;
}

