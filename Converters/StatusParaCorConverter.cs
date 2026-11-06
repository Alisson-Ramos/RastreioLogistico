/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using System.Globalization;
using RastreioLogistico.Models;

namespace RastreioLogistico.Converters;

/// <summary>
/// Converte o StatusPacote em uma cor para os indicadores visuais da View.
/// (Lógica puramente visual: fica na camada de View, não na ViewModel.)
/// </summary>
public class StatusParaCorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is StatusPacote status ? status switch
        {
            StatusPacote.Entregue           => Color.FromArgb("#1B873F"),
            StatusPacote.SaiuParaEntrega    => Color.FromArgb("#0F6CBD"),
            StatusPacote.EmTransito         => Color.FromArgb("#E09B00"),
            StatusPacote.AguardandoRetirada => Color.FromArgb("#7A4DD8"),
            StatusPacote.Atrasado           => Color.FromArgb("#C62828"),
            _                               => Color.FromArgb("#6B7280"),
        } : Colors.Gray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

/// <summary>
/// Converte o StatusPacote em um ícone (emoji) para o histórico.
/// </summary>
public class StatusParaIconeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is StatusPacote status ? status switch
        {
            StatusPacote.Postado            => "📮",
            StatusPacote.EmTransito         => "🚚",
            StatusPacote.SaiuParaEntrega    => "🛵",
            StatusPacote.Entregue           => "✅",
            StatusPacote.AguardandoRetirada => "🏤",
            StatusPacote.Atrasado           => "⚠️",
            _ => "📦"
        } : "📦";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
