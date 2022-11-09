using PSI.ViewModels;

namespace PSI.Views;

public partial class ReportDetailPage : ContentPage
{

    private int timePassed = 0;
    public ReportDetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;


        System.Timers.Timer aTimer = new System.Timers.Timer();
        aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
        aTimer.Interval = 5000;
        aTimer.Enabled = true;

        Debug.WriteLine("Time on ReportDetailPage: " + vm.currentTime);
	}

	public void OnReportScrolled(object sender, ScrolledEventArgs e)
	{

       

        Debug.WriteLine($"ScrollX: {e.ScrollX}, ScrollY: {e.ScrollY}");
    }

    public void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
    {
        timePassed += 5;
        
        Debug.WriteLine("Time passed: " + timePassed);
    }
}