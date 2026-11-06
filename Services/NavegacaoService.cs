/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using RastreioLogistico.Models;
using RastreioLogistico.ViewModels;
using RastreioLogistico.Views;

namespace RastreioLogistico.Services;

/// <summary>
/// Navegação hierárquica com NavigationPage (PushAsync / PopAsync), conforme a Aula 05.
/// </summary>
public class NavegacaoService : INavegacaoService
{
    private readonly IRastreioService _rastreioService;

    public NavegacaoService(IRastreioService rastreioService)
    {
        _rastreioService = rastreioService;
    }

    private static INavigation Navigation =>
        Application.Current?.Windows.FirstOrDefault()?.Page?.Navigation
        ?? throw new InvalidOperationException("Janela principal não encontrada.");

    public Task IrParaResultadoAsync(Pacote pacote)
    {
        var viewModel = new ResultadoViewModel(pacote, _rastreioService, this);
        return Navigation.PushAsync(new ResultadoPage(viewModel));
    }

    public Task VoltarAsync() => Navigation.PopAsync();
}
