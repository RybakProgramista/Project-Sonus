using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsIKSolver : MonoBehaviour
{
    public Transform[] targets, bases;
    public float stepDist, wallHeight, stepSpeed, stepHeight;
    private float[] lerpT;
    private Vector3[] currPos, newPos, oldPos;
    private void Start()
    {
        currPos = new Vector3[2];
        oldPos = new Vector3[2];
        newPos = new Vector3[2];
        lerpT = new float[2];
    }
    void Update()
    {
        for(int x = 0; x < 2; x++)
        {
            targets[x].position = currPos[x];
            Ray ray = new Ray(bases[x].position, bases[x].TransformDirection(Vector3.down));
            if (Physics.Raycast(ray, out RaycastHit ground, Mathf.Infinity) && ((x == 0 && lerpT[1] >= 1) || (x == 1 && lerpT[0] > 0)))
            {
                if (Mathf.Abs(newPos[x].x - ground.point.x) > stepDist || Mathf.Abs(newPos[x].z - ground.point.z) > stepDist)
                {
                    newPos[x] = ground.point;
                    if (ground.collider.tag.Equals("wall"))
                    {
                        newPos[x].y = wallHeight / 100 * Random.Range(40, 80);
                    }
                    lerpT[x] = 0;
                }
            }
            if(lerpT[x] < 1)
            {
                Vector3 footPos = Vector3.Lerp(oldPos[x], newPos[x], lerpT[x]);
                footPos.y += Mathf.Sin(lerpT[x] * Mathf.PI) * stepHeight;
                currPos[x] = footPos;
                lerpT[x] += Time.deltaTime * stepSpeed;
            }
            else
            {
                oldPos[x] = currPos[x];
            }
        }
    }
}
