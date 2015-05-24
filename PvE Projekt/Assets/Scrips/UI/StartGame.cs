using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private string firstLevelName = "";

    public void LoadFirstLevel()
    {
        Application.LoadLevel(firstLevelName);
    }
}
