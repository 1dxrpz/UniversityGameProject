using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScr : MonoBehaviour
{
    private NavMeshAgent enemyBot;

    [Header("Игрок")]
    public GameObject Player;

    [Header("Тип патрона")]
    public GameObject Bullet;
    [Header("Сила полёта патрона")]
    public float Power;

    [Header("Колво патронов, и какие")]
    public GameObject[] CounOfBulets;

    [Header("Дистанция детекции")]
    public float distDetection;
    [Header("Дистанция аттаки")]
    public float distAttack;
    [Header("Скорость передвижения")]
    public float move_speed;
    [Header("Скорость поворота")]
    public float rotation_speed;

    [Header("Колво позиций, и какие")]
    public List<Vector3> posForMove = new List<Vector3>();

    private bool detect = false;

    private void Start()
    {
        enemyBot = GetComponent<NavMeshAgent>();
        enemyBot.speed = move_speed;
    }

    void Update()
    {
        bool underFloor = Player.GetComponent<PlayerController>().underFloor;

        if ((Vector3.Distance(Player.transform.position, transform.position) <= distDetection) && !underFloor)
            detect = true;
        else
            detect = false;

        //if (underFloor && detect == true)
        //{
        //    detect = false;

        //    if (posForMove.Count == 1)
        //        posForMove.RemoveAt(0);
        //    posForMove.Add(player.position);

        //    JustMove();
        //}

        if (detect)
        {
            MoveOrAttack();
        }

        if (!detect)
        {
            //JustMove();
            enemyBot.isStopped = true;
        }
    }

    private void MoveOrAttack()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        if (Vector3.Distance(Player.transform.position, transform.position) < distAttack)
        {
            var look_dir = (Player.transform.position - transform.position).normalized;
            //if (Quaternion.LookRotation(look_dir).eulerAngles.y != transform.rotation.eulerAngles.y)
            if ((Quaternion.LookRotation(look_dir).eulerAngles.y + 0.5f >= transform.rotation.eulerAngles.y) &&
                (Quaternion.LookRotation(look_dir).eulerAngles.y - 0.5f <= transform.rotation.eulerAngles.y))
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, transform.forward * 4f);
                if (!Physics.Raycast(ray, out hit))
                {
                    enemyBot.isStopped = true;
                    Attack();
                }
            }
            else
            {
                look_dir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look_dir), rotation_speed * 3 * Time.deltaTime);
            }
        }
        else
        {
            //var look_dir = Player.transform.position - transform.position;
            //look_dir.y = 0;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
            //transform.position += transform.forward * move_speed * Time.deltaTime;
            enemyBot.isStopped = false;
            enemyBot.destination = Player.transform.position;
        }
    }

    private void Attack()
    {
        CounOfBulets = GameObject.FindGameObjectsWithTag("Bulet");

        if (CounOfBulets.Length < 1)
        {
            var look_dir = (Player.transform.position - transform.position).normalized;
            //look_dir.y = 0;

            GameObject b = Instantiate(Bullet, base.transform.position, base.transform.rotation);
            b.tag = "Bulet";

            b.GetComponent<Rigidbody>().AddForce(look_dir * Power, ForceMode.Impulse);
        }
    }
}

//    private void JustMove()
//    {
//        if (posForMove.Count == 0)
//        {
//            float randomPointZ = Random.Range(-5f, 5f);
//            float randomPointX = Random.Range(-5f, 5f);

//            posForMove.Add(new Vector3(Random.Range(transform.position.x, transform.position.x + randomPointX), 0,
//                Random.Range(transform.position.z, transform.position.z + randomPointZ)));

//            //if (GetComponent<NavMeshAgent>().Warp(a))
//            //    posForMove.Add(a);
//        }
//        else
//        {
//            //var look_dir = posForMove[0] - transform.position;
//            //look_dir.y = 0;
//            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look_dir), rotation_speed * Time.deltaTime);
//            //transform.position += transform.forward * move_speed * Time.deltaTime;

//            enemyBot.destination = posForMove[0];

//            //if (CheckPointTrue(transform.position, posForMove[0]))
//            //{
//            //    posForMove.RemoveAt(0);
//            //}
//            if (GetComponent<NavMeshAgent>().hasPath)
//                posForMove.RemoveAt(0);
//        }
//    }

//    private bool CheckPointTrue(Vector3 curentPos, Vector3 posToDo)
//    {
//        if( (posToDo.x + 1 > curentPos.x) && (posToDo.x - 1 < curentPos.x) )
//        {
//            if( (posToDo.z + 1 > curentPos.z) && (posToDo.z - 1 < curentPos.z))
//            {
//                return true;
//            }
//        }
//        return false;
//    }
//}
