using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class LevelGameOverSituation : MonoBehaviour
{
    public TextMeshProUGUI currentlives;

    public UnityEvent unityEvents;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (int.Parse(currentlives.text) == 0)
        {
            unityEvents.Invoke();
        }
    }
}
