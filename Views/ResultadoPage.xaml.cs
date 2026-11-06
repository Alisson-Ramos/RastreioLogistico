/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using RastreioLogistico.ViewModels;

namespace RastreioLogistico.Views;

public partial class ResultadoPage : ContentPage
{
    public ResultadoPage(ResultadoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
