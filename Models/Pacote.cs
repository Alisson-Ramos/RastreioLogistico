/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

namespace RastreioLogistico.Models;

/// <summary>
/// MODEL - Representa um pacote rastreado: ID, status, datas e histórico de eventos.
/// Não conhece a View nem a ViewModel.
/// </summary>
public class Pacote
{
    /// <summary>ID / código de rastreamento do pacote (ex.: BR123456789BR).</summary>
    public string Codigo { get; set; } = string.Empty;

    public StatusPacote Status { get; set; }

    public DateTime DataEnvio { get; set; }
    public DateTime DataPrevistaEntrega { get; set; }
    public DateTime? DataEntrega { get; set; }

    public string LocalizacaoAtual { get; set; } = string.Empty;
    public string Origem { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;
    public string Servico { get; set; } = string.Empty;
    public double PesoKg { get; set; }

    /// <summary>Histórico de eventos, do mais recente para o mais antigo.</summary>
    public List<EventoRastreio> Historico { get; set; } = new();

    // ----- Regras de negócio simples -----
    public bool FoiEntregue => Status == StatusPacote.Entregue;

    public bool EstaAtrasado =>
        !FoiEntregue && DateTime.Today > DataPrevistaEntrega.Date;
}
