using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace Sample;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new MainViewModel();
    }
}

public class MainViewModel
{
    public List<object> Items { get; set; }

    public MainViewModel()
    {
        Items = new List<object>();
        Items.Add("Hello");
        Items.Add("World");
        Items.Add(DateTime.Today);

    }
}