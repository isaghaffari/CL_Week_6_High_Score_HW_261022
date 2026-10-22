using UnityEngine;

public class CandySpawner : MonoBehaviour
{
   //a list of candy prefabs. i will put candy prefabs in here
   public GameObject[] candyPrefabs;

   public float spawnInterval = 0.5f;
   public float minFallSpeed = 2f;
   public float maxFallSpeed = 7f;
   public float xRange = 8f;
   private float timer;

   void Update()
   {
      timer -= Time.deltaTime;

      if (timer <= 0f)
      {
         //picks a random nuymber from 0 up to the number of prefabs in the list
         int randomIndex = Random.Range(0, candyPrefabs.Length);
         
         //picks a random x position between -x and x
         float randomX = Random.Range(-xRange, xRange);

         //builds the spawn position: random x, near the top of the screen
         Vector3 spawnPosition = new Vector3(randomX, 6f, 0f);
         
         //creates a copy of the chosen prefab at that position with no rotation
         //Instantiate(candyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
         
         //so m and ms are falling at different speeds
         GameObject newCandy = Instantiate(candyPrefabs[randomIndex], spawnPosition, Quaternion.identity);

         newCandy.GetComponent<Candy>().fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
         
         //resets the timer so we wait again before the next drop
         timer = spawnInterval;
      }
   }
}
