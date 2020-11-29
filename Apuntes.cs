using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apuntes : MonoBehaviour
{


	/*  tiempoRestanteParaPoderUsarla;
	if tiempoRestanteParaPoderUsarla>0 que pasa? peta porque es null? o lo coge como 0

     * Cosas a hacer
     * Menus
	 * Interfaz ingame (dinero, datos torre, upgrades, datos wave, pantalla fin...)
	 * Arrastrar torres y snap to grid
	 * Heroe (copiarse del otro td, de las cabañas que sacan soldados)
	 * Talentos/mejores tipo infinitoide, lo mismo para los heroes
     * Añadir mas torres (sueltan soldados, ralentizar, area, tipo lanzallamas, invisibles, atraviesan...) 
	 * Añadir mas enemigos (corren, vuelan, con armadura, se hacen tp, se dividen al morir...) y algun boss con habilidad como por ejemplo que cada x tiemp suelte minions
	 * Online, la primera version puede fiarse de los datos que envien los clientesy el server simplemente difundirlos
	 * PENSAR QUE DEBERIA TENER PARA SER DIVERTIDO, JUGARLO Y VER QUE LE FALTARIA
	 *
	 *
	 *
Ideas
Podriamos mejorar al heroe ingame tambien y que tengas que decidir si gastar dinero en torres o en el heroe
	A parte una aldea tipo clash de menu principal? que hagas algo ahi para mejorar cosas
	Tipos torres:
	torre veneno, veneno en area, fuego,cono de fuego, caldero en x area, invisibles, mejora torres cercanas, rayo que rebota, atraviesa todo,
	multiples proyectiles, tira bombas donde le marques, dispara hacia donde le digas


	/*
	 * COSAS A SABER:
	 * Cuidado al cambiar un metodo virtual en el padre que algunos hijos usan ese mimso copiado, cambiar tambien en los hijos
	 * Cuando no tiene enemigo y ha pasado el tiempo entre ataques, busca un enemigo nuevo por update, si no encuentra le añadimos 0.1 al tiempo que falta para atacar
	 * Asi buscara 10 veces por segundo y no 60 (a lo mejor se convierte en un 0.2, testear fps)
	 * 
	 * Layer 8 enemigos, 9 torres, 10 balas, hacer que puedan colisionar entre ellos los que deban hacerlo
	 * 
	 *Guia para meter nuevas torres, mapas, heroes y hechizos
	 *
	 *Usar awake para cosas propias o sin dependencias y start para lo demas
	 */
}
