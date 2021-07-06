using System.Collections.Generic;
using UnityEngine;

public class EnemyScr : MonoBehaviour
{
    public GameObject Bullet;
    public float Power;

    public List<GameObject> bulets = new List<GameObject>();


    public Transform player;
    public Transform enemy;
    public float distDetection = 3;
    public float move_speed;
    public float rotation_speed;

    public List<Vector3> posForMove = new List<Vector3>();

    private bool detect = false;
    
    void Update()
    {
        //Debug.Log(Vector3.Distance(player.position, enemy.position).ToString());

        if (Vector3.Distance(player.position, enemy.position) <= 4.0f)
        {
            detect = true;
            //if(posForMove.Count == 1)
            //    posForMove.RemoveAt(0);
        }

        if (detect)
        {
            if(Vector3.Distance(player.position, enemy.position) < 2f)
            {
                Attack();
            }
            else
            {
                var look_dir = player.position - enemy.position;
                look_dir.y = 0;
                enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
                enemy.position += enemy.forward * move_speed * Time.deltaTime;
            }
        }

        if (Vector3.Distance(player.position, enemy.position) > 4.0f)
        {
            detect = false;
            //JustMove();
        }
    }

    private void Attack()
    {
        if (bulets.Count == 0)
        {
            var look_dir = player.position - enemy.position;
            look_dir.y = 0;
            var buletRotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir), 3);

            GameObject b = Instantiate(Bullet, transform.position, buletRotation);
            b.transform.localScale.Set(0.01f, 0.01f, 0.01f);
            bulets.Add(b);
            b.GetComponent<Rigidbody>().AddForce(enemy.forward * Power, ForceMode.Impulse);
        }
    }

    private void JustMove()
    {
        if (posForMove.Count == 0)
        {
            var randomPointZ = Random.Range(-5, 5);
            var randomPointX = Random.Range(-5, 5);

            posForMove.Add(new Vector3(Random.Range(enemy.position.x, enemy.position.x + randomPointX), 0, 
                Random.Range(enemy.position.z, enemy.position.z + randomPointZ)));
        }

        var look_dir = posForMove[0] - enemy.position;
        look_dir.y = 0;
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
        enemy.position += enemy.forward * move_speed * Time.deltaTime;

        if(CheckPointTrue(enemy.position, posForMove[0]))
        {
            posForMove.RemoveAt(0);
        }
    }

    private bool CheckPointTrue(Vector3 curentPos, Vector3 posToDo)
    {
        if( (posToDo.x + 1 > curentPos.x) && (posToDo.x - 1 < curentPos.x) )
        {
            if( (posToDo.z + 1 > curentPos.z) && (posToDo.z - 1 < curentPos.z))
            {
                return true;
            }
        }
        return false;
    }
}