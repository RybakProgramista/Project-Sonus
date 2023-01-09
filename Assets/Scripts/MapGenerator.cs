using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MapGenerator : MonoBehaviour
{
    public int MAP_SIZE;
    public float smooth, wallOffset, scale = 7f;
    private int offsetX, offsetY;
    private Renderer renderer;

    public int[,] map;
    public GameObject straight, curve, triple, cross;
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            renderer = GetComponent<Renderer>();
            offsetX = Random.Range(-100, 101);
            offsetY = Random.Range(-100, 101);
            map = new int[MAP_SIZE, MAP_SIZE];

            GenerateMap();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void GenerateMap()
    {
        renderer.material.mainTexture = GenerateNoise();
        for(int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if(map[x,y] == 0)
                {
                    spawnWall(x, y);
                }
                else
                {
                    //little chance for lamp
                }
            }
        }
    }
    Texture2D GenerateNoise()
    {
        Texture2D texture = new Texture2D(MAP_SIZE, MAP_SIZE);
        for (int x = 0; x < MAP_SIZE; x++)
        {
            for (int y = 0; y < MAP_SIZE; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
                map[x,y] = (int)texture.GetPixel(x, y).grayscale;

            }
        }
        texture.Apply();
        return texture;
    }
    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / MAP_SIZE * scale + offsetX;
        float yCoord = (float)y / MAP_SIZE * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        if(sample > smooth)
        {
            sample = 1;
        }
        else
        {
            sample = 0;
        }
        return new Color(sample, sample, sample);
    }
    void spawnWall(int x, int y)
    {
        int[] wN = new int[4];

        if (y + 1 == map.GetLength(1))
        {
            wN[0] = 1;
        }
        else
        {
            wN[0] = map[x, y + 1];
        }
        if(x + 1 == map.GetLength(0))
        {
            wN[1] = 1;
        }
        else
        {
            wN[1] = map[x + 1, y];
        }
        if(y == 0)
        {
            wN[2] = 1;
        }
        else
        {
            wN[2] = map[x, y - 1];
        }
        if (x == 0)
        {
            wN[3] = 1;
        }
        else
        {
            wN[3] = map[x - 1, y];
        }

        if(wN[0] == 0 && wN[1] == 0 && wN[2] == 0 && wN[3] == 0)
        {
            PhotonNetwork.Instantiate("cross", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.identity);
        }
        else if (wN[0] == 1 && wN[1] == 1 && wN[2] == 1 && wN[3] == 1)
        {
            PhotonNetwork.Instantiate("cross", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.identity);
        }
        else if(wN[0] == 0 && wN[1] == 1 && wN[2] == 0 && wN[3] == 1)
        {
            PhotonNetwork.Instantiate("straight", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.identity);
        }
        else if(wN[0] == 1 && wN[1] == 0 && wN[2] == 1 && wN[3] == 0)
        {
            PhotonNetwork.Instantiate("straight", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.Euler(0f, 90f, 0f));
        }
        else if (wN[0] == 0 && wN[1] == 0 && wN[2] == 1 && wN[3] == 0)
        {
            PhotonNetwork.Instantiate("triple", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.Euler(0f, 90f, 0f));
        }
        else if (wN[0] == 0 && wN[1] == 0 && wN[2] == 0 && wN[3] == 1)
        {
            PhotonNetwork.Instantiate("triple", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.Euler(0f, 180f, 0f));
        }
        else if (wN[0] == 1 && wN[1] == 0 && wN[2] == 0 && wN[3] == 0)
        {
            PhotonNetwork.Instantiate("triple", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.Euler(0f, 270f, 0f));
        }
        else if (wN[0] == 0 && wN[1] == 1 && wN[2] == 0 && wN[3] == 0)
        {
            PhotonNetwork.Instantiate("triple", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.identity);
        }
        else if(wN[0] == 0 && wN[1] == 0 && wN[2] == 1 && wN[3] == 1)
        {
            PhotonNetwork.Instantiate("curve", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.Euler(0f, 90f, 0f));
        }
        else if (wN[0] == 1 && wN[1] == 0 && wN[2] == 0 && wN[3] == 1)
        {
            PhotonNetwork.Instantiate("curve", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.Euler(0f, 180f, 0f));
        }
        else if (wN[0] == 1 && wN[1] == 1 && wN[2] == 0 && wN[3] == 0)
        {
            PhotonNetwork.Instantiate("curve", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.Euler(0f, 270f, 0f));
        }
        else if (wN[0] == 0 && wN[1] == 1 && wN[2] == 1 && wN[3] == 0)
        {
            PhotonNetwork.Instantiate("curve", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.Euler(0f, 0f, 0f));
        }
        else
        {
            if(wN[0] == 0)
            {
                PhotonNetwork.Instantiate("straight", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate("straight", new Vector3(x * wallOffset, 0, y * wallOffset), Quaternion.Euler(0f, 90f, 0f));
            }
        }
    }
}
