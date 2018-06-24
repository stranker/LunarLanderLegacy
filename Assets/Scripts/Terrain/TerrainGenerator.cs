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
    [Range(0,2)]
    public List<int> chunksPredefined;
    public List<int> chunksInGame;
    public List<GameObject> windZones;
    public int windZonesAmount;
    public LayerMask terrainLayer;
    public int distanceBetweenWinds;

    private void Start()
    {
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
        chunksInGame = new List<int>();
        if (chunksPredefined.Count > 0)
        {
            chunksInGame = chunksPredefined;
        }
        else
        {
            chunksInGame = GenerateRandomChunks(chunkCount);
        }
        for (int i = 0; i < chunksInGame.Count; i++)
        {
            switch (chunksInGame[i])
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
        CreateWindZones();
    }

    private void CreateWindZones()
    {
        Vector3 initialPos = new Vector3(firstPointX, maxHighMountain);
        for (int i = 0; i < windZonesAmount; i++)
        {
            initialPos.x += distanceBetweenWinds;
            Instantiate(windZones[UnityEngine.Random.Range(0, windZones.Count)], initialPos, transform.rotation, transform);
        }
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

    private List<int> GenerateRandomChunks(int count)
    {
        List<int> chunks = new List<int>();
        int noPlaneTerrainCount = 0;
        // Lleno aleatoriamente
        for (int i = 0; i < count; i++)
        {
            chunks.Add(UnityEngine.Random.Range(0, 3));
            // sumo si no es plano
            if (chunks[i] != 0)
                noPlaneTerrainCount++;
            // si tengo mas de 2 terrenos no planos consecutivos, el ultimo es plano
            if (noPlaneTerrainCount > 2)
            {
                chunks[i] = 0;
                noPlaneTerrainCount = 0;
            }
        }
        return chunks;
    }

}
