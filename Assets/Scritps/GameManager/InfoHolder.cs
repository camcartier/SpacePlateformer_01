using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoHolder : MonoBehaviour
{
    public bool dialogHasHappened;
    public int LVL1sceneLoadCount;

    public static InfoHolder Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded (UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TestLevel1")
        {
            LVL1sceneLoadCount++;
            
            if (LVL1sceneLoadCount > 1)
            {
                dialogHasHappened = true;
            }

        }
    }
}
