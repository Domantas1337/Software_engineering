#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif


namespace PSI;

public partial class App : Application
{
    const int WindowWidth = 400;
    const int WindowHeight = 812;

    public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
    }

    protected override void OnStart()
    {
        base.OnStart();
    }
}
