using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawn : MonoBehaviour
{
    public int PointNum;
    private float Width;
    private float cubeMoveTime;
    float zStep;

    // Start is called before the first frame update
    void Start()
    {
        cubeMoveTime = GameObject.Find("GameCtrl").GetComponent<GameCtrl>().cubeMoveTime;
        Width = gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x
            * gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createCube(string tag)
    {
        if (tag == "both")
        {
            GameObject go = GameObject.Instantiate(Resources.Load("red") as GameObject);
            createOneCube(go,"left");
            GameObject go1 = GameObject.Instantiate(Resources.Load("red") as GameObject);
            createOneCube(go1,"right");
        } else if (tag == "left")
        {
            GameObject go = GameObject.Instantiate(Resources.Load("green") as GameObject);
            createOneCube(go, tag);
        }
        else if (tag == "right")
        {
            GameObject go = GameObject.Instantiate(Resources.Load("yellow") as GameObject);
            createOneCube(go, tag);
        }
    }


    private GameObject createOneCube(GameObject go, string tag)
    {
        go.AddComponent<CubeTween>().Init(cubeMoveTime);
        go.transform.SetParent(transform);
        float xOffset = 0;
        if (tag == "left")
            xOffset = -0.25f;
        else if (tag == "right")
        {
            xOffset = 0.25f;
        }
        go.transform.localPosition = new Vector3(xOffset, 0, 0);
        return go;
    }
}
