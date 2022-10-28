using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Object = UnityEngine.Object;

public class TopScores : MonoBehaviour
{
    private Transform container;

    private Transform template;

    public void Awake()
    {
        container = GameObject.FindWithTag("container").transform;
        template = GameObject.FindWithTag("template").transform;
        template.gameObject.SetActive(false);

        float templateHeight = 20f;

        List<playersScore> l = ScoreManagment.LoadFromJson();

        //l.Sort((x, y) => x.playerScore > y.playerScore ? x.playerScore : y.playerScore);
        List<playersScore> l_ordered =
            l.OrderByDescending(o => o.playerScore).ToList();

        int nbr = l.Count > 9 ? 9 : l.Count;
        for (int i = 0; i < nbr; i++)
        {
            Transform entryTransform = Instantiate(template, container);
            RectTransform rectTransform =
                entryTransform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition =
                new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);
            entryTransform
                .Find("playerName")
                .GetComponent<TextMeshProUGUI>()
                .text = l_ordered[i].playerName;
            entryTransform
                .Find("playerScore")
                .GetComponent<TextMeshProUGUI>()
                .text = l_ordered[i].playerScore + "";
        }
    }
}
