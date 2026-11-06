/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using RastreioLogistico.Models;
using RastreioLogistico.Services;

namespace RastreioLogistico.ViewModels;

/// <summary>
/// VIEWMODEL da página de resultados: expõe os dados do pacote já formatados para a View
/// e permite atualizar o status (simulando o "tempo real") ou voltar para nova consulta.
/// </summary>
public class ResultadoViewModel : BaseViewModel
{
    private static readonly CultureInfo PtBr = new("pt-BR");

    private readonly IRastreioService _rastreioService;
    private readonly INavegacaoService _navegacao;
    private Pacote _pacote;

    public ResultadoViewModel(Pacote pacote, IRastreioService rastreioService, INavegacaoService navegacao)
    {
        _pacote = pacote;
        _rastreioService = rastreioService;
        _navegacao = navegacao;

        Historico = new ObservableCollection<EventoRastreio>(pacote.Historico);
        UltimaAtualizacao = DateTime.Now;

        AtualizarCommand = new Command(async () => await AtualizarAsync());
        NovaConsultaCommand = new Command(async () => await _navegacao.VoltarAsync());
    }

    // ----------------- Dados do pacote -----------------
    public string Codigo => _pacote.Codigo;
    public StatusPacote Status => _pacote.Status;
    public string StatusTexto => _pacote.Status.Descricao();
    public double Progresso => _pacote.Status.Progresso();

    public string DataEnvioTexto => _pacote.DataEnvio.ToString("dd/MM/yyyy 'às' HH:mm", PtBr);

    public string PrevisaoEntregaTexto => _pacote.DataPrevistaEntrega.ToString("ddd, dd/MM/yyyy", PtBr);

    public string PrazoTexto
    {
        get
        {
            if (_pacote.FoiEntregue && _pacote.DataEntrega is { } entrega)
                return $"Entregue em {entrega:dd/MM/yyyy} às {entrega:HH:mm}";

            var dias = (_pacote.DataPrevistaEntrega.Date - DateTime.Today).Days;
            return dias switch
            {
                < 0 => $"Atrasado há {-dias} dia(s)",
                0   => "Previsto para hoje",
                1   => "Previsto para amanhã",
                _   => $"Faltam {dias} dias"
            };
        }
    }

    public bool PrazoEmAlerta => _pacote.EstaAtrasado || _pacote.Status == StatusPacote.Atrasado;

    public string LocalizacaoAtual => _pacote.LocalizacaoAtual;
    public string Rota => $"{_pacote.Origem}  →  {_pacote.Destino}";
    public string Servico => _pacote.Servico;
    public string PesoTexto => _pacote.PesoKg.ToString("0.0 'kg'", PtBr);
    public string TotalEventosTexto => $"{Historico.Count} evento(s)";

    public ObservableCollection<EventoRastreio> Historico { get; }

    private DateTime _ultimaAtualizacao;
    public DateTime UltimaAtualizacao
    {
        get => _ultimaAtualizacao;
        set
        {
            if (SetProperty(ref _ultimaAtualizacao, value))
                OnPropertyChanged(nameof(UltimaAtualizacaoTexto));
        }
    }

    public string UltimaAtualizacaoTexto => $"Atualizado às {UltimaAtualizacao:HH:mm:ss}";

    private bool _isRefreshing;
    /// <summary>Ligado ao RefreshView (puxar para atualizar).</summary>
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }

    // ----------------- Comandos -----------------
    public ICommand AtualizarCommand { get; }
    public ICommand NovaConsultaCommand { get; }

    private async Task AtualizarAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            IsRefreshing = true;

            var atualizado = await _rastreioService.BuscarPacoteAsync(_pacote.Codigo);
            if (atualizado is not null)
            {
                _pacote = atualizado;

                Historico.Clear();
                foreach (var evento in atualizado.Historico)
                    Historico.Add(evento);

                OnPropertyChanged(string.Empty); // atualiza todos os bindings
            }

            UltimaAtualizacao = DateTime.Now;
        }
        finally
        {
            IsRefreshing = false;
            IsBusy = false;
        }
    }
}
