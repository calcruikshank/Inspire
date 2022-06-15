using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointBetweenPlayers : MonoBehaviour
{
    public List<Transform> players;
    Vector3 pointToFollow;
    public float furthestDistanceBetweenPlayer;
    public float[] numsToChooseFrom;
    public static PointBetweenPlayers singleton;

    float startingSize;

    private void Awake()
    {
        singleton = this;
        startingSize = Camera.main.orthographicSize;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (players.Count == 1)
        {
            pointToFollow = players[0].transform.position;
            this.transform.position = new Vector3(pointToFollow.x, pointToFollow.y);
            return;
        }
        foreach (Transform player in players)
        {
            pointToFollow += player.transform.position;
            if (players.Count - 1 != numsToChooseFrom.Length)
            {
                numsToChooseFrom = new float[players.Count - 1];
            }
        }

        for (int i = 0; i < players.Count - 1; i++)
        {
            float distanceBetweenPlayer = Vector3.Distance(players[i].transform.position, players[i + 1].transform.position);
            numsToChooseFrom[i] = distanceBetweenPlayer;
            furthestDistanceBetweenPlayer = distanceBetweenPlayer;
            for (int j = 0; j < numsToChooseFrom.Length; j++)
            {
                if (numsToChooseFrom[j] > furthestDistanceBetweenPlayer)
                {
                    furthestDistanceBetweenPlayer = numsToChooseFrom[j];
                }
            }

        }


        if (players.Count > 1)
        {
            pointToFollow = pointToFollow / (players.Count + 1);

        }


        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pointToFollow.x, pointToFollow.y), 50 * Time.deltaTime);

        if (furthestDistanceBetweenPlayer > startingSize)
        {
            Camera.main.orthographicSize = furthestDistanceBetweenPlayer;
        }
        else
        {
            Camera.main.orthographicSize = startingSize;
        }
    }

    internal void SetTarget(Transform transform)
    {
        players.Add(transform);
    }
}
