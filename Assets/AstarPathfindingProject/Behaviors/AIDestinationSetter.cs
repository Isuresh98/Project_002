using UnityEngine;
using System.Collections;
using Pathfinding;

[UniqueComponent(tag = "ai.destination")]
public class AIDestinationSetter : VersionedMonoBehaviour
{
    public Transform target;
    public float activeDistance = 15f; // new variable for active distance
    private IAstarAI ai;
    private Animator enemyAnima;

    private void Start()
    {
        enemyAnima = GetComponent<Animator>();

    }

    void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
        if (ai != null) ai.onSearchPath += Update;
    }

    void OnDisable()
    {
        if (ai != null) ai.onSearchPath -= Update;
    }

    void Update()
    {
        enemyAnima.SetBool("isAttact", false);

        if (target != null && ai != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < activeDistance)
            {

                ai.destination = target.position;
                enemyAnima.SetBool("isAttact", true);

            }
        }
    }
}
