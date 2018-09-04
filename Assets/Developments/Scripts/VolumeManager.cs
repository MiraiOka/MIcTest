using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class VolumeManager : MonoBehaviour
{

    public static VolumeManager instance;

    [SerializeField] float S_Volume;
    [SerializeField] float A1_Volume;
    [SerializeField] float A3_Volume;
    [SerializeField] float B1_Volume;
    [SerializeField] float B3_Volume;
    [SerializeField] float RightPole_Volume;
    [SerializeField] float LeftPole_Volume;
    [SerializeField] float RightBack_Volume;
    [SerializeField] float LeftBack_Volume;
    [SerializeField] float C_Volume;
    [SerializeField] float D1_Volume;
    [SerializeField] float D3_Volume;

    NCMBObject volumeClass;

    public VolumeManager(float s, float a1, float a3, float b1, float b3, float rp, float lp, float rb, float lb, float c, float d1, float d3)
    {
        S_Volume = s;
        A1_Volume = a1;
        A3_Volume = a3;
        B1_Volume = b1;
        B3_Volume = b3;
        RightPole_Volume = rp;
        LeftPole_Volume = lp;
        RightBack_Volume = rb;
        LeftBack_Volume = lb;
        C_Volume = c;
        D1_Volume = d1;
        D3_Volume = d3;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this);

        volumeClass = new NCMBObject("VolumeClass");

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("VolumeClass");
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e == null)
            {
                if (objList.Count != 0)
                {
                    volumeClass.ObjectId = objList[0].ObjectId;
                }
            }
            else
            {
                print("Error:" + e);
            }
            volumeClass["s"] = 0;
            volumeClass.SaveAsync();
        });
    }

    public void setS_Volume(float s)
    {
        S_Volume = s;
        volumeClass["s"] = S_Volume;
        volumeClass.SaveAsync();
    }
}