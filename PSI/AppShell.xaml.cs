using PSI.Views;

namespace PSI;

public partial class AppShell : Shell
{

	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AddLocationView), typeof(AddLocationView));

    }
}
