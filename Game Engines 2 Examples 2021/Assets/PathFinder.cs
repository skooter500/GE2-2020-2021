using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
    
public class PathFinder : MonoBehaviour
{
    float voxelSize = 5.0f;
    public string message = "";
    

    public bool isThreeD = false;


    Dictionary<Vector3, Node> open = new Dictionary<Vector3, Node>(20000);
    //PriorityQueue<Node> openPQ = new PriorityQueue<Node>();
    //List<Node> openList = new List<Node>();

    Dictionary<Vector3, Node> closed = new Dictionary<Vector3, Node>(20000);
   
    Vector3 start, end;

    bool smooth = false;

    public bool Smooth
    {
        get { return smooth; }
        set { smooth = value; }
    }

    Vector3 PositionToVoxel(Vector3 v)
    {
        Vector3 ret = new Vector3();
        ret.x = ((int)(v.x / voxelSize)) * voxelSize;
        ret.y = ((int)(v.y / voxelSize)) * voxelSize;
        ret.z = ((int)(v.z / voxelSize)) * voxelSize;
        return ret;
    }

    public Path FindPath(Vector3 start, Vector3 end)
    {
        long oldNow = DateTime.Now.Ticks;
        bool found = false;
        this.end = PositionToVoxel(start); // end refers to start
        this.start = PositionToVoxel(end); // start refers to end

        open.Clear();
        closed.Clear();

        Node first = new Node();
        first.f = first.g = first.h = 0.0f;
        first.pos = this.start;
        open[this.start] = first;
        //openList.Add(first);
        //openPQ.Enqueue(first);

        Node current = first;
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        int maxSize = 0;
        stopwatch.Start();
        bool timeout = false;
        while (open.Count > 0)
        {
            if (stopwatch.ElapsedMilliseconds > 5000)
            {
                timeout = true;
                break;
            }
            if (open.Count > maxSize)
            {
                maxSize = open.Count;
            }
            //current = openPQ.Dequeue();
            //float min = current.f;
            
            // Get the top of the q
            float min = float.MaxValue;
            foreach (Node node in open.Values)
            {
                if (node.f < min)
                {
                    current = node;
                    min = node.f;
                }
            }
            
            if (current.pos.Equals(this.end))
            {
                found = true;
                break;
            }
            addAdjacentNodes(current);
            open.Remove(current.pos);
            //openList.Remove(current);
            closed[current.pos] = current;
        }
        Path path = GetComponent<Path>();
        if (found)
        {
            while (!current.pos.Equals(this.start))
            {
                path.waypoints.Add(current.pos);
                current = current.parent;
            }
            path.waypoints.Add(current.pos);
            message = "A * took: " + stopwatch.ElapsedMilliseconds + " milliseconds. Open list: " + maxSize;

        }
        else
        {
            if (timeout)
            {
                message = "A* timed out after 5 seconds. Open list: " + maxSize;
            }
            else
            {
                message = "No path found. Open list: " + maxSize;
            }
            
        }
        if (smooth)
        {
            SmoothPath(path);
        }
        return path;
    }

