using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GT
{
    public class HighScoreManager
    {
        private const string HIGH_SCORE_FILE_NAME = "HighScore.top";
        private const char HS_SEPARATOR = '*';
        private const int HIGH_SCORE_COUNT = 3;

        private DataManager dataManager = DataManager.Instance;

        public bool CompareWithHighScores(int score)
        {
            RetrieveHighScores();
            for (int i = 0; i < HIGH_SCORE_COUNT; i++)
            {
                if (score > dataManager.GetHighScores()[i].score)
                {
                    return true;
                }
            }
            return false;
        }

        public int InsertInHighScore(HighScore hs)
        {
            int plancingIndex = -1;
            RetrieveHighScores();
            for (int i = 0; i < dataManager.GetHighScores().Length; i++)
            {
                if (hs.score > dataManager.GetHighScores()[i].score)
                {
                    for (int j = dataManager.GetHighScores().Length - 1; j > i; j--)
                    {
                        dataManager.GetHighScores()[j] = dataManager.GetHighScores()[j - 1];
                    }

                    dataManager.GetHighScores()[i] = hs;
                    plancingIndex = i;
                    break;
                }
            }
            SaveHighScores();
            return plancingIndex;
        }

        private void RetrieveHighScores()
        {
            string content = ReadFile();

            List<string> hsTexts = new List<string>();

            int itCount = 0;

            foreach (string hsText in content.Split(HS_SEPARATOR))
            {
                dataManager.GetHighScores()[itCount] = HighScore.ToStruct(hsText);
                itCount++;
            }

        }

        private void SaveHighScores()
        {
            string chain = "";
            for (int i = 0; i < HIGH_SCORE_COUNT; i++)
            {
                chain += dataManager.GetHighScores()[i].ToString();

                if (i < HIGH_SCORE_COUNT - 1)
                {
                    chain += HS_SEPARATOR;
                }

            }
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
                File.WriteAllText(Path.Combine(Application.dataPath, HIGH_SCORE_FILE_NAME), chain);
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
                File.WriteAllText(Path.Combine(Application.dataPath, HIGH_SCORE_FILE_NAME), string.Empty);
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
                if (!File.Exists(Path.Combine(Application.dataPath, HIGH_SCORE_FILE_NAME)))
                {
                    CreateFile();
                }

                content = File.ReadAllText(Path.Combine(Application.dataPath, HIGH_SCORE_FILE_NAME));

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
                if (!File.Exists(Path.Combine(Application.dataPath, HIGH_SCORE_FILE_NAME)))
                {
                    CreateFile();
                }

                File.WriteAllText(Path.Combine(Application.dataPath, HIGH_SCORE_FILE_NAME), data);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("<HighScore> Error: " + ex.Message);
            }
        }

    }

}