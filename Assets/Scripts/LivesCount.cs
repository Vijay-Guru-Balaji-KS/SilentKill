using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesCount : MonoBehaviour
{
    public TextMeshProUGUI livesCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decrementLife()
    {
        livesCount.text = ((int.Parse(livesCount.text)+1)-2).ToString();
        //livesCount.text = (int.Parse(livesCount.text)+1).ToString();
    }
}
