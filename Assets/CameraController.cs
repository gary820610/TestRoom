using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector3 pos)
    {
        pos.y = 0;
        pos.z = 0;
        pos.x = pos.x * -1;
        this.transform.position += pos;
        // Debug.Log("move " + pos);
    }

    public void MoveTo(Vector3 pos)
    {
        pos.y = 0;
        pos.z = 0;
        this.transform.position = pos;
    }
}
