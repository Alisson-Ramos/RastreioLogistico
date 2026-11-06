/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using RastreioLogistico.Models;

namespace RastreioLogistico.Services;

/// <summary>
/// Implementação SIMULADA do serviço de rastreamento, com dados fictícios.
/// As datas são calculadas a partir de "hoje" para que os exemplos pareçam sempre atuais.
/// Em produção, bastaria criar outra classe que chame a API real da transportadora.
/// </summary>
public class RastreioServiceFake : IRastreioService
{
    private readonly Dictionary<string, Func<Pacote>> _base;

    public RastreioServiceFake()
    {
        _base = new(StringComparer.OrdinalIgnoreCase)
        {
            ["BR123456789BR"] = CriarEmTransito,
            ["LG202611020BR"] = CriarSaiuParaEntrega,
            ["LG000000001BR"] = CriarEntregue,
            ["LG987654321BR"] = CriarAtrasado,
        };
    }

    public IReadOnlyList<string> CodigosDeExemplo => _base.Keys.ToList();

    public async Task<Pacote?> BuscarPacoteAsync(string codigo)
    {
        // Simula a latência de uma chamada de rede
        await Task.Delay(1200);

        if (string.IsNullOrWhiteSpace(codigo))
            return null;

        return _base.TryGetValue(codigo.Trim(), out var fabrica) ? fabrica() : null;
    }

    // ------------------------------------------------------------------
    // Dados fictícios
    // ------------------------------------------------------------------
    private static DateTime Dia(int deslocamento, int hora, int minuto) =>
        DateTime.Today.AddDays(deslocamento).AddHours(hora).AddMinutes(minuto);

    private static Pacote CriarEmTransito() => new()
    {
        Codigo = "BR123456789BR",
        Status = StatusPacote.EmTransito,
        DataEnvio = Dia(-3, 9, 12),
        DataPrevistaEntrega = Dia(2, 0, 0),
        LocalizacaoAtual = "Centro de Distribuição – Campinas/SP",
        Origem = "Manaus/AM",
        Destino = "Cubatão/SP",
        Servico = "Expresso",
        PesoKg = 1.8,
        Historico = new()
        {
            new() { DataHora = Dia(0, 6, 40),  Status = StatusPacote.EmTransito, Descricao = "Objeto em trânsito", Local = "Campinas/SP", Detalhe = "De Centro de Distribuição Campinas para Unidade de Tratamento Santos" },
            new() { DataHora = Dia(-1, 22, 5), Status = StatusPacote.EmTransito, Descricao = "Objeto chegou ao centro de distribuição", Local = "Campinas/SP" },
            new() { DataHora = Dia(-2, 14, 30),Status = StatusPacote.EmTransito, Descricao = "Objeto em trânsito", Local = "Manaus/AM", Detalhe = "Transferência aérea para Campinas/SP" },
            new() { DataHora = Dia(-3, 9, 12), Status = StatusPacote.Postado,    Descricao = "Objeto postado", Local = "Agência Centro – Manaus/AM" },
        }
    };

    private static Pacote CriarSaiuParaEntrega() => new()
    {
        Codigo = "LG202611020BR",
        Status = StatusPacote.SaiuParaEntrega,
        DataEnvio = Dia(-2, 10, 0),
        DataPrevistaEntrega = Dia(0, 0, 0),
        LocalizacaoAtual = "Em rota de entrega – Cubatão/SP",
        Origem = "São Paulo/SP",
        Destino = "Cubatão/SP",
        Servico = "Econômico",
        PesoKg = 0.6,
        Historico = new()
        {
            new() { DataHora = Dia(0, 8, 15),  Status = StatusPacote.SaiuParaEntrega, Descricao = "Objeto saiu para entrega ao destinatário", Local = "Cubatão/SP", Detalhe = "Entregador: rota 07 – Vila Nova" },
            new() { DataHora = Dia(0, 5, 50),  Status = StatusPacote.EmTransito,      Descricao = "Objeto chegou à unidade de distribuição", Local = "Cubatão/SP" },
            new() { DataHora = Dia(-1, 19, 20),Status = StatusPacote.EmTransito,      Descricao = "Objeto em trânsito", Local = "São Paulo/SP", Detalhe = "Para Unidade de Distribuição Cubatão/SP" },
            new() { DataHora = Dia(-2, 10, 0), Status = StatusPacote.Postado,         Descricao = "Objeto postado", Local = "Agência Pinheiros – São Paulo/SP" },
        }
    };

    private static Pacote CriarEntregue() => new()
    {
        Codigo = "LG000000001BR",
        Status = StatusPacote.Entregue,
        DataEnvio = Dia(-8, 11, 30),
        DataPrevistaEntrega = Dia(-3, 0, 0),
        DataEntrega = Dia(-4, 15, 42),
        LocalizacaoAtual = "Entregue ao destinatário – Santos/SP",
        Origem = "Curitiba/PR",
        Destino = "Santos/SP",
        Servico = "Expresso",
        PesoKg = 3.2,
        Historico = new()
        {
            new() { DataHora = Dia(-4, 15, 42), Status = StatusPacote.Entregue,        Descricao = "Objeto entregue ao destinatário", Local = "Santos/SP", Detalhe = "Recebido por: Portaria" },
            new() { DataHora = Dia(-4, 8, 3),   Status = StatusPacote.SaiuParaEntrega, Descricao = "Objeto saiu para entrega ao destinatário", Local = "Santos/SP" },
            new() { DataHora = Dia(-5, 21, 10), Status = StatusPacote.EmTransito,      Descricao = "Objeto chegou à unidade de distribuição", Local = "Santos/SP" },
            new() { DataHora = Dia(-7, 7, 25),  Status = StatusPacote.EmTransito,      Descricao = "Objeto em trânsito", Local = "Curitiba/PR", Detalhe = "Transferência rodoviária para Santos/SP" },
            new() { DataHora = Dia(-8, 11, 30), Status = StatusPacote.Postado,         Descricao = "Objeto postado", Local = "Agência Batel – Curitiba/PR" },
        }
    };

    private static Pacote CriarAtrasado() => new()
    {
        Codigo = "LG987654321BR",
        Status = StatusPacote.Atrasado,
        DataEnvio = Dia(-9, 16, 0),
        DataPrevistaEntrega = Dia(-1, 0, 0),
        LocalizacaoAtual = "Unidade de Tratamento – Belo Horizonte/MG",
        Origem = "Recife/PE",
        Destino = "Cubatão/SP",
        Servico = "Econômico",
        PesoKg = 5.4,
        Historico = new()
        {
            new() { DataHora = Dia(-1, 13, 0),  Status = StatusPacote.Atrasado,   Descricao = "Atraso na entrega", Local = "Belo Horizonte/MG", Detalhe = "Fluxo operacional acima do normal. Nova previsão será informada." },
            new() { DataHora = Dia(-4, 2, 45),  Status = StatusPacote.EmTransito, Descricao = "Objeto chegou à unidade de tratamento", Local = "Belo Horizonte/MG" },
            new() { DataHora = Dia(-7, 18, 30), Status = StatusPacote.EmTransito, Descricao = "Objeto em trânsito", Local = "Recife/PE", Detalhe = "Transferência rodoviária para Belo Horizonte/MG" },
            new() { DataHora = Dia(-9, 16, 0),  Status = StatusPacote.Postado,    Descricao = "Objeto postado", Local = "Agência Boa Viagem – Recife/PE" },
        }
    };
}
