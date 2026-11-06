/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using RastreioLogistico.Models;

namespace RastreioLogistico.Services;

/// <summary>
/// Contrato do serviço que busca as informações de um pacote.
/// A ViewModel depende desta interface (injeção de dependência), e não da implementação.
/// </summary>
public interface IRastreioService
{
    /// <summary>Retorna o pacote ou null se o código não existir.</summary>
    Task<Pacote?> BuscarPacoteAsync(string codigo);

    /// <summary>Códigos fictícios disponíveis para teste.</summary>
    IReadOnlyList<string> CodigosDeExemplo { get; }
}
