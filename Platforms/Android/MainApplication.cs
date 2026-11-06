// CBTPRDM - Trabalho Prático 03 | Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
using Android.App;
using Android.Runtime;

namespace RastreioLogistico;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
