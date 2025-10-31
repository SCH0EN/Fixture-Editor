﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using InterdisciplinairProject.Fixtures.Models;
using System.Linq;

namespace InterdisciplinairProject.Fixtures.ViewModels
{
    public partial class ChannelViewModel : ObservableObject
    {

        // --- INTERACTIE EIGENSCHAPPEN (Gebruikt door FixtureCreateView.xaml.cs) ---
        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool isExpanded;

        // --- MODEL & WRAPPERS ---
        private Channel _model;

        // Wrapper voor Model.Name
        public string Name
        {
            get => _model.Name;
            set => SetProperty(_model.Name, value, _model, (m, v) => m.Name = v);
        }

        // Wrapper voor Model.Value (De Parameter is de string die wordt opgeslagen in JSON)
        public string? Parameter
        {
            get => _model.Value;
            set => SetProperty(_model.Value, value, _model, (m, v) => m.Value = v);
        }

        // --- TYPE SELECTIE & WAARDE EIGENSCHAPPEN ---

        [ObservableProperty]
        private ObservableCollection<string> availableTypes = new()
        {
            "Lamp", "Ster", "Klok", "Tilt", "Ventilator", "Rood", "Groen", "Blauw", "Wit",
        };

        [ObservableProperty]
        private string selectedType; // Bindt aan de ComboBox

        [ObservableProperty]
        private int level = 0; // Bindt aan de Slider (0-255)

        // --- CONSTRUCTOR ---
        public ChannelViewModel(Channel model)
        {
            _model = model;
            selectedType = _model.Type;

            // Initialisatie van Level op basis van modelwaarde
            if (int.TryParse(_model.Value, out int currentLevel))
            {
                Level = currentLevel;
            }
        }

        // --- MVVM SYNCHRONISATIE METHODEN ---

        // Wordt automatisch aangeroepen wanneer SelectedType wijzigt
        partial void OnSelectedTypeChanged(string value)
        {
            _model.Type = value;
            Level = 0; // Reset level bij typeverandering

            // Forceer Level om de Parameter/Value te schrijven als het een 'level' type is
            if (new[] { "Rood", "Groen", "Blauw", "Wit" }.Contains(value))
            {
                OnLevelChanged(Level);
            }
        }

        // Wordt automatisch aangeroepen wanneer Level wijzigt
        partial void OnLevelChanged(int value)
        {
            // Cruciale fix: zet de int Level om naar string voor opslag in _model.Value
            _model.Value = value.ToString();

            // Notificatie om UI elementen die aan Parameter zijn gebonden, bij te werken (bijv. TextBox)
            OnPropertyChanged(nameof(Parameter));
        }
    }
}