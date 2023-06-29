using System.Text.Json;

namespace ReflexSmurf
{
    public class ScoreService
    {
        private readonly string filePath;

        public ScoreService()
        {
            filePath = Path.Combine(FileSystem.AppDataDirectory, "highscores.json");
        }

        public List<Score> LoadScores()
        {
            List<Score> highScores;

            if (!File.Exists(filePath))
            {
                highScores = new List<Score>();

                for (int i = 0; i < 10; i++)
                {
                    highScores.Add(new Score(99999, DateTime.Now));
                }

                File.WriteAllText(filePath, JsonSerializer.Serialize(highScores));
            }
            else
            {
                string json = File.ReadAllText(filePath);
                highScores = JsonSerializer.Deserialize<List<Score>>(json) ?? new List<Score>();
            }

            return highScores;
        }

        public void SaveScore(List<Score> highScores)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(highScores));
        }
    }
}
