using UnityEngine;

public class delivery : MonoBehaviour
{

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("package"))
    {

      Debug.Log("Picked up package");
    }

  }
}
