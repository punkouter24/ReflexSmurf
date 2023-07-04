using System.Diagnostics;

namespace ReflexSmurf;

public partial class GamePage : ContentPage
{
    private readonly ScoreService scoreService = new();

    private List<BoxView> _bars;
    private List<Stopwatch> _stopwatches;
    private List<Score> highScores;

    private int _currentBarIndex;
    private bool _isPlaying;

    public GamePage()
    {
        InitializeComponent();
        InitializeGame();
    }

    [Obsolete]
    private void InitializeGame()
    {
        _currentBarIndex = 0;
        _isPlaying = true;
        _bars = new List<BoxView>();
        _stopwatches = new List<Stopwatch>();

        for (int i = 0; i < 6; i++)
        {
            BoxView bar = new() { BackgroundColor = Colors.Orange, HeightRequest = 0, VerticalOptions = LayoutOptions.End, HorizontalOptions = LayoutOptions.FillAndExpand };
            Grid.SetColumn(bar, i);
            BarGrid.Children.Add(bar);
            _bars.Add(bar);
            _stopwatches.Add(new Stopwatch());
        }

        _ = StartNextBar();
    }

    private async Task StartNextBar()
    {
        if (IsGameOver())
        {
            double averageTime = _stopwatches.Average(sw => sw.Elapsed.TotalMilliseconds);
            ShowGameOverDialog($"{averageTime:0.00}");
            return;
        }

        int delay = new Random().Next(2000, 4001);
        await Task.Delay(delay);

        BoxView currentBar = _bars[_currentBarIndex];
        Stopwatch currentStopwatch = _stopwatches[_currentBarIndex];

        currentStopwatch.Start();

        while (_isPlaying)
        {
            currentBar.HeightRequest += 5; // Increase growth speed
            await Task.Delay(10);

            if (currentBar.HeightRequest >= BarGrid.Height) // Check if bar reached the top
            {
                _isPlaying = false;
                ShowGameOverDialog();
                return;
            }
        }

        currentStopwatch.Stop();
        _isPlaying = true;
        _currentBarIndex++;

        await StartNextBar();
    }

    private bool IsGameOver()
    {
        return _currentBarIndex >= _bars.Count;
    }

    private void StopButton_Clicked(object sender, EventArgs e)
    {
        if (!_stopwatches[_currentBarIndex].IsRunning && _stopwatches[_currentBarIndex].Elapsed.TotalMilliseconds == 0)
        {
            ShowGameOverDialog(); // Show the game over dialog with "N/A" as default
            return;
        }

        _isPlaying = false;

        if (IsGameOver())
        {
            double averageTime = _stopwatches.Average(sw => sw.Elapsed.TotalMilliseconds);
            ShowGameOverDialog($"{averageTime:0.00} ms");
        }
    }

    private async void ShowGameOverDialog(string averageTime = "N/A")
    {
        if (averageTime != "N/A")
        {
            int averageTimeInt = Convert.ToInt32(100 * Convert.ToDouble(averageTime));
            highScores = scoreService.LoadScores();
            //get username from static class
            string userName = UserData.UserName;
            highScores.Add(new Score(userName, averageTimeInt, DateTime.Now));
            scoreService.SaveScore(highScores);
        }

        await DisplayAlert("Game Over", $"Average Time: {averageTime}", "OK");
        _ = await Navigation.PopAsync();
    }
}
