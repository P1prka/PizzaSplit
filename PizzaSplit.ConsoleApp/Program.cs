using System.Globalization;
using System.Text;
using PizzaSplit.Core;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

CultureInfo culture = CultureInfo.GetCultureInfo("et-EE");

Console.Write("Sisesta tellimuse summa eurodes: ");
string? totalText = Console.ReadLine()?.Trim();

if (string.IsNullOrWhiteSpace(totalText))
{
    Console.WriteLine("Viga: tellimuse summa on sisestamata.");
    return;
}

if (!decimal.TryParse(
        totalText,
        NumberStyles.Number,
        culture,
        out decimal total))
{
    Console.WriteLine("Viga: tellimuse summa peab olema arv.");
    return;
}

Console.Write("Sisesta sööjate arv: ");
string? peopleText = Console.ReadLine()?.Trim();

if (string.IsNullOrWhiteSpace(peopleText))
{
    Console.WriteLine("Viga: sööjate arv on sisestamata.");
    return;
}

if (!int.TryParse(
        peopleText,
        NumberStyles.Integer,
        culture,
        out int people))
{
    Console.WriteLine("Viga: sööjate arv peab olema täisarv.");
    return;
}

Console.Write("Kas lisada 10% jootraha? (j/e, Enter = ei): ");
string? tipText = Console.ReadLine()?.Trim();

bool addTip = string.Equals(
    tipText,
    "j",
    StringComparison.OrdinalIgnoreCase);

if (BillCalculator.TryCalculate(
    total,
    people,
    addTip,
    out decimal share,
    out string error))
{
    Console.WriteLine(
        $"Mina maksan {share.ToString("F2", culture)} €");
}
else
{
    Console.WriteLine($"Viga: {error}");
}