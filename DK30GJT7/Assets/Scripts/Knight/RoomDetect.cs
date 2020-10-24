using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomDetect : MonoBehaviour
{
    public Tilemap Walls;
    public Tilemap Doors;
    public List<Vector2Int> OpenXYList;
    public List<Vector2Int> XYList;
    public RectInt Room;
    public int xMax;
    public int xMin;
    public int yMax;
    public int yMin;

    public RectInt DetectRoom(Vector2 Origin)
    {
        
        Vector2Int OriginInt = new Vector2Int(Mathf.RoundToInt(Origin.x), Mathf.RoundToInt(Origin.y));
        xMin = OriginInt.x;
        xMax = OriginInt.x;
        yMin = OriginInt.y;
        yMax = OriginInt.y;
        OpenXYList.Clear();
        XYList.Clear();
        LookAtChunk(OriginInt);

        if (TileCollider(OriginInt.x, OriginInt.y))
        {
            Room.SetMinMax(new Vector2Int(0,0), new Vector2Int(0,0));
        }
        int i = 0;
        while (OpenXYList.Count >= 1 && i < 2500)
        {
            i++;
            LookAtChunk(OpenXYList[0]);
            if (i >= 2500)
            {
                Debug.Log("oops");
            }
        }

        foreach (Vector2Int XY in XYList)
        {
            if (XY.x >= xMax)
            {
                xMax = XY.x;
            }
            if (XY.x <= xMin)
            {
                xMin = XY.x;
            }
            if (XY.y >= yMax)
            {
                yMax = XY.y;
            }
            if (XY.y <= yMin)
            {
                yMin = XY.y;
            }
        }

        Room.SetMinMax(new Vector2Int(xMin - 1, yMin - 1), new Vector2Int(xMax + 2, yMax + 2));

        return Room;
    }


    public bool TileCollider(int PosX, int PosY)
    {
        if (Walls.GetTile(new Vector3Int(PosX, PosY, 0)) != null || Doors.GetTile(new Vector3Int(PosX, PosY, 0)) != null)
        {
            return true;
        }
        return false;
    }

    public bool XYContains(Vector2Int check, List<Vector2Int> xyList)
    {

        for (int i = 0; i < xyList.Count; i++)
        {
            if (xyList[i] == check)
            {
                return true;
            }
        }
        
        return false;
    }

    public void LookAtChunk(Vector2Int Instance)
    {
        int InstanceX = Instance.x;
        int InstanceY = Instance.y;

        if (TileCollider(InstanceX, InstanceY))
        {
            return;
        }

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (!TileCollider(InstanceX + i, InstanceY + j))
                {
                    if (!XYContains(new Vector2Int(InstanceX + i, InstanceY + j), OpenXYList))
                    {
                                OpenXYList.Add(new Vector2Int(InstanceX + i, InstanceY + j));
                                XYList.Add(new Vector2Int(InstanceX + i, InstanceY + j));
                    }
                }
            }
        }
        if (OpenXYList.Contains(Instance))
        {
            OpenXYList.Remove(Instance);
        }
        else
        {
            OpenXYList.Clear();
        }

    }

}
