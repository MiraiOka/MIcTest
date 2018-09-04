using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using NCMB;

public class SVolumeController : MonoBehaviour
{

    public static SVolumeController instance;
    Text text;
    [SerializeField] float S_Volume;

    AudioSource audio;


    void Start()
    {
        text = GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, 44100);
        audio.Play();

        this.UpdateAsObservable()
           .Where(_ => Input.GetKeyDown(KeyCode.UpArrow))
           .Subscribe(_ => plusS());

        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.DownArrow))
            .Subscribe(_ => minusS());
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
		print("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa:::" + vol);

        NCMBObject volumeClass = new NCMBObject("VolumeClass");

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("VolumeClass");
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e == null)
            {
                text.text = (objList[0])["s"].ToString();
            }
            else
            {
                print("Error:" + e);
            }
        });
    }
    /*
        void changeText()
        {
            NCMBObject volumeClass = new NCMBObject("VolumeClass");

            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("VolumeClass");
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e == null)
                {
                    text.text = (objList[0])["s"].ToString();
                }
                else
                {
                    print("Error:" + e);
                }
            });
        }*/
    

    void plusS()
    {
        S_Volume++;
        VolumeManager.instance.setS_Volume(S_Volume);
    }

	void minusS()
	{
		S_Volume--;
		VolumeManager.instance.setS_Volume(S_Volume);      
	}
}