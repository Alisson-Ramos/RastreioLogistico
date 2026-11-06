/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using Microsoft.Extensions.Logging;
using RastreioLogistico.Services;
using RastreioLogistico.ViewModels;
using RastreioLogistico.Views;

namespace RastreioLogistico;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        // Injeção de dependência (Aula 06)
        // Services
        builder.Services.AddSingleton<IRastreioService, RastreioServiceFake>();
        builder.Services.AddSingleton<INavegacaoService, NavegacaoService>();

        // ViewModels
        builder.Services.AddSingleton<RastreioViewModel>();

        // Views
        builder.Services.AddSingleton<RastreioPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
