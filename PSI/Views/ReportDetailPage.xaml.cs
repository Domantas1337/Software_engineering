using PSI.ViewModels;

namespace PSI.Views;

public partial class ReportDetailPage : ContentPage
{
	public ReportDetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

		Debug.WriteLine("Time on ReportDetailPage: " + vm.currentTime);
	}

	public void OnReportScrolled(object sender, ScrolledEventArgs e)
	{
        Debug.WriteLine($"ScrollX: {e.ScrollX}, ScrollY: {e.ScrollY}");
    }
}