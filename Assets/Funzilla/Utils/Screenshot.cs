using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Screenshot : MonoBehaviour
{
	public string FolderPath = "";
	public KeyCode captureKey = KeyCode.Space;

	private void Update()
	{
		if (Input.GetKeyDown(captureKey))
		{
			ScreenCapture.CaptureScreenshot(FolderPath + "/" + Time.time + ".png");
		}
	}
}