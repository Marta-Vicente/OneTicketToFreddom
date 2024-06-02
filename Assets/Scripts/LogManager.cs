using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    public static LogManager Instance;

    private string fileName;

    private string nextLine = "";

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            fileName = Application.dataPath + "Logger.txt";
            CreateFile();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateFile() { 
        if (File.Exists(fileName))
        {
            StreamWriter sr = new StreamWriter(fileName, true);
            sr.WriteLine("Started New Play Session");
        }
        else
        {
            var sr = File.CreateText(fileName);
            sr.WriteLine("Created New File");
        }
    }

    public void AddToLine(string toAdd)
    {
        nextLine += " " + toAdd;
    }

    public void WriteOut()
    {
        StreamWriter sr = new StreamWriter(fileName, true);
        sr.WriteLine(nextLine);
        nextLine = "";
    }
}
