using UnityEngine;

public class InputMonitor:MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
          Application.Quit();
    }
}