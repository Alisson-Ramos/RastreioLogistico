/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using RastreioLogistico.Services;

namespace RastreioLogistico.ViewModels;

/// <summary>
/// VIEWMODEL da página inicial: recebe o código digitado, valida, busca o pacote
/// no serviço e navega para a página de resultados.
/// </summary>
public partial class RastreioViewModel : BaseViewModel
{
    private readonly IRastreioService _rastreioService;
    private readonly INavegacaoService _navegacao;

    // Padrão de código de rastreamento: 2 letras + 9 dígitos + 2 letras (ex.: BR123456789BR)
    [GeneratedRegex(@"^[A-Z]{2}\d{9}[A-Z]{2}$")]
    private static partial Regex PadraoCodigo();

    public RastreioViewModel(IRastreioService rastreioService, INavegacaoService navegacao)
    {
        _rastreioService = rastreioService;
        _navegacao = navegacao;

        CodigosExemplo = new ObservableCollection<string>(_rastreioService.CodigosDeExemplo);
        PesquisasRecentes = new ObservableCollection<string>();
        PesquisasRecentes.CollectionChanged += (_, _) => OnPropertyChanged(nameof(TemPesquisasRecentes));

        RastrearCommand = new Command(async () => await RastrearAsync(), PodeRastrear);
        UsarCodigoCommand = new Command<string>(codigo => CodigoRastreio = codigo);
        LimparCommand = new Command(() => CodigoRastreio = string.Empty);
    }

    // ----------------- Propriedades ligadas à View -----------------

    private string _codigoRastreio = string.Empty;
    public string CodigoRastreio
    {
        get => _codigoRastreio;
        set
        {
            var normalizado = (value ?? string.Empty).Trim().ToUpperInvariant();
            if (SetProperty(ref _codigoRastreio, normalizado))
            {
                MensagemErro = string.Empty;
                ((Command)RastrearCommand).ChangeCanExecute();
            }
        }
    }

    private string _mensagemErro = string.Empty;
    public string MensagemErro
    {
        get => _mensagemErro;
        set
        {
            if (SetProperty(ref _mensagemErro, value))
                OnPropertyChanged(nameof(TemErro));
        }
    }

    public bool TemErro => !string.IsNullOrEmpty(MensagemErro);

    public ObservableCollection<string> CodigosExemplo { get; }
    public ObservableCollection<string> PesquisasRecentes { get; }
    public bool TemPesquisasRecentes => PesquisasRecentes.Count > 0;

    // ----------------- Comandos -----------------
    public ICommand RastrearCommand { get; }
    public ICommand UsarCodigoCommand { get; }
    public ICommand LimparCommand { get; }

    private bool PodeRastrear() => !IsBusy && !string.IsNullOrWhiteSpace(CodigoRastreio);

    protected override void OnBusyChanged() => ((Command)RastrearCommand).ChangeCanExecute();

    private async Task RastrearAsync()
    {
        if (!PadraoCodigo().IsMatch(CodigoRastreio))
        {
            MensagemErro = "Código inválido. Use o formato AA123456789AA (13 caracteres).";
            return;
        }

        try
        {
            IsBusy = true;
            MensagemErro = string.Empty;

            var pacote = await _rastreioService.BuscarPacoteAsync(CodigoRastreio);

            if (pacote is null)
            {
                MensagemErro = $"Nenhum pacote encontrado para o código {CodigoRastreio}.";
                return;
            }

            RegistrarPesquisa(pacote.Codigo);
            await _navegacao.IrParaResultadoAsync(pacote);
        }
        catch (Exception ex)
        {
            MensagemErro = $"Falha ao consultar o rastreamento: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void RegistrarPesquisa(string codigo)
    {
        if (PesquisasRecentes.Contains(codigo))
            PesquisasRecentes.Remove(codigo);

        PesquisasRecentes.Insert(0, codigo);

        while (PesquisasRecentes.Count > 5)
            PesquisasRecentes.RemoveAt(PesquisasRecentes.Count - 1);
    }
}
