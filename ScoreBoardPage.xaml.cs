using System.Text.Json;

namespace ReflexSmurf;

public partial class ScoreBoardPage : ContentPage
{
	public ScoreBoardPage()
	{
		InitializeComponent();
        LoadScores();
    }

    private async void LoadScores()
    {
        const string filename = "scores.json";
        var scores = new List<Score>();

        if (File.Exists(filename))
        {
            var json = await File.ReadAllTextAsync(filename);
            scores = JsonSerializer.Deserialize<List<Score>>(json) ?? new List<Score>();
        }

        ScoreListView.ItemsSource = scores;
    }


}