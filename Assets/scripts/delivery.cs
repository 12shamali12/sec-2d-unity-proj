using UnityEngine;

public class delivery : MonoBehaviour
{
  bool has_package = false;
   float destroy_delay = 0.08f;
  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("package"))
    {
      if (has_package)
      {
        Debug.Log("already have package");
      }
      else
      {

        has_package = true;
        GetComponent<ParticleSystem>().Play();
        Debug.Log("package picked up");
        Destroy(collision.gameObject, destroy_delay);
      }
    }
      
    if (collision.CompareTag("customer"))
    {
      if (has_package)
      {
        has_package = false;
        GetComponent<ParticleSystem>().Stop();
        Debug.Log("delivered");
      }
      else
      {
        Debug.Log("NO package to deliver");
      }
    }

  }
}
