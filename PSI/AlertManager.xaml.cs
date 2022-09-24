using CommunityToolkit.Maui.Views;

namespace PSI;

public partial class AlertManager : Popup
{
	public AlertManager()
	{
		InitializeComponent();
		SelectionState.utilityState = UtilityState.Taromat;
	}
}