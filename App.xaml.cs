/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using Microsoft.Extensions.DependencyInjection;
using RastreioLogistico.Views;

namespace RastreioLogistico;

public partial class App : Application
{
    private readonly IServiceProvider _services;

    public App(IServiceProvider services)
    {
        InitializeComponent();
        _services = services;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // NavigationPage = navegação hierárquica (pilha de páginas) - Aula 05
        // A página é resolvida aqui (depois de InitializeComponent) para que os
        // recursos de App.xaml (cores e estilos) já estejam carregados.
        var paginaInicial = _services.GetRequiredService<RastreioPage>();

        var janela = new Window(new NavigationPage(paginaInicial))
        {
            Title = "Rastreio Logístico"
        };

#if WINDOWS
        // No Windows, abre a janela com proporção de celular
        janela.Width = 430;
        janela.Height = 860;
#endif
        return janela;
    }
}