    private void addAdjacentNodes(Node current)
    {



        // Forwards
        Vector3 pos;
        pos.x = current.pos.x;
        pos.y = current.pos.y;
        pos.z = current.pos.z + voxelSize;
        AddIfValid(pos, current);

        // Forwards right
        pos.x = current.pos.x + voxelSize;
        pos.y = current.pos.y;
        pos.z = current.pos.z + voxelSize;
        AddIfValid(pos, current);

        // Right
        pos.x = current.pos.x + voxelSize;
        pos.y = current.pos.y;
        pos.z = current.pos.z;
        AddIfValid(pos, current);

        // Backwards Right
        pos.x = current.pos.x + voxelSize;
        pos.y = current.pos.y;
        pos.z = current.pos.z - voxelSize;
        AddIfValid(pos, current);

        // Backwards
        pos.x = current.pos.x;
        pos.y = current.pos.y;
        pos.z = current.pos.z - voxelSize;
        AddIfValid(pos, current);

        // Backwards Left
        pos.x = current.pos.x - voxelSize;
        pos.y = current.pos.y;
        pos.z = current.pos.z - voxelSize;
        AddIfValid(pos, current);

        // Left
        pos.x = current.pos.x - voxelSize;
        pos.y = current.pos.y;
        pos.z = current.pos.z;
        AddIfValid(pos, current);

        // Forwards Left
        pos.x = current.pos.x - voxelSize;
        pos.y = current.pos.y;
        pos.z = current.pos.z + voxelSize;
        AddIfValid(pos, current);

        if (isThreeD)
        {
            // Above in front row
            pos.x = current.pos.x - voxelSize;
            pos.y = current.pos.y + voxelSize;
            pos.z = current.pos.z - voxelSize;
            AddIfValid(pos, current);

            pos.x = current.pos.x;
            pos.y = current.pos.y + voxelSize;
            pos.z = current.pos.z - voxelSize;
            AddIfValid(pos, current);

            pos.x = current.pos.x + voxelSize;
            pos.y = current.pos.y + voxelSize;
            pos.z = current.pos.z - voxelSize;
            AddIfValid(pos, current);

            
            // Above middle row
            pos.x = current.pos.x - voxelSize;
            pos.y = current.pos.y + voxelSize;
            pos.z = current.pos.z;
            AddIfValid(pos, current);

            pos.x = current.pos.x;
            pos.y = current.pos.y + voxelSize;
            pos.z = current.pos.z;
            AddIfValid(pos, current);

            pos.x = current.pos.x + voxelSize;
            pos.y = current.pos.y + voxelSize;
            pos.z = current.pos.z;
            AddIfValid(pos, current);

            // Above back row
            pos.x = current.pos.x - voxelSize;
            pos.y = current.pos.y + voxelSize;
            pos.z = current.pos.z + voxelSize;
            AddIfValid(pos, current);

            pos.x = current.pos.x;
            pos.y = current.pos.y + voxelSize;
            pos.z = current.pos.z + voxelSize;
            AddIfValid(pos, current);

            pos.x = current.pos.x + voxelSize;
            pos.y = current.pos.y + voxelSize;
            pos.z = current.pos.z + voxelSize;
            AddIfValid(pos, current);

            // Below in front row
            pos.x = current.pos.x - voxelSize;
            pos.y = current.pos.y - voxelSize;
            pos.z = current.pos.z - voxelSize;
            AddIfValid(pos, current);

            pos.x = current.pos.x;
            pos.y = current.pos.y - voxelSize;
            pos.z = current.pos.z - voxelSize;
            AddIfValid(pos, current);

            pos.x = current.pos.x + voxelSize;
            pos.y = current.pos.y - voxelSize;
            pos.z = current.pos.z - voxelSize;
            AddIfValid(pos, current);

            // Below middle row
            pos.x = current.pos.x - voxelSize;
            pos.y = current.pos.y - voxelSize;
            pos.z = current.pos.z;
            AddIfValid(pos, current);

            pos.x = current.pos.x;
            pos.y = current.pos.y - voxelSize;
            pos.z = current.pos.z;
            AddIfValid(pos, current);

            pos.x = current.pos.x + voxelSize;
            pos.y = current.pos.y - voxelSize;
            pos.z = current.pos.z;
            AddIfValid(pos, current);

            // Below back row
            pos.x = current.pos.x - voxelSize;
            pos.y = current.pos.y - voxelSize;
            pos.z = current.pos.z + voxelSize;
            AddIfValid(pos, current);

            pos.x = current.pos.x;
            pos.y = current.pos.y - voxelSize;
            pos.z = current.pos.z + voxelSize;
            AddIfValid(pos, current);

            pos.x = current.pos.x + voxelSize;
            pos.y = current.pos.y - voxelSize;
            pos.z = current.pos.z + voxelSize;
            AddIfValid(pos, current);
                
        }

    }

    private void AddIfValid(Vector3 pos, Node parent)
    {
        if ((!RayTrace(parent.pos, pos)))
        {
            if (!closed.ContainsKey(pos))
            {
                if (!open.ContainsKey(pos))
                {
                    Node node = new Node();
                    node.pos = pos;
                    node.g = parent.g + cost(node.pos, parent.pos);
                    node.h = heuristic(pos, end);
                    node.f = node.g + node.h;
                    node.parent = parent;
                    //openPQ.Enqueue(node);
                    open[pos] = node;
                    //openList.Add(node);
                }
                else
                {
                    // Edge relaxation?
                    Node node = open[pos];
                    float g = parent.g + cost(node.pos, parent.pos);
                    if (g < node.g)
                    {
                        node.g = g;
                        node.f = node.g + node.h;
                        node.parent = parent;
                    }
                }
            }
        }
    }

    public void SmoothPath(Path path)
    {
        List<Vector3> wayPoints = path.waypoints;

        if (wayPoints.Count < 3)
        {
            return;
        }

        int current;
        int middle;
        int last;

        current = 0;
        middle = current + 1;
        last = current + 2;

        while (last != wayPoints.Count)
        {

            Vector3 point0, point2;

            point0 = wayPoints[current];
            point2 = wayPoints[last];
            point0.y = 0;
            point2.y = 0;
            if ((RayTrace(point0, point2)))
            {
                current++;
                middle++;
                last++;

            }
            else
            {
                wayPoints.RemoveAt(middle);
            }
        }
    }

    private float heuristic(Vector3 v1, Vector3 v2)
    {
        return 10.0f * (Math.Abs(v2.x - v1.x) + Math.Abs(v2.y - v1.y) + Math.Abs(v2.z - v1.z));
    }

    private float cost(Vector3 v1, Vector3 v2)
    {
        int dist = (int)Math.Abs(v2.x - v1.x) + (int)Math.Abs(v2.y - v1.y) + (int)Math.Abs(v2.z - v1.z);
        return (dist == 1) ? 10 : 14;
    }

    public LayerMask obstaclesLayerMask;

    bool RayTrace(Vector3 start, Vector3 end)
    {
        Vector3 toEnd = end - start;
        return Physics.Raycast(start, toEnd, toEnd.magnitude, obstaclesLayerMask);
    }
}
