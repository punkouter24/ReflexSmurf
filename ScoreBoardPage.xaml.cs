namespace ReflexSmurf;

public partial class ScoreBoardPage : ContentPage
{
    private readonly ScoreService scoreService = new();

    private List<Score> highScores;

    public ScoreBoardPage()
    {
        InitializeComponent();
        LoadScores();
    }

    private void LoadScores()
    {
        highScores = scoreService.LoadScores();
        highScores = highScores.OrderBy(s => s.Value).Take(10).ToList();

        foreach (var score in highScores)
        {
            // Create new row in the grid for each score
            ScoreGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            // Create and add labels for each property of the score
            var nameLabel = new Label { Text = score.UserName };
            Grid.SetRow(nameLabel, ScoreGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(nameLabel, 0);
            ScoreGrid.Children.Add(nameLabel);

            var valueLabel = new Label { Text = score.Value.ToString() };
            Grid.SetRow(valueLabel, ScoreGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(valueLabel, 1);
            ScoreGrid.Children.Add(valueLabel);

            var timestampLabel = new Label { Text = score.Timestamp.ToString() };
            Grid.SetRow(timestampLabel, ScoreGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(timestampLabel, 2);
            ScoreGrid.Children.Add(timestampLabel);
        }
    }
}
