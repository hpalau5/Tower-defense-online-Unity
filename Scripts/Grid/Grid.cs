using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private float size = 1f;

    public static Grid instance;
    public Hashtable listaOcupados = new Hashtable();
    float maxXPosition;
    float maxZPosition;
    float minXPosition;
    float minZPosition;

    void Awake()
    {
        instance = this;
        maxXPosition = Mathf.Max(transform.position.x, transform.GetChild(0).transform.position.x);
        maxZPosition = Mathf.Max(transform.position.z, transform.GetChild(0).transform.position.z);
        minXPosition = Mathf.Min(transform.position.x, transform.GetChild(0).transform.position.x);
        minZPosition = Mathf.Min(transform.position.z, transform.GetChild(0).transform.position.z);
    }

    void Start()
    {
        RellenarOcupadosPorElCamino();
    }

    /*
    cogiendo la posicion del waypoint inicial, restando con el siguiente para saber en que direccion contar e ir sumando grid.size para añadir todos
    a parte si es de 3 de ancho, añadir tambien la otra coordenada -1 y +1 grid.size
    */
    //snapear waypoints en el editor
    void RellenarOcupadosPorElCamino()
    {
        for (int i = 0; i < Waypoints.waypoints.Length - 1; i++)
        {
            Vector3 point = Waypoints.waypoints[i + 1] - Waypoints.waypoints[i];
            if (Mathf.Abs(point.x) > Mathf.Abs(point.z))
            {
                float nextX = Mathf.Max(Waypoints.waypoints[i + 1].x, Waypoints.waypoints[i].x);
                float prevX = Mathf.Min(Waypoints.waypoints[i + 1].x, Waypoints.waypoints[i].x);
                float waypointCoord = prevX;
                while (nextX - waypointCoord >= 0)
                {
                    waypointCoord += size;
                    GetNearestPointOnGrid(new Vector3(waypointCoord, 0, Waypoints.waypoints[i].z));
                    GetNearestPointOnGrid(new Vector3(waypointCoord, 0, Waypoints.waypoints[i].z+size));
                    GetNearestPointOnGrid(new Vector3(waypointCoord, 0, Waypoints.waypoints[i].z-size));
                }
            }
            else
            {
                float nextZ = Mathf.Max(Waypoints.waypoints[i + 1].z, Waypoints.waypoints[i].z);
                float prevZ = Mathf.Min(Waypoints.waypoints[i + 1].z, Waypoints.waypoints[i].z);
                float waypointCoord = prevZ;
                while (nextZ - waypointCoord >= 0)
                {
                    waypointCoord += size;
                    GetNearestPointOnGrid(new Vector3 (Waypoints.waypoints[i].x, 0, waypointCoord));
                    GetNearestPointOnGrid(new Vector3(Waypoints.waypoints[i].x+size, 0, waypointCoord));
                    GetNearestPointOnGrid(new Vector3(Waypoints.waypoints[i].x-size, 0, waypointCoord));
                }
            }
        }
    }

    
    public bool CheckPositionIsFree(Vector3 position)
    {
        if(!CheckPositionIsInsideGrid(position))
            return false;

        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        if (!listaOcupados.ContainsKey(hashFunction(xCount, zCount)))
            return true;
        else
            return false;
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        if (!CheckPositionIsInsideGrid(position))
            return Vector3.zero;

        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3((float)xCount * size, 0, (float)zCount * size);

        result += transform.position;
        
        float hash = hashFunction(xCount,zCount);
        if (!listaOcupados.ContainsKey(hash))
        {
            listaOcupados.Add(hash, "");
            return result;
        }
        else
        {
            return Vector3.zero; 
        }  
    }
    bool CheckPositionIsInsideGrid(Vector3 pos)
    {
        return pos.x < maxXPosition && pos.x > minXPosition && pos.z < maxZPosition && pos.z > minZPosition;
    }
    float hashFunction (int x, int z)
    {
        return ((x + z) * (x + z + 1) / 2) + z;
    }

    public Vector3 DrawGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            0,
            (float)zCount * size);

        result += transform.position;

        if (!listaOcupados.ContainsKey(hashFunction(xCount, zCount)))
            return result;
        else
            return Vector3.zero;
    }
    //usa su propia posicion como punto para empezar el grid y la posicion del hijo como la esquina contraria
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = transform.position.x; x < transform.GetChild(0).transform.position.x; x += size)
        {
            for (float z = transform.position.z; z < transform.GetChild(0).transform.position.z; z += size)
            {
                Vector3 point = DrawGrid(new Vector3(x, 0f, z));
                if(point !=Vector3.zero)
                    Gizmos.DrawSphere(point, 0.1f);
            }

        }
    }
}


