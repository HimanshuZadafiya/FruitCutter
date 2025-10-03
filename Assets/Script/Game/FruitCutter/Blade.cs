using System.Collections;
using UnityEngine;

public class Blade : MonoBehaviour
{

    public GameObject bladeTrailPrefab;
    public float minCuttingVelocity = .001f, moveX = 0f;

    public GameObject blast;
    public AudioClip bombs, cuts;
    public AudioSource audio1;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
            bladeTrailPrefab.transform.position = pos;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Vector2 direction = (targetPosition - transform.position);
            // Vector2 vel = direction * moveSpeed;
            // rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, vel, damping * Time.fixedDeltaTime);
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
            // if (DataManager.Instance.GetSound() == 0)
            // {
            //     audio1.clip = bombs;
            //     audio1.Play();
            // }
            StartCoroutine(DestroyObj(b));
            Destroy(col.gameObject);
        }
    }

    IEnumerator DestroyObj(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }
}



