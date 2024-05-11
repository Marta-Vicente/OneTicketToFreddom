using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person : MonoBehaviour
{

    public string characterName { get; set; }
    public int age { get; set; }
    public int threatLevel { get; set; }

    public string threatLevelJustification { get; set; }
    public int healthStatus { get; set; }
    public string healthStatusDescription { get; set; }

    public string description { get; set; }

    public PersonData personData { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Speak()
    {
        return "Name: "  + characterName + "\n" +
            "Age:" + age + "\n \n" +
            description + "\n \n" +
            "Threat: " + threatLevel + "\n" +
            "( " + threatLevelJustification + ") \n \n" +
            "health status: " + healthStatus + "\n" +
            "( " + healthStatusDescription + ")";
    }

}
