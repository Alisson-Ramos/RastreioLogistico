/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using RastreioLogistico.ViewModels;

namespace RastreioLogistico.Views;

/// <summary>
/// Code-behind mínimo: apenas liga a View à sua ViewModel pelo BindingContext.
/// </summary>
public partial class RastreioPage : ContentPage
{
    public RastreioPage(RastreioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
