using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameElementController : MonoBehaviour
{
    public GameObject[] tilesToPick;

    public GameObject doodle;

    public SpriteMask[] masks;

    public GameObject background;

    public GameObject hole;

    public GameObject springPrefab;

    // In percent
    private float brownTileRNG = 15;

    private float blueTileRNG = 25;

    private float greenTileRNG = 60;

    private float objGenPb = 0.8f;

    public static int nb_tiles = 0;

    public List<GameObject> enemys = new List<GameObject>();

    public List<GameObject> enemysCurr = new List<GameObject>();

    private float high = -1.5f;

    private float score = 0;

    private bool swapBackground = false;

    private float backgroundHeight;

    private int enemyIndex;

    private bool side = true;

    // Start is called before the first frame update
    void Start()
    {
        backgroundHeight = background.GetComponent<Renderer>().bounds.extents.y;

        System.Random rnd = new System.Random();
        enemyIndex = rnd.Next(1, 5);

        hole = Instantiate(hole, new Vector3(0f, -10f, 0f), Quaternion.identity);
        foreach (var e in enemys)
        {
            enemysCurr.Add(Instantiate(e, new Vector3(0f, -10f, 0f), Quaternion.identity));
        }
    }

    // Let's keep at least 15 tiles around doodle
    void TilesHandler()
    {
        System.Random rnd = new System.Random();
        while (nb_tiles < 15)
        {
            // Choosing which tile we pick
            int tileChoose = 0;
            int rng = rnd.Next(1, 100);
            //Debug.Log (rng);
            if (rng < brownTileRNG)
            {
                tileChoose = 0;
            }
            else if ((rng >= brownTileRNG) && (rng < blueTileRNG + brownTileRNG)
            )
            {
                tileChoose = 1;
            }
            else
            {
                tileChoose = 2;
            }

            // Positionnig it well
            float x = (float)(rnd.NextDouble()) * (1.25f - (-1.25f)) + (-1.25f);
            Vector3 tile_pos = new Vector3(x, high + 0.7f, 0f);
            GameObject tile = Instantiate(tilesToPick[tileChoose], tile_pos, Quaternion.identity);

            // Generate objects like spring, propeller and jetpack on platform except brown tile
            if (tileChoose != 0 && Random.Range(0, 1) <= objGenPb)
            {
                Vector3 pos = tile_pos;
                pos.Set(pos.x, pos.y + 0.2f, pos.z);
                GameObject obj = Instantiate(springPrefab, pos, Quaternion.identity);
                obj.transform.parent = tile.transform;
            }

            if (tileChoose != 0)
            {
                high += (float)(rnd.NextDouble()) * (0.95f - (0.8f)) + (0.8f);
            }
            else
            {
                high += (float)(rnd.NextDouble()) * (0.1f - (-0.1f)) + (-0.1f);
            }

            nb_tiles += 1;
        }
    }

    bool IsGameObjectCollidedWithTiles(GameObject o) {
        GameObject[] gtiles = GameObject.FindGameObjectsWithTag("green_tile");
        GameObject[] bltiles = GameObject.FindGameObjectsWithTag("blue_tile");
        GameObject[] brtiles = GameObject.FindGameObjectsWithTag("brown_tile");
        GameObject[] tiles = ConcatArrays(gtiles, bltiles, brtiles);
        //GameObject[] tiles = GameObject.FindGameObjectsWithTag("white_tile");
        foreach (GameObject e in tiles)
        {
            if(Math.Abs(o.transform.position.x - e.transform.position.x) > 0.2f && Math.Abs(o.transform.position.y - e.transform.position.y) > 0.2f) {
                continue;
            } else {
                return true;
            }
        }

        return false;
    }

    void HoleHandler() {
        System.Random rnd = new System.Random();
        if(doodle.transform.position.y > hole.transform.position.y + 15f) {
            float x = (float)(rnd.NextDouble()) * (1.05f - (-1.05f)) + (-1.05f);
            hole.transform.position = new Vector3(x, doodle.transform.position.y + 4f, 0f);
            while(IsGameObjectCollidedWithTiles(hole)) {
                hole.transform.position = new Vector3(hole.transform.position.x - 0.05f, hole.transform.position.y + 0.2f, 0f);
            }
        }
    }

    void EnemysHandler() {
        //Debug.Log(enemysCurr[enemyIndex].transform.position);
        System.Random rnd = new System.Random();
        if(doodle.transform.position.y > enemysCurr[enemyIndex].transform.position.y + 12f) {
            float x = (float)(rnd.NextDouble()) * (1.05f - (-1.05f)) + (-1.05f);
            //pick an enemy
            enemyIndex = rnd.Next(1, 5);
            enemysCurr[enemyIndex].transform.position = new Vector3(x, doodle.transform.position.y + rnd.Next(4, 10), 0f);
            Debug.Log(enemysCurr[enemyIndex].transform.position);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // This could be the score
        score = Math.Max(score, doodle.transform.position.y);
        //Debug.Log((int)(score * 100));

        //Handling Tiles Generation
        TilesHandler();

        // Handling Hole
        if(nb_tiles != 0)
            HoleHandler();

        // Handling Enemys
        EnemysHandler();

        // Handling sprite masks by following doodle high
        foreach (var e in masks)
        {
            e.transform.position =
                new Vector3(e.transform.position.x,
                    doodle.transform.position.y,
                    e.transform.position.z);
        }

        // Handling Background overflow
        // HARDCODED CAMERA SIZE 3.071246
        //Debug.Log(background.transform.position.y);
        if (
            doodle.transform.position.y + 3.071246 - backgroundHeight >
            transform.position.y &&
            swapBackground
        )
        {
            background.transform.position =
                new Vector3(background.transform.position.x,
                    transform.position.y + backgroundHeight,
                    background.transform.position.z);
            swapBackground = !swapBackground;
        }
        else if (
            doodle.transform.position.y + 3.071246 - backgroundHeight >
            background.transform.position.y &&
            !swapBackground
        )
        {
            transform.position =
                new Vector3(transform.position.x,
                    background.transform.position.y + backgroundHeight,
                    transform.position.z);
            swapBackground = !swapBackground;
        }
    }
    public static void decreaseNbTiles()
    {
        nb_tiles -= 1;
    }
    public static T[] ConcatArrays<T>(params T[][] list)
    {
        var result = new T[list.Sum(a => a.Length)];
        int offset = 0;
        for (int x = 0; x < list.Length; x++)
        {
            list[x].CopyTo(result, offset);
            offset += list[x].Length;
        }
        return result;
    }
}
