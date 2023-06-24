using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace ReflexSmurf;

public partial class GamePage : ContentPage
{
    private List<BoxView> _bars;
    private List<Stopwatch> _stopwatches;
    private int _currentBarIndex;
    private bool _isPlaying;

    public GamePage()
    {
        InitializeComponent();
        InitializeGame();
    }

    private void InitializeGame()
    {
        _currentBarIndex = 0;
        _isPlaying = true;
        _bars = new List<BoxView>();
        _stopwatches = new List<Stopwatch>();

        for (int i = 0; i < 6; i++)
        {
            var bar = new BoxView { BackgroundColor = Colors.Orange, HeightRequest = 0, VerticalOptions = LayoutOptions.End, HorizontalOptions = LayoutOptions.FillAndExpand };
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


         //   ShowGameOverDialog();
            return;
        }

        int delay = new Random().Next(2000, 4001);
        await Task.Delay(delay);

        var currentBar = _bars[_currentBarIndex];
        var currentStopwatch = _stopwatches[_currentBarIndex];

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

    private bool IsGameOver() => _currentBarIndex >= _bars.Count;

    private async void StopButton_Clicked(object sender, EventArgs e)
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
         //   int averageTimeInt = Convert.ToInt32(1000 * Convert.ToDouble(averageTime));
          //  SaveScore(averageTimeInt); // Save score to storage
        }

          await DisplayAlert("Game Over", $"Average Time: {averageTime}", "OK");
        //await DisplayAlert("Game Over", $"Average Time: {averageTime}\nTotal Score: {totalScore}", "OK");
        await Navigation.PopAsync();
    }


    private async void SaveScore(int scoreValue)
    {
        const string filename = "scores.json";
        var scores = new List<Score>();

        if (File.Exists(filename))
        {
            var json = await File.ReadAllTextAsync(filename);
            scores = JsonSerializer.Deserialize<List<Score>>(json) ?? new List<Score>();
        }

        scores.Add(new Score(scoreValue, DateTime.Now));
        scores = scores
            .OrderByDescending(s => s.Value)
            .ThenBy(s => s.Timestamp)
            .Take(10)
            .ToList();

        var jsonString = JsonSerializer.Serialize(scores);
        await File.WriteAllTextAsync(filename, jsonString);
    }
}
