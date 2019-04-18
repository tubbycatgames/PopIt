using UnityEngine;

public class ZitLander : MonoBehaviour
{
    public void Start() 
    {
        var x = Random.Range(-0.5f, 0.5f);
        var z = Random.Range(-1.0f, 1.0f);

        transform.position = new Vector3(
            x,
            transform.position.y,
            z
        );
    }
}
