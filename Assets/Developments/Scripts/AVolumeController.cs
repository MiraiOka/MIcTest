using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.UI;

public class AVolumeController : MonoBehaviour {

	public static AVolumeController instance;
	Text text;
	[SerializeField] float A_Volume;

	AudioSource audio;

	void Start () {
		text = GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, 44100);
        audio.Play();
	}

	void Update () {
		float[] data = new float[256];
        float vol = 0;
        audio.GetOutputData(data, 0);

        foreach (float s in data)
        {
            vol += Mathf.Abs(s);
        }

        VolumeManager.instance.setA_Volume(vol);

		NCMBObject volumeClass = new NCMBObject("VolumeClass");

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("VolumeClass");
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e == null)
            {
                text.text = (objList[0])["a"].ToString();
            }
            else
            {
                print("Error:" + e);
            }
        });
	}
}
