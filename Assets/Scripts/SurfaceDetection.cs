using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDetection : MonoBehaviour
{
    public GameObject tracker;
    private GameObject obj = null;

    public Teleporter teleporter;

    // Start is called before the first frame update
    void Start()
    {
        teleporter = GetComponent<Teleporter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            obj = collision.gameObject;
            tracker.transform.position = obj.transform.position;
            //Debug.Log("Got Hit by a " + obj.gameObject.name);

            Destroy(obj, 3);
        }
    }
}
