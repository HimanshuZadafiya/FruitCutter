using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FruitScript : MonoBehaviour
{

    [Header("--- UI Element ---")]
    public SpriteRenderer fruitImg;
    public SpriteRenderer fruitCut1;
    public SpriteRenderer fruitCut2;
    public ParticleSystem fruitParticalSystem;


    public GameObject fruiteObj;
    public Rigidbody2D fruiteRB;
    public Rigidbody2D cut1RB;
    public Rigidbody2D cut2RB;
    public CircleCollider2D collider;
    public AudioClip cut;
    public AudioSource audio2;
    public AudioSource audioget;
    public int score;

    public bool isCut = false;



    public void UpdateFruitData(FruitData fruitData)
    {
        float fruitsize = fruitData.fruitsize;
        gameObject.transform.localScale = new Vector3(fruitsize, fruitsize, fruitsize);

        fruitImg.sprite = fruitData.fruitSprite;
        fruitCut1.sprite = fruitData.fruitSprite1;
        fruitCut2.sprite = fruitData.fruitSprite1;

        ParticleSystem.MainModule main = fruitParticalSystem.main;
        main.startColor = fruitData.particaleColor;


    }


    public void StartCutAnimation()
    {
        isCut = true;
        fruiteObj.gameObject.SetActive(false);
        cut1RB.gameObject.SetActive(true);
        cut2RB.gameObject.SetActive(true);
        fruitParticalSystem.gameObject.SetActive(true);

        cut1RB.AddForce(new Vector2(1f, 1f), ForceMode2D.Impulse);
        cut2RB.AddForce(new Vector2(-1f, -1f), ForceMode2D.Impulse);
        cut1RB.AddTorque(Random.Range(-50f, 50f));
        cut2RB.AddTorque(Random.Range(-50f, 50f));

        ScoreManage(score, true, transform.position);

        audio2.clip = cut;
        audio2.Play();

    }

    public void ScoreManage(int no, bool isPlus, Vector3 pos)
    {
        GameObject genScore = Instantiate(FruitGameManager.Instance.scoreGen, FruitGameManager.Instance.scoreGenParent.transform);//, Quaternion.identity);
        genScore.transform.position = pos;
        if (isPlus)
        {
            score += no;
            genScore.GetComponent<Text>().text = "+" + no;
        }
        else
        {
            score -= no;
            genScore.GetComponent<Text>().text = "-" + no;
            if (score <= 0)
            {
                score = 0;
            }
        }
        StartCoroutine(DestroyObject(genScore));
        FruitGameManager.Instance.CurrentScoreManage(score);
    }

    IEnumerator DestroyObject(GameObject genScore)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(genScore);
    }



}
