using System;
using System.Globalization;

public class Fracao : IEquatable<Fracao>, IComparable<Fracao>
{
    public int Numerador { get; private set; }
    public int Denominador { get; private set; }

    public Fracao(int numerador, int denominador)
    {
        if (denominador == 0)
            throw new ArgumentOutOfRangeException(nameof(denominador));
        if (denominador < 0)
        {
            numerador = -numerador;
            denominador = -denominador;
        }
        int mdc = MDC(Math.Abs(numerador), Math.Abs(denominador));
        Numerador = numerador / mdc;
        Denominador = denominador / mdc;
    }

    public Fracao(int inteiro) : this(inteiro, 1) { }

    public Fracao(string texto)
    {
        var partes = texto.Split('/');
        int n = int.Parse(partes[0].Trim());
        int d = int.Parse(partes[1].Trim());
        if (d == 0)
            throw new ArgumentOutOfRangeException(nameof(denominador));
        if (d < 0)
        {
            n = -n;
            d = -d;
        }
        int mdc = MDC(Math.Abs(n), Math.Abs(d));
        Numerador = n / mdc;
        Denominador = d / mdc;
    }

    public Fracao(double valor)
    {
        if (double.IsNaN(valor) || double.IsInfinity(valor))
            throw new ArgumentOutOfRangeException(nameof(valor));
        const int maxDen = 1000000;
        int sign = Math.Sign(valor);
        valor = Math.Abs(valor);
        int bestDen = 1;
        int bestNum = (int)Math.Round(valor);
        double bestError = Math.Abs(valor - bestNum);
        for (int d = 1; d <= maxDen && bestError > 0; d++)
        {
            int n = (int)Math.Round(valor * d);
            double err = Math.Abs(valor - (double)n / d);
            if (err < bestError)
            {
                bestError = err;
                bestNum = n;
                bestDen = d;
                if (err < 1e-10) break;
            }
        }
        int mdc = MDC(bestNum, bestDen);
        Numerador = sign * bestNum / mdc;
        Denominador = bestDen / mdc;
    }

    public override string ToString() => $"{Numerador}/{Denominador}";

    public static Fracao operator +(Fracao f, int n) => f.Somar(new Fracao(n));
    public static Fracao operator +(Fracao f, double n) => f.Somar(new Fracao(n));
    public static Fracao operator +(Fracao f, string s) => f.Somar(new Fracao(s));
    public static Fracao operator +(Fracao a, Fracao b) => a.Somar(b);

    public Fracao Somar(int n) => Somar(new Fracao(n));
    public Fracao Somar(double n) => Somar(new Fracao(n));
    public Fracao Somar(string s) => Somar(new Fracao(s));
    public Fracao Somar(Fracao outra)
    {
        int m = MMC(Denominador, outra.Denominador);
        int num = Numerador * (m / Denominador) + outra.Numerador * (m / outra.Denominador);
        return new Fracao(num, m);
    }

    public bool Equals(Fracao? other)
    {
        if (other is null) return false;
        return Numerador == other.Numerador && Denominador == other.Denominador;
    }

    public override bool Equals(object? obj) => Equals(obj as Fracao);
    public override int GetHashCode() => HashCode.Combine(Numerador, Denominador);

    public static bool operator ==(Fracao a, Fracao b) => a.Equals(b);
    public static bool operator !=(Fracao a, Fracao b) => !a.Equals(b);

    public int CompareTo(Fracao? other)
    {
        if (other == null) return 1;
        long left = (long)Numerador * other.Denominador;
        long right = (long)other.Numerador * Denominador;
        return left.CompareTo(right);
    }

    public static bool operator >(Fracao a, Fracao b) => a.CompareTo(b) > 0;
    public static bool operator <(Fracao a, Fracao b) => a.CompareTo(b) < 0;
    public static bool operator >=(Fracao a, Fracao b) => a.CompareTo(b) >= 0;
    public static bool operator <=(Fracao a, Fracao b) => a.CompareTo(b) <= 0;

    public bool IsImpropria => Math.Abs(Numerador) >= Denominador;
    public bool IsPropria => !IsImpropria;
    public bool IsAparente => Numerador % Denominador == 0;
    public bool IsUnitaria => Math.Abs(Numerador) == 1 && Denominador != 0;

    private static int MDC(int a, int b)
    {
        while (b != 0)
        {
            int t = b;
            b = a % b;
            a = t;
        }
        return a == 0 ? 1 : a;
    }

    private static int MMC(int a, int b) => a / MDC(a, b) * b;
}
