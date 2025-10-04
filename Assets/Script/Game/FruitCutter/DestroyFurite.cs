using UnityEngine;

public class DestroyFurite : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("col.gameObject.tag::::" + col.gameObject.tag + " gameObject.tag ::::: " + gameObject.tag);
        if (col.gameObject.tag == "Fruit" && gameObject.tag == "Destroyer")
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Bomb" && gameObject.tag == "Destroyer")
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Ticket" && gameObject.tag == "Destroyer")
        {
            Destroy(col.gameObject);
        }
        else
        {
            Debug.Log("--- return ---");
        }
    }
}
