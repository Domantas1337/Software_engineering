using PSI.ViewModels;

namespace PSI.Views;

public partial class ReportDetailPage : ContentPage
{
	public ReportDetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	public void OnReportScrolled(object sender, ScrolledEventArgs e)
	{
        Console.WriteLine($"ScrollX: {e.ScrollX}, ScrollY: {e.ScrollY}");
    }
}