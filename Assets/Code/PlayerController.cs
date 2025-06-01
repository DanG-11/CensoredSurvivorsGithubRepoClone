using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 controllerInput;
    public float speed = 5f;
    Rigidbody rb;
    public List<GameObject> enemies;
    public GameObject Gun;
    public GameObject BulletSpawn;
    public GameObject bulletPreFab;
    public GameObject SwordHandle;

    LevelManager lm;
    void Start()
    {
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        rb = GetComponent<Rigidbody>();
        enemies = new List<GameObject>();
        InvokeRepeating("Shoot", 0, 2);
    }

    void Update()
    {
        controllerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Vector3 movementVector = new Vector3(controllerInput.x, 0, controllerInput.y);
        //transform.Translate(movementVector * Time.deltaTime * speed);

        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        enemies = enemies.OrderBy(enemy => Vector3.Distance(enemy.transform.position, transform.position)).ToList();

        if (enemies.Count > 0 && Vector3.Distance(transform.position, enemies[0].transform.position) < 2f)
        {
            SwordHandle.SetActive(true);
            SwordHandle.transform.Rotate(0, 5f, 0);
        }
        else
        {
            SwordHandle.SetActive(false);
        }


    }
    private void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(controllerInput.x, 0, controllerInput.y);
        Vector3 targetPosition = transform.position + movementVector * Time.fixedDeltaTime * speed;
        rb.MovePosition(targetPosition);
    }
    void Shoot()
    {

        if (enemies.Count > 0)
        {
            Gun.transform.LookAt(enemies[0].transform);
            GameObject bullet = Instantiate(bulletPreFab, BulletSpawn.transform.position, Gun.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(Gun.transform.forward * 1000);
            //Destroy(enemies[0]);
            Debug.Log("Pif paf!");
            Destroy(bullet, 2f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lm.ReducePlayerHealth(1);
        }
    }
}
