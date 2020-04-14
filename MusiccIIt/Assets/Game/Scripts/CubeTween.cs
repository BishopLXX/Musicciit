using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTween : MonoBehaviour
{
    private float moveTime;
    private bool isDo = false;
    private GameObject hitpoint;
    private float zStep;

    public void Init(float moveTime)
    {
        

        this.moveTime = moveTime;
        this.isDo = true;

        hitpoint = GameObject.Find("HitPoint");
        float Z = hitpoint.transform.position.z - GameObject.Find("CubeSpawn").transform.position.z;
        zStep = Z * 1000 / ((moveTime * (1/Time.deltaTime))*1000);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDo)
        {
            this.moveTime -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x,transform.position.y, transform.position.z + zStep);
            if (this.moveTime <= -1)
            {
                Destroy(gameObject);
            }
        }
    }
}
