using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTool : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public Transform projectileTool;

    protected virtual void Start()
    {
        projectileTool = GameObject.Find("Projectile Tool").transform;
    }

    protected virtual void Update()
    {
        if(Input.GetMouseButtonDown(0) && Level2_Manager.instance.gameOn == true)
        {
            var projectile = Instantiate(projectilePrefab, projectileTool.position, projectileTool.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectileTool.forward * projectileSpeed;
        }
    }
}
