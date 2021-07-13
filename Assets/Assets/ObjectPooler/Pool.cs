using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPool",menuName ="ObjectPool")]
public class Pool : ScriptableObject
{
    public int size;
    public GameObject prefab;
    Queue<GameObject> queue;
    bool instantiated = false;

    public void Instantiate()
    {
        queue = new Queue<GameObject>();
        Debug.Log("instantiated");
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }
        instantiated = true;
    }

    public GameObject Spawn(Vector3 position)
    {
        if (queue==null)
            Instantiate();

        GameObject spawn = queue.Dequeue();
        spawn.SetActive(true);

        spawn.transform.position = position;

        queue.Enqueue(spawn);
        return spawn;
    }
    public GameObject Spawn(Vector3 position,Quaternion rotation)
    {
        if (!instantiated)
            Instantiate();

        GameObject spawn = queue.Dequeue();
        spawn.SetActive(true);

        spawn.transform.position = position;
        spawn.transform.rotation = rotation;

        queue.Enqueue(spawn);
        return spawn;
    }
    public GameObject Spawn(Vector3 position, Quaternion rotation,Vector3 velocity)
    {
        if (!instantiated)
            Instantiate();

        GameObject spawn = queue.Dequeue();
        spawn.SetActive(true);

        spawn.transform.position = position;
        spawn.transform.rotation = rotation;

        Rigidbody rb = spawn.GetComponent<Rigidbody>();
        if(rb!=null)
            rb.velocity = velocity;

        queue.Enqueue(spawn);
        return spawn;
    }
}


