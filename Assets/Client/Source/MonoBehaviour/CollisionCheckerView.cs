using Client;
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckerView : MonoBehaviour
{
    // auto-injected fields.
    public EcsWorld ecsWorld { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = ecsWorld.NewEntity();

        var hitPool = ecsWorld.GetPool<HitComponent>();
        hitPool.Add(hit);
        ref var hitComponent = ref hitPool.Get(hit);

        hitComponent.first = transform.root.gameObject;
        hitComponent.other = collision.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy") && !gameObject.CompareTag("Player") && !gameObject.CompareTag("PhysicsUnDisable"))
        {
            gameObject.SetActive(false);
            //Debug.Log("here");
        }
        
        //Debug.Log("hello");
        var hit = ecsWorld.NewEntity();

        var hitPool = ecsWorld.GetPool<HitComponent>();
        hitPool.Add(hit);
        ref var hitComponent = ref hitPool.Get(hit);

        hitComponent.first = transform.root.gameObject;
        hitComponent.other = other.gameObject;
    }


}
