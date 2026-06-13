namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class FonteBiblica
{
    private FonteBiblica() { }

    public FonteBiblica(string referencia, string? textoVersiculo = null)
    {
        Referencia = referencia;
        TextoVersiculo = textoVersiculo;
    }

    public string Referencia { get; private set; } = string.Empty;
    public string? TextoVersiculo { get; private set; }

    public override bool Equals(object? obj) =>
        obj is FonteBiblica other && Referencia == other.Referencia;

    public override int GetHashCode() => Referencia.GetHashCode();
}
