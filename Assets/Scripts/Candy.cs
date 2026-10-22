using UnityEngine;

public class Candy : MonoBehaviour
{
    public float fallSpeed = 4f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;


        //if the candy has fallen beolow thge bottom of the screen...
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("candy caught!!!");
            
            //this will tell the gamemanager to add 1 point to candy caught score
            GameManager.instance.AddPoint();

            Destroy(gameObject);
        }
    }
}
