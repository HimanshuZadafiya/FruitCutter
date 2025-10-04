using System.Collections;
using UnityEngine;

public class Blade : MonoBehaviour
{

    public static Blade Instance;
    public GameObject bladeTrailPrefab;
    public float minCuttingVelocity = .001f, moveX = 0f;

    public GameObject blast;
    public AudioClip bombs, cuts;
    public AudioSource audio1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("Fruit"))
        {
            FruitScript fruitScript = col.gameObject.GetComponent<FruitScript>();
            if (!fruitScript.isCut)
            {
                fruitScript.StartCutAnimation();
            }
        }
        if (col.gameObject.tag == "Bomb")
        {
            GameObject b = Instantiate(blast, transform.position, Quaternion.identity);
            if (DataManager.Instance.GetSound() == 0)
            {
                audio1.clip = bombs;
                audio1.Play();
            }
            StartCoroutine(DestroyObj(b));
            Destroy(col.gameObject);

            FruitGameManager.Instance.StopSpawning();
        }
        if (col.gameObject.tag == "Ticket")
        {
            Debug.Log("--- Destroy Ticket ---");
            Destroy(col.gameObject);
        }

    }

    IEnumerator DestroyObj(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }
}



