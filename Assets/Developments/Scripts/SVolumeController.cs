using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SVolumeController : MonoBehaviour
{

    public static SVolumeController instance;
    [SerializeField] float S_Volume;

    AudioSource audio;


    void Start()
	{
        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, 44100);
        audio.Play();
        /*
        this.UpdateAsObservable()
           .Where(_ => Input.GetKeyDown(KeyCode.UpArrow))
           .Subscribe(_ => plusS());

        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.DownArrow))
            .Subscribe(_ => minusS());
            */
    }

    void Update()
    {
		float[] data = new float[256];
        float vol = 0;
        audio.GetOutputData(data, 0);
      
        foreach (float s in data)
        {
            vol += Mathf.Abs(s);         
        }

        VolumeManager.instance.setS_Volume(vol);
    }
}