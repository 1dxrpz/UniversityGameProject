using System.Collections.Generic;
using UnityEngine;

public class EnemyScr : MonoBehaviour
{
    public GameObject Bullet;
    public float Power;

    public GameObject[] CounOfBulets;

    public Transform player;
    public float distDetection = 3;
    public float move_speed;
    public float rotation_speed;

    public List<Vector3> posForMove = new List<Vector3>();

    private bool detect = false;
    
    void Update()
    {

        if (Vector3.Distance(player.position, transform.position) <= 8.0f)
        {
            detect = true;
            //if (posForMove.Count == 1)
            //    posForMove.RemoveAt(0);
        }

        if (detect)
        {
            if(Vector3.Distance(player.position, transform.position) < 5f)
            {
                var look_dir = (player.position - transform.position).normalized;
                if (Quaternion.LookRotation(look_dir).eulerAngles.y != transform.rotation.eulerAngles.y)
                {
                    //look_dir.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look_dir), rotation_speed * 5 * Time.deltaTime);
                }
                else
                {
                    Attack();
                }
        }
            else
            {
                var look_dir = player.position - transform.position;
                look_dir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
                transform.position += transform.forward * move_speed * Time.deltaTime;
            }
        }

        if (Vector3.Distance(player.position, transform.position) > 8.0f)
        {
            detect = false;
            JustMove();
        }
    }

    private void Attack()
    {
        CounOfBulets = GameObject.FindGameObjectsWithTag("Bulet");

        if (CounOfBulets.Length < 1)
        {
            var look_dir = (player.position - transform.position).normalized;
            //look_dir.y = 0;

            GameObject b = Instantiate(Bullet, base.transform.position, base.transform.rotation);
            b.tag = "Bulet";

            b.GetComponent<Rigidbody>().AddForce(look_dir * Power, ForceMode.Impulse);
        }
    }

    private void JustMove()
    {
        if (posForMove.Count == 0)
        {
            var randomPointZ = Random.Range(-5, 5);
            var randomPointX = Random.Range(-5, 5);

            posForMove.Add(new Vector3(Random.Range(transform.position.x, transform.position.x + randomPointX), 0, 
                Random.Range(transform.position.z, transform.position.z + randomPointZ)));
        }

        var look_dir = posForMove[0] - transform.position;
        look_dir.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
        transform.position += transform.forward * move_speed * Time.deltaTime;

        if(CheckPointTrue(transform.position, posForMove[0]))
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
