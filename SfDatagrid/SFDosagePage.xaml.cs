using Syncfusion.Maui.DataGrid;


namespace SfDatagrid;

public partial class SFDosagePage : ContentPage
{
    public SFDosagePage()
    {
        InitializeComponent();
    }

    // This method is called when you click on a row in the DataGrid
    private void Datagrid_SelectionChanged(object sender, DataGridSelectionChangedEventArgs e)
    {
        // Make sure something was selected
        if (e.AddedRows?.Count > 0)
        {
            // Get the selected row as an Area object (your model)
            var selectedRow = e.AddedRows[0] as Area;

            // If we successfully cast to Area
            if (selectedRow != null && BindingContext is SFDosagePageViewModel vm)
            {
                // Get the row index from the ObservableCollection
                int rowIndex = vm.Areas.IndexOf(selectedRow);

                // If it exists in the list, update the Entry field by setting IndexText
                if (rowIndex >= 0)
                {
                    vm.IndexText = rowIndex.ToString();
                }
            }
        }
    }
}