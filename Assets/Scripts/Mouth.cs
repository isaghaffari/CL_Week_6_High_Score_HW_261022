using UnityEngine;

public class Mouth : MonoBehaviour
{
    
    //how fast the mouth moves
   //public float speed = 10f

   void Update()
   {
       //gets the mouse position in screen pixels
       Vector3 mouseScreen = Input.mousePosition;
       
       //converts that pixel posiution into a position in the game world
       Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
       
       //m,oves the mouth to the mouse's x position, keeping its y and z movement static
       transform.position = new Vector3(mouseWorld.x, transform.position.y, transform.position.z);
   }
}
