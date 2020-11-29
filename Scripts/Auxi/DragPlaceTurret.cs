using System.Collections;
using UnityEngine;
/*
Al hacer click sin soltar crear torre encima del boton?
Cuando la dejes en un sitio cuyo lugar mas cerca este libre, poner area de la torre normal y snapear al soltar
Si esta ocupado o no puede estar ahi poner area roja y no snapear al soltar
Si la soltamos cerca del menu, desaparece la torre
*/
public class DragPlaceTurret : MonoBehaviour
{
    public SpriteRenderer rangeRenderer;

    bool dragging = true;
    float distance;

    private void Start()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    void OnMouseUp()
    {
        if (dragging)
        {
            if (Grid.instance.CheckPositionIsFree(GetPosition()))
            {
                if (GameManager.instance.PlaceTower(gameObject.GetComponent<TowerFather>().towerCost))
                {
                    Vector3 snappedPosition = Grid.instance.GetNearestPointOnGrid(GetPosition());
                    if (snappedPosition != Vector3.zero)
                    {
                        transform.position = snappedPosition;
                        gameObject.GetComponent<TowerFather>().towerPlaced = true;
                        gameObject.GetComponent<TowerFather>().EnableRangeSprite(false);

                        TurretSpawner.instance.turretsSpawned.Add(gameObject);
                        Destroy(this);
                    }
                }
            }
        }
    }
    //aqui deberia verse y cambiar de color el area, al ponerse desactivar el area
    void Update()
    {
        if (dragging)
        {
            Vector3 pos = GetPosition();
            transform.position = pos;
            if (Grid.instance.CheckPositionIsFree(pos))
            {
                rangeRenderer.material.color = Color.green;
            }
            else
            {
                rangeRenderer.material.color = Color.red;
            }
        }
    }
    Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        return new Vector3(rayPoint.x, 0, rayPoint.z);
    }
}