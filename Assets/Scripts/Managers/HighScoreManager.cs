using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GT
{
    public class HighScoreManager : MonoBehaviour
    {
        private const string HIGH_SCORE_FILE_NAME = "HighScore.top";

        private const char HS_SEPARATOR = '*';

        private const int HIGH_SCORE_COUNT = 3;

        private HighScore[] highScores = new HighScore[HIGH_SCORE_COUNT];

        string path = "";

        private void Awake()
        {
            path = Path.Combine(Application.dataPath, HIGH_SCORE_FILE_NAME);
        }
        private void Start()
        {
            Debug.Log(ReadFile());
            HighScore highScore;
            highScore.name = "GTB";
            highScore.score = 10000000;
            if(CompareWithHighScores(highScore.score))
            {
                InsertInHighScore(highScore);
            }
            Debug.Log(ReadFile());
        }

        public HighScore[] GetHighScores() => highScores;

        public bool CompareWithHighScores(int score)
        {
            RetrieveHighScores();
            for (int i = 0; i < HIGH_SCORE_COUNT; i++)
            {
                if (score > highScores[i].score)
                {
                    return true;
                }
            }
            return false;
        }

        public void InsertInHighScore(HighScore hs)
        {
            RetrieveHighScores();
            for (int i = 0; i < highScores.Length; i++)
            {
                if (hs.score > highScores[i].score)
                {
                    for (int j = highScores.Length - 1; j > i; j--)
                    {
                        highScores[j] = highScores[j - 1];
                    }

                    highScores[i] = hs;
                    break;
                }
            }
            SaveHighScores();
        }

        private void RetrieveHighScores()
        {
            string content = ReadFile();
            List<string> hsTexts = new List<string>();

            int itCount = 0;

            foreach (string hsText in content.Split(HS_SEPARATOR))
            {
                highScores[itCount] = HighScore.ToStruct(hsText);
                itCount++;
            }

        }

        private void SaveHighScores()
        {
            string chain = "";
            for (int i = 0; i < HIGH_SCORE_COUNT; i++)
            {
                chain += highScores[i].ToString();

                if (i < HIGH_SCORE_COUNT - 1)
                {
                    chain += HS_SEPARATOR;
                }

            }
            //Debug.LogWarning("El output del escore, a guardar en el archivo es: " + chain);
            EraseFileContent();
            WriteFile(chain);
        }


        private void CreateFile()
        {
            string chain = "";
            for (int i = 0; i < HIGH_SCORE_COUNT; i++)
            {

                chain += HighScore.Default().ToString();

                if (i < HIGH_SCORE_COUNT - 1)
                {
                    chain += HS_SEPARATOR;
                }

            }
            try
            {
                File.WriteAllText(path, chain);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("<HighScore> Error: " + ex.Message);
            }
        }

        private void EraseFileContent()
        {
            try
            {
                File.WriteAllText(path, string.Empty);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("<HighScore> Error: " + ex.Message);
            }
        }

        private string ReadFile()
        {
            string content = "";
            try
            {
                if (!File.Exists(path))
                {
                    Debug.LogWarning("The file does not exist. A new one will be created.");
                    CreateFile();
                }

                content = File.ReadAllText(path);

                if (content == "")
                {
                    throw new System.Exception("The file is empty");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("<HighScore> Error: " + ex.Message);
            }

            return content;
        }

        private void WriteFile(string data)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Debug.LogWarning("The file does not exist. A new one will be created.");
                    CreateFile();
                }

                File.WriteAllText(path, data);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("<HighScore> Error: " + ex.Message);
            }
        }

    }

}