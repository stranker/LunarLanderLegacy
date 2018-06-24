using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public int chunkCount;
    public int planeTerrainWidth = 10;
    public int mountainTerrainWidth = 30;
    public int pitTerrainWidth = 20;
    public float maxHighMountain;
    public float minHighMountain;
    public float maxLowPit;
    public float minLowPit;
    public int firstPointX;
    private List<int> chunks;

    private void Start()
    {
        chunks = new List<int>();
        CreateTerrain();
    }

    private void CreateTerrain()
    {
        LineRenderer terrain = GetComponent<LineRenderer>();
        terrain.startWidth = 0.08f;
        Vector3 firstPoint = new Vector3(firstPointX, UnityEngine.Random.Range(-1.0f, 2.0f));
        List<Vector3> terrainPositions = new List<Vector3>();
        terrainPositions.Add(firstPoint);
        terrain.positionCount = 1;
        for (int i = 0; i < chunkCount; i++)
        {
            chunks.Add(UnityEngine.Random.Range(0, 3));
            switch (chunks[i])
            {
                case 0:
                    terrain.positionCount += planeTerrainWidth;
                    CreatePlaneTerrain(terrainPositions);
                    break;
                case 1:
                    terrain.positionCount += mountainTerrainWidth;
                    CreateMountainTerrain(terrainPositions);
                    break;
                case 2:
                    terrain.positionCount += pitTerrainWidth;
                    CreatePitTerrain(terrainPositions);
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < terrain.positionCount; i++)
        {
            terrain.SetPosition(i, terrainPositions[i]);
        }
        CreateTerrainCollider(terrainPositions);
    }

    private void CreateTerrainCollider(List<Vector3> terrainPositions)
    {
        Vector2[] positions = new Vector2[terrainPositions.Count]; 
        EdgeCollider2D terrainCollider = GetComponent<EdgeCollider2D>();
        for (int i = 0; i < terrainPositions.Count; i++)
        {
            Vector2 colliderPosition = new Vector2(terrainPositions[i].x, terrainPositions[i].y);
            positions[i] = colliderPosition;
        }
        terrainCollider.points = positions;
    }

    private void CreatePlaneTerrain(List<Vector3> listPoint)
    {
        for (int i = 0; i < planeTerrainWidth; i++)
        {
            Vector3 point = new Vector3(listPoint[listPoint.Count - 1].x + 0.5f, listPoint[listPoint.Count - 1].y + UnityEngine.Random.Range(-0.1f, 0.1f));
            listPoint.Add(point);
        }
    }

    private void CreateMountainTerrain(List<Vector3> listPoint)
    {
        float mountainHigh = UnityEngine.Random.Range(minHighMountain,maxHighMountain);
        float currentHigh = 0;
        bool topControl = false;
        for (int i = 0; i < mountainTerrainWidth; i++)
        {
            Vector3 lastPoint = listPoint[listPoint.Count - 1];
            Vector3 point = Vector2.zero;
            point.x = lastPoint.x + 0.3f;
            if (!topControl && currentHigh < mountainHigh)
            {
                point.y = lastPoint.y + UnityEngine.Random.Range(0.1f, 0.5f);
                currentHigh += point.y - lastPoint.y;
            }
            else
            {
                topControl = true;
                point.y = lastPoint.y - UnityEngine.Random.Range(0.1f, 0.5f);

            }
            listPoint.Add(point);
        }
    }

    private void CreatePitTerrain(List<Vector3> listPoint)
    {
        float lower = -UnityEngine.Random.Range(minLowPit,maxLowPit);
        float currentlow = 0;
        bool lowControl = false;
        for (int i = 0; i < pitTerrainWidth; i++)
        {
            Vector3 lastPoint = listPoint[listPoint.Count - 1];
            Vector3 point = Vector2.zero;
            point.x = lastPoint.x + 0.3f;
            if (!lowControl && currentlow > lower)
            {
                point.y = lastPoint.y - UnityEngine.Random.Range(0, 0.4f);
                currentlow -= lastPoint.y - point.y;
            }
            else
            {
                lowControl = true;
                point.y = lastPoint.y + UnityEngine.Random.Range(0, 0.4f);
            }
            listPoint.Add(point);
        }
    }
}
