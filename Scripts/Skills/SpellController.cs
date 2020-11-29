using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    //deberia ser como las torres, arrastrarlo y donde lo sueltes cae pero sin comprobar nada mas
    public GameObject spell1;
    public float tiempoRestanteParaPoderUsarla = 0;

    //cambiar
    public void Update()
    {
        if (tiempoRestanteParaPoderUsarla > 0)
        {
            tiempoRestanteParaPoderUsarla -= Time.deltaTime;
        }
    }
    public void UseSpell()
    {
        if (tiempoRestanteParaPoderUsarla <= 0)
        {
            tiempoRestanteParaPoderUsarla = spell1.GetComponent<SpellFather>().cd;
            Instantiate(spell1, new Vector3(0, 0, 0), Quaternion.identity).
            GetComponent<SpellFather>().SetTargetPosition(new Vector3(10, 10, 10));
        }
    }
}
