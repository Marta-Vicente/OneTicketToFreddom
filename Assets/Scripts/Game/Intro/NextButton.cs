using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    public Text original;
    public Text sentence2;
    public Text sentence3;
    public Text sentence4;
    public Text sentence5;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewText()
    {
        
        Debug.Log(count.ToString());
        switch (count)
        {
            case 1:
                original.text = sentence2.text;
                count++;
                Debug.Log(count.ToString());
                break;
            case 2:
                original.text = sentence3.text;
                count++;
                Debug.Log(count.ToString());
                break;
            case 3:
                original.text = sentence4.text;
                count++;
                Debug.Log(count.ToString());
                break;
            case 4:
                original.text = sentence5.text;
                count++;
                Debug.Log(count.ToString());
                break;
            case 5:
                SceneManager.LoadScene(1);
                break;
            default:
                break;
        }


    }
}
