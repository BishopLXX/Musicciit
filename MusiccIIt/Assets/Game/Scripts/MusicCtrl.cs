using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCtrl : MonoBehaviour
{

    KoreographyEvent curTextEvent;

    public string eventID;

    private CubeSpawn cubeSpawn;

    public Material red = null;
    public Material green = null;
    public Material yellow = null;
    public Renderer spawnRend;
    public Renderer hitRend;
    float moveTime;

    // Start is called before the first frame update
    void Start()
    {
        red = Resources.Load("meterials/red") as Material;
        green = Resources.Load("meterials/green") as Material;
        yellow = Resources.Load("meterials/yellow") as Material;
        moveTime = GameObject.Find("GameCtrl").GetComponent<GameCtrl>().cubeMoveTime;

        Koreographer.Instance.RegisterForEventsWithTime(eventID, UpdateText);
        cubeSpawn = GameObject.Find("CubeSpawn").GetComponent<CubeSpawn>();
        spawnRend = GameObject.Find("CubeSpawn").GetComponent<Renderer>();
        hitRend = GameObject.Find("HitPoint").GetComponent<Renderer>();
    }

    void OnDestroy()
    {
        // Sometimes the Koreographer Instance gets cleaned up before hand.
        //  No need to worry in that case.
        if (Koreographer.Instance != null)
        {
            Koreographer.Instance.UnregisterForAllEvents(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateText(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        // Verify that we have Text in the Payload.
        if (evt.HasTextPayload())
        {
            // Set the text if we have a text event!
            // We can get multiple events called at the same time (if they overlap in the track).
            //  In this case, we prefer the event with the most recent start sample.
            if (curTextEvent == null ||
                (evt != curTextEvent && evt.StartSample > curTextEvent.StartSample))
            {
                string text = evt.GetTextValue();
                Debug.Log(text);
                if (text == "left")
                {
                    spawnRend.sharedMaterial = green;
                    Invoke("Green", moveTime);
                } else if (text == "right")
                {
                    spawnRend.sharedMaterial = yellow;
                    Invoke("Yellow", moveTime);
                }
                else
                {
                    spawnRend.sharedMaterial = red;
                    Invoke("Red", moveTime);
                }
                cubeSpawn.createCube(text);
                // Store for later comparison.
                curTextEvent = evt;
            }

            // Clear out the text if our event ended this musical frame.
            if (curTextEvent.EndSample < sampleTime)
            {
                // guiTextCom.text = string.Empty;

                // Remove so that the above timing logic works when the audio loops/jumps.
                curTextEvent = null;
            }
        }
    }

    void Green()
    {
        hitRend.sharedMaterial = green;
    }
    void Red()
    {
        hitRend.sharedMaterial = red;
    }
    void Yellow()
    {
        hitRend.sharedMaterial = yellow;
    }
}
