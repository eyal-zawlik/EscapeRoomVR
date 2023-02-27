using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EscapeRoomAutoTest : MonoBehaviour
{
    [SerializeField] private PlayableDirector puzzle1, puzzle2, puzzle3, sceneTour;
    private List<Rect> _rectList = new List<Rect>();

    private void Awake()
    {
        for (var i = 0; i < 4; i++)
        {
            var rect = new Rect(new Vector2(i * 200f, 0), new Vector2(200, 50));
            _rectList.Add(rect);
        }
    }

    private void OnGUI()
    {
        if (!Application.isEditor) return;
        

        if (GUI.Button(_rectList[0],"Scene Tour"))
        {
            StopAll();
            sceneTour.Play();
        }
        if (GUI.Button(_rectList[1], "Puzzle 1"))
        {
            StopAll();
            puzzle1.Play();
        }
        if (GUI.Button(_rectList[2], "Puzzle 2"))
        {
            StopAll();
            puzzle2.Play();
        }
        if (GUI.Button(_rectList[3], "Puzzle 3"))
        {
            StopAll();
            puzzle3.Play();
        }
        
    }

    private void StopAll()
    {
        sceneTour.Stop();
        puzzle1.Stop();
        puzzle2.Stop();
        puzzle3.Stop();
    }
}
