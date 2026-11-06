/*
 * CBTPRDM - Trabalho Prático 03
 * Aplicação de Rastreamento Logístico com .NET MAUI e MVVM
 * Aluno: Alisson Ramos Aquino dos Santos - Prontuário: CB3032868
 * IFSP - Campus Cubatão - ADS
 */

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RastreioLogistico.ViewModels;

/// <summary>
/// Base de todas as ViewModels: implementa INotifyPropertyChanged para avisar a View
/// sempre que uma propriedade muda (Aula 06 - MVVM).
/// </summary>
public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool _isBusy;
    /// <summary>Indica processamento em andamento (exibe o ActivityIndicator).</summary>
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (SetProperty(ref _isBusy, value))
            {
                OnPropertyChanged(nameof(IsNotBusy));
                OnBusyChanged();
            }
        }
    }

    public bool IsNotBusy => !IsBusy;

    protected virtual void OnBusyChanged() { }

    protected bool SetProperty<T>(ref T campo, T valor, [CallerMemberName] string? nomePropriedade = null)
    {
        if (EqualityComparer<T>.Default.Equals(campo, valor))
            return false;

        campo = valor;
        OnPropertyChanged(nomePropriedade);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string? nomePropriedade = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
}
