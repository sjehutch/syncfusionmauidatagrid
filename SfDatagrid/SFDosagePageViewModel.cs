using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SfDatagrid;

// Area model stays the same
public class Area
{
    public string AreaName { get; set; }
    public double AreaVolume { get; set; }
    public double EstimatedHalfLossTime { get; set; }
    public double ExposureTime { get; set; }
    public bool FumigantRequired { get; set; }
    public double Temperature { get; set; }
    public double UserDefinedCT { get; set; }
    public double TargetCT { get; set; }
    public double InitialConcentration { get; set; }
}

// ViewModel now uses ObservableObject to support binding
public partial class SFDosagePageViewModel : ObservableObject
{
    // Collection of Area rows
    [ObservableProperty]
    private ObservableCollection<Area> areas = new();

    // User-entered index in Entry field (as string for easier binding)
    [ObservableProperty]
    private string indexText;
    

    public SFDosagePageViewModel()
    {
        Areas = new ObservableCollection<Area>
        {
            new Area
            {
                AreaName = "Test",
                AreaVolume = 283.16846,
                EstimatedHalfLossTime = 12.0,
                ExposureTime = 24.0,
                FumigantRequired = true,
                Temperature = 32.22,
                UserDefinedCT = 0.0,
                TargetCT = 286.0367,
                InitialConcentration = 22.0295
            }
        };
    }

    // RelayCommand for adding a new row
    [RelayCommand]
    private void AddRow()
    {
        var area = new Area
        {
            AreaName = "New Area",
            AreaVolume = 100,
            EstimatedHalfLossTime = 10,
            ExposureTime = 5,
            FumigantRequired = false,
            Temperature = 25,
            UserDefinedCT = 1,
            TargetCT = 200,
            InitialConcentration = 20
        };

        if (int.TryParse(IndexText, out int index) && index >= 0 && index <= Areas.Count)
        {
            Areas.Insert(index, area);
        }
        else
        {
            Areas.Add(area); // fallback to append
        }

        IndexText = string.Empty; // clear entry field
    }

    // RelayCommand for deleting a row
    [RelayCommand]
    private async Task DeleteRowAsync()
    {
        // Parse the index first
        if (int.TryParse(IndexText, out int index) &&
            index >= 0 && index < Areas.Count)
        {
            var area = Areas[index];

            // Ask the user if they really want to delete it
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirm Delete",
                $"Delete \"{area.AreaName}\"?",
                "Delete",
                "Cancel");

            if (confirm)
            {
                Areas.RemoveAt(index);
            }
        }

        IndexText = string.Empty; // always clear the entry box
    }

}
