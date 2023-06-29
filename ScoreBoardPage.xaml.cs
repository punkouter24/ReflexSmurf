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
        ScoreListView.ItemsSource = highScores.OrderBy(s => s.Value).Take(10);
    }
}