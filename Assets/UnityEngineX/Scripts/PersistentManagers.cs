using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManagers : RuleBehaviour
{
    public Model_Variables dataModelVariables;
    
    private void Start()
    {
        // ------------------- Rule 1 -------------------
        // Get data from model
        int nextSceneIndex = dataModelVariables.intVars[0];
        
        // Load the specified scene index
        SceneManager.LoadScene(nextSceneIndex);
        
        // ------------------- Rule 2 -------------------
        // ..
    }
}