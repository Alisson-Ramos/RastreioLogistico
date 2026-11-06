/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

namespace RastreioLogistico.Models;

/// <summary>
/// MODEL - Situações possíveis de um pacote durante o transporte.
/// </summary>
public enum StatusPacote
{
    Postado,
    EmTransito,
    SaiuParaEntrega,
    Entregue,
    AguardandoRetirada,
    Atrasado
}

/// <summary>
/// Regras de negócio ligadas ao status (texto exibido e etapa da entrega).
/// </summary>
public static class StatusPacoteExtensions
{
    public static string Descricao(this StatusPacote status) => status switch
    {
        StatusPacote.Postado            => "Objeto postado",
        StatusPacote.EmTransito         => "Em trânsito",
        StatusPacote.SaiuParaEntrega    => "Saiu para entrega",
        StatusPacote.Entregue           => "Entregue",
        StatusPacote.AguardandoRetirada => "Aguardando retirada",
        StatusPacote.Atrasado           => "Atrasado",
        _ => status.ToString()
    };

    /// <summary>Percentual do caminho já percorrido (0 a 1), usado na barra de progresso.</summary>
    public static double Progresso(this StatusPacote status) => status switch
    {
        StatusPacote.Postado            => 0.15,
        StatusPacote.EmTransito         => 0.50,
        StatusPacote.Atrasado           => 0.50,
        StatusPacote.SaiuParaEntrega    => 0.85,
        StatusPacote.AguardandoRetirada => 0.90,
        StatusPacote.Entregue           => 1.00,
        _ => 0
    };
}
