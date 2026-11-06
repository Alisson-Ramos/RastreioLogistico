/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

namespace RastreioLogistico.Models;

/// <summary>
/// MODEL - Um evento do histórico de movimentação do pacote.
/// </summary>
public class EventoRastreio
{
    public DateTime DataHora { get; set; }
    public StatusPacote Status { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public string? Detalhe { get; set; }

    // Propriedades auxiliares somente leitura, usadas na lista do histórico
    public string DataTexto => DataHora.ToString("dd/MM/yyyy");
    public string HoraTexto => DataHora.ToString("HH:mm");
    public bool TemDetalhe => !string.IsNullOrWhiteSpace(Detalhe);
}
