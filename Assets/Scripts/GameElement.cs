using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElement : MonoBehaviour
{
    public GameObject[] tilesToPick;

    public GameObject doodle;

    public SpriteMask[] masks;

    public GameObject background;

    // In percent
    private float brownTileRNG = 15;

    private float blueTileRNG = 25;

    private float greenTileRNG = 60;

    private List<GameObject> tiles = new List<GameObject>();

    private float high = -1.5f;

    private float score = 0;

    private bool swapBackground = false;

    private float backgroundHeight;

    // Start is called before the first frame update
    void Start()
    {
        backgroundHeight = background.GetComponent<Renderer>().bounds.extents.y;
    }

    // Let's keep at least 15 tiles around doodle
    void TilesHandler()
    {
        System.Random rnd = new System.Random();
        while (tiles.Count < 15)
        {
            // Choosing which tile we pick
            int tileChoose = 0;
            int rng = rnd.Next(1, 100);
            Debug.Log (rng);
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

            // Creating it
            GameObject curr = GameObject.Instantiate(tilesToPick[tileChoose]);

            // Positionnig it well
            float x = (float)(rnd.NextDouble()) * (1.5f - (-1.5f)) + (-1.5f);
            curr.transform.position = new Vector3(x, high + 1f, 0f);

            curr.SetActive(true);

            SpriteRenderer spriteRenderer = curr.GetComponent<SpriteRenderer>();
            spriteRenderer.maskInteraction =
                SpriteMaskInteraction.VisibleOutsideMask;
            tiles.Add (curr);
            if (tileChoose != 0)
            {
                high += (float)(rnd.NextDouble()) * (0.95f - (0.8f)) + (0.8f);
            }
            else
            {
                high += (float)(rnd.NextDouble()) * (0.1f - (-0.1f)) + (-0.1f);
            }
        }

        if (doodle.transform.position.y - tiles[0].transform.position.y > 5.0f)
        {
            Destroy(tiles[0]);
            tiles.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // This could be the score
        score = Math.Max(score, doodle.transform.position.y);
        Debug.Log((int)(score * 100));

        //Handling Tiles Generation
        TilesHandler();

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
        Debug.Log(background.transform.position.y);
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
}
