using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class OrderAux
{
    // Start is called before the first frame update
    public static Collider MinElement(this IEnumerable<Collider> items, Func<Collider, float> f)
    {
        return items.Aggregate(
          new { Min = float.MaxValue, Item = default(Collider) },
          (state, el) => {
              var current = f(el);
              if (current < state.Min)
                  return new { Min = current, Item = el };
              else
                  return state;
          }).Item;
    }
	public static Collider MaxElement(this IEnumerable<Collider> items, Func<Collider, float> f)
    {
        return items.Aggregate(
          new { Max = float.MinValue, Item = default(Collider) },
          (state, el) => {
              var current = f(el);
              if (current > state.Max)
                  return new { Max = current, Item = el };
              else
                  return state;
          }).Item;
    }
	public static GameObject MaxByGameObject(this IEnumerable<GameObject> items, Func<GameObject, float> f)
    {
        return items.Aggregate(
          new { Min = float.MaxValue, Item = default(GameObject) },
          (state, el) => {
              var current = f(el);
              if (current < state.Min)
                  return new { Min = current, Item = el };
              else
                  return state;
          }).Item;
    }
	
	public static Transform MaxByTransform(this IEnumerable<Transform> items, Func<Transform, float> f)
    {
        return items.Aggregate(
          new { Min = float.MaxValue, Item = default(Transform) },
          (state, el) => {
              var current = f(el);
              if (current < state.Min)
                  return new { Min = current, Item = el };
              else
                  return state;
          }).Item;
    }
	
	
    public static Collider2D MaxBy2D(this IEnumerable<Collider2D> items, Func<Collider2D, float> f)
    {
        return items.Aggregate(
          new { Min = float.MaxValue, Item = default(Collider2D) },
          (state, el) => {
              var current = f(el);
              if (current < state.Min)
                  return new { Min = current, Item = el };
              else
                  return state;
          }).Item;
    }
}
