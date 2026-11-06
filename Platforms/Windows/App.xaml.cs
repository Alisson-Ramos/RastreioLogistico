// CBTPRDM - Trabalho Prático 03 | Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
using Microsoft.UI.Xaml;

namespace RastreioLogistico.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
	public App()
	{
		this.InitializeComponent();
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
