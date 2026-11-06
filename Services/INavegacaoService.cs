/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using RastreioLogistico.Models;

namespace RastreioLogistico.Services;

/// <summary>
/// Abstrai a navegação para que a ViewModel não precise conhecer as páginas (Views).
/// </summary>
public interface INavegacaoService
{
    Task IrParaResultadoAsync(Pacote pacote);
    Task VoltarAsync();
}
