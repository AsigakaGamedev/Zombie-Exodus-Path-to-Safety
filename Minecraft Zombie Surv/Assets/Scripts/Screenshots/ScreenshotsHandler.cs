using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotsHandler : MonoBehaviour
{
    public Camera screenshotCamera;
    public string screenshotsFolder = "Screenshots";
    public string screenshotName = "screenshot";

    [Button]
    public void TakeScreenshot()
    {
        string folderPath = Application.dataPath + "/" + screenshotsFolder;
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string screenshotPath = folderPath + "/" + screenshotName + "_" + timestamp + ".png";
        ScreenCapture.CaptureScreenshot(screenshotPath);
    }
}
