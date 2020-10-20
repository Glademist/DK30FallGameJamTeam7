using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{

    public Tilemap Colliders;
    public List<int> Distances;
    public List<Vector2Int> OpenXYList;
    public List<Vector2Int> ClosedXYList;
    public List<Vector2Int> XYList;
    public List<Vector2Int> XYParent;
    public List<Vector2Int> Path;
    public int XDistance;
    public int YDistance;
    public int TargetX;
    public int TargetY;
    public Vector2Int NextInstance;
    public Vector2Int target;
    public Vector2Int origin;

    public bool TileCollider(int PosX, int PosY)
    {
        if (Colliders.GetTile(new Vector3Int(PosX, PosY, 0)) != null)
        {
            return true;
        }
        return false;
    }

    public List<Vector2Int> LoadAStar(Vector2Int Origin, Vector2Int Target)
    {
        origin = Origin;
        target = Target;
        if (OpenXYList.Count <= 0)
        {
            Debug.Log("1st");
            ClearProcessedLists();
            ClearLists();
            LoadAStarChunk(Origin, Target, Origin);
            NextInstance = OpenXYList[GetLowestDist()];
        }
        while (!(XYContains(Target.x, Target.y, ClosedXYList)))
        {
            try
            {
                NextInstance = OpenXYList[GetLowestDist()];
                ClosedXYList.Add(OpenXYList[GetLowestDist()]);
                ClearOpenLists(GetLowestDist());
                LoadAStarChunk(Origin, Target, NextInstance);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                Debug.Log(GetLowestDist() + ", " + e);
                Path.Clear();
                return Path;
            }
        }
        if ((XYContains(Target.x, Target.y, ClosedXYList)))
        {
            Debug.Log("AStar Successful");
            BuildPath();
            ClearOpenLists();
        }
        else
        {
            Debug.Log("Error With AStar");
        }

        return Path;
    }

    public List<Vector2Int> BuildPath()
    {
        Path.Clear();
        Path.Add(NextInstance);
        float i = 0;
        while (.5 > i && NextInstance != origin)
        {
            Path.Add(XYParent[XYList.IndexOf(NextInstance)]);
            NextInstance = XYParent[XYList.IndexOf(NextInstance)];
            i += 0.01f;
        }


        return Path;
    }
    

    public int GetLowestDist()
    {
        int LowestDist = int.MaxValue;
        int DIndex = 0;

        for (int a = 0; a < Distances.Count; a++)
        {
            if (Distances[a] <= LowestDist)
            {
                if (XYContains(XYList[a].x, XYList[a].y, OpenXYList))
                {
                    LowestDist = Distances[a];
                    DIndex = OpenXYList.IndexOf(XYList[a]);
                }
                else
                {
                    //Debug.Log(XYContains(XYList[a], ClosedXYList) + " " + XYList[a] + " " + Distances[a]);
                }
            }
        }

        return DIndex;
    }

    public void ClearLists(int index = -1)
    {
        if (index <= -1)
        {
            XYList.Clear();
            XYParent.Clear();
            Distances.Clear();
        }
        else if (OpenXYList.Count >= index)
        {
            XYList.RemoveAt(index);
            XYParent.RemoveAt(index);
            Distances.RemoveAt(index);
        }
        else
        {
            Debug.Log("There was an error with AStar Clear Lists");
        }
    }

    public void ClearOpenLists(int index = -1)
    {
        if (index <= -1)
        {
            OpenXYList.Clear();
        }
        else if (OpenXYList.Count >= index)
        {
            OpenXYList.RemoveAt(index);
        }
        else
        {
            Debug.Log("There was an error with AStar Clear Lists");
        }
    }

    public void ClearProcessedLists(int index = -1)
    {
        if (index <= -1)
        {
            ClosedXYList.Clear();
        }
        else if (ClosedXYList.Count >= index)
        {
            ClosedXYList.RemoveAt(index);
        }
        else
        {
            Debug.Log("There was an error with AStar Clear Lists");
        }
    }

    public void LoadAStarChunk(Vector2Int Origin, Vector2Int Target, Vector2Int Instance)
    {

        int OriginX = Origin.x;
        int OriginY = Origin.y;
        int InstanceX = Instance.x;
        int InstanceY = Instance.y;
        int TargetX = Target.x;
        int TargetY = Target.y;


        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (!TileCollider(InstanceX + i, InstanceY + j))
                {
                    if (!XYContains(InstanceX + i, InstanceY + j, OpenXYList))
                    {
                        if (!XYContains(InstanceX + i, InstanceY + j, ClosedXYList))
                        {
                            if ((i == 1 || i == -1) && ((j == 1 || j == -1)))
                            {
                                if (CheckAdjacent(Instance))
                                {
                                    OpenXYList.Add(new Vector2Int(InstanceX + i, InstanceY + j));
                                    XYList.Add(new Vector2Int(InstanceX + i, InstanceY + j));
                                    XYParent.Add(Instance);
                                    Distances.Add(GetDistance(Target, InstanceX + i, InstanceY + j) + GetDistance(Origin, InstanceX + i, InstanceY + j));
                                }
                            }
                            else
                            {
                                OpenXYList.Add(new Vector2Int(InstanceX + i, InstanceY + j));
                                XYList.Add(new Vector2Int(InstanceX + i, InstanceY + j));
                                XYParent.Add(Instance);
                                Distances.Add(GetDistance(Target, InstanceX + i, InstanceY + j) + GetDistance(Origin, InstanceX + i, InstanceY + j));
                            }
                        }
                        else
                        {
                            //Debug.Log("SkippingTile " + (OriginX + i) + " " + (OriginY + j) + " because Processed contains");
                        }
                    }
                    else
                    {
                        //   Debug.Log("SkippingTile " + (OriginX + i) + " " + (OriginY + j) + " because Unprocessed contains");
                    }
                }
                else
                {
                    //  Debug.Log("SkippingTile " + (OriginX + i) + " " + (OriginY + j) + " because of collider");
                }
            }
        }

    }

    public bool CheckAdjacent(Vector2Int Instance)
    {

        if (!TileCollider(Instance.x +1, Instance.y) && !TileCollider(Instance.x, Instance.y +1) && !TileCollider(Instance.x -1, Instance.y) && !TileCollider(Instance.x, Instance.y -1))
        {
            return true;
        }
        return false;
    }


    public bool XYContains(int x, int y, List<Vector2Int> xyList)
    {

        for (int i = 0; i < xyList.Count; i++)
        {
            if (xyList[i].x == x && xyList[i].y == y)
            {
                return true;
            }
        }


        return false;
    }
    public int GetDistance(Vector2Int Target, int OriginX, int OriginY)
    {
        int Distance = 0;
        TargetX = Target.x;
        TargetY = Target.y;
        XDistance = Mathf.Abs(TargetX - OriginX);
        YDistance = Mathf.Abs(TargetY - OriginY);
        if (XDistance >= YDistance)
        {
            Distance += Mathf.Abs(XDistance * 10);
            Distance += Mathf.Abs(YDistance * 4);
        }
        else if (YDistance >= XDistance)
        {
            Distance += Mathf.Abs(YDistance * 10);
            Distance += Mathf.Abs(XDistance * 4);
        }

        return Distance;
    }

}
