using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            await DisplayAlert("Game Over", $"Average Time: {averageTime:0.00} ms", "OK");
            await Navigation.PopAsync();

            return;
        }

        int delay = new Random().Next(2000, 4001); // Random delay between 2 and 4 seconds (in milliseconds)
        await Task.Delay(delay);

        var currentBar = _bars[_currentBarIndex];
        var currentStopwatch = _stopwatches[_currentBarIndex];

        currentStopwatch.Start();
        while (_isPlaying)
        {
            currentBar.HeightRequest += 1;
            await Task.Delay(10);
        }
        currentStopwatch.Stop();
        _isPlaying = true;
        _currentBarIndex++;

        await StartNextBar();
    }

    private bool IsGameOver() => _currentBarIndex >= _bars.Count;

    private async void StopButton_Clicked(object sender, EventArgs e)
    {
        _isPlaying = false;
        if (IsGameOver())
        {
            double averageTime = _stopwatches.Average(sw => sw.Elapsed.TotalMilliseconds);
            await DisplayAlert("Game Over", $"Average Time: {averageTime:0.00} ms", "OK");
            await Navigation.PopAsync();
        }
    }
}
