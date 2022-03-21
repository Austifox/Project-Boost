
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Don't go backwards!");
                break;
            case "Finish":
                Debug.Log("You Made it!");
                break;
            case "Fuel":
                Debug.Log("Time to top up");
                break;
            default:
                Debug.Log("Better lock next time!");
                break;

        }
    }
}
