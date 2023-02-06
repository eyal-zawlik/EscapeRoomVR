using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ScoreSystem))]
    public class ScoreSystemEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = (ScoreSystem)target;

            if (GUILayout.Button("Add TEST Score"))
            {
                script.AddScoreBox(new ScoreRecord
                {
                    PlayerName = "TEST ",
                    Score = Random.Range(1, 300),
                });
            }

            if (GUILayout.Button("Clear"))
            {
                script.Clear();
            }

            if (GUILayout.Button("Test Database Leaderboard"))
            {
                var list = new List<ScoreRecord>();
                for (var i = 0; i < 5; i++)
                {
                    list.Add(new ScoreRecord
                    {
                        PlayerName = "TEST " + i,
                        Score = Random.Range(1, 300),
                    });
                    Debug.Log(list[i].ToJsonString());
                }

                var leaderBoard = new ScoreLeaderBoard
                {
                    Records = list.ToArray()
                };
                script._leaderBoard = leaderBoard;
                script.Save();
                Debug.Log(leaderBoard.ToJsonString());
            }

            if (GUILayout.Button("Load Data"))
            {
                script.Load();
            }
        }
    }
}