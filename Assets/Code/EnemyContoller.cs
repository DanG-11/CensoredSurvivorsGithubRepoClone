using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    GameObject player;
    public float speed = 4f;

    public TextMeshProUGUI EnemyHpText;
    public int EnemyHp = 100;
    public Canvas canvas;
    LevelManager lm;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.forward = Camera.main.transform.forward;
    }
    private void FixedUpdate()
    {
        if (canvas != null && Camera.main != null)
        {
            canvas.transform.LookAt(Camera.main.transform);
            canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - Camera.main.transform.position);
        }

        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        canvas.GetComponentInChildren<TextMeshProUGUI>().text = EnemyHp.ToString();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {

            EnemyHp -= 30;
            CheckDead();
            //Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            EnemyHp -= 20;
            CheckDead();
        }
        if (other.gameObject.CompareTag("Star"))
        {
            EnemyHp -= 50;
            CheckDead();
        }
    }
    public void CheckDead()
    {
        if (EnemyHp <= 0)
        {
            lm.AddPoints(1);
            Destroy(gameObject);
        }
    }
}
