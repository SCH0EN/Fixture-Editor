using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InterdisciplinairProject.Views;
using System.Diagnostics;
using System.Windows.Controls;

namespace InterdisciplinairProject.ViewModels;

/// <summary>
/// Main ViewModel for the InterdisciplinairProject application.
/// </summary>
/// <remarks>
/// This ViewModel manages the state and commands for the main window, serving as the entry point for MVVM pattern.
/// It inherits from <see cref="ObservableObject"/> to enable property change notifications.
/// Properties and commands here can bind to UI elements in <see cref="MainWindow"/>.
/// Future extensions will include navigation to feature ViewModels (e.g., FixtureViewModel from Features).
/// </remarks>
/// <seealso cref="ObservableObject"/>
/// <seealso cref="MainWindow"/>
public partial class MainViewModel : ObservableObject
{
    /// <summary>
    /// Gets or sets the window title.
    /// </summary>
    [ObservableProperty]
    private string title = "InterdisciplinairProject - DMX Lighting Control";

    /// <summary>
    /// Gets or sets the current view displayed in the main window.
    /// </summary>
    [ObservableProperty]
    private UserControl? currentView;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    public MainViewModel()
    {
        Debug.WriteLine("[DEBUG] MainViewModel constructor called");

        // Initialize ViewModel, e.g., load services from DI if injected
        OpenFixtureSettingsCommand = new RelayCommand(OpenFixtureSettings);
        Debug.WriteLine("[DEBUG] MainViewModel initialized with OpenFixtureSettingsCommand");
    }

    /// <summary>
    /// Gets the command to open the fixture settings view.
    /// </summary>
    public RelayCommand OpenFixtureSettingsCommand { get; private set; }

    /// <summary>
    /// Opens the fixture settings view window.
    /// </summary>
    private void OpenFixtureSettings()
    {
        Debug.WriteLine("[DEBUG] OpenFixtureSettings() called - Fixture Settings button clicked");
        var fixtureSettingsView = new InterdisciplinairProject.Views.FixtureSettingsView();
        Debug.WriteLine("[DEBUG] FixtureSettingsView instance created");
        //fixtureSettingsView.Show();
        Debug.WriteLine("[DEBUG] FixtureSettingsView.Show() called - window should be visible now");
    }

    /// <summary>
    /// Opens the show builder view.
    /// </summary>
    [RelayCommand]
    private void OpenShowBuilder()
    {
        CurrentView = new ShowbuilderView();
    }

    /// <summary>
    /// Opens the scene builder view.
    /// </summary>
    [RelayCommand]
    private void OpenSceneBuilder()
    {
        CurrentView = new ScenebuilderView();
    }
}