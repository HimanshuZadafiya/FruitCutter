using UnityEngine;

public class DestroyFurite : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Fruit") )
        {
            Destroy(col.gameObject);
        }
    }
}
