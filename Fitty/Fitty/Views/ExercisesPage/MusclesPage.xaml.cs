using Fitty.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MusclesPage : ContentPage
	{
		MusclesViewModel _viewmodel;
		public MusclesPage()
		{
			InitializeComponent ();

			BindingContext = _viewmodel = new MusclesViewModel();
		}
	}
}