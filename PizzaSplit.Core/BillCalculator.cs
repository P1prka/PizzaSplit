namespace PizzaSplit.Core;

public static class BillCalculator
{
    public static bool TryCalculate(
        decimal total,
        int people,
        bool addTip,
        out decimal share,
        out string error)
    {
        share = 0m;
        error = string.Empty;

        // Сумма должна быть больше 0 и не больше 10 000 €
        if (total <= 0m || total > 10_000m)
        {
            error = "Tellimuse summa peab olema üle 0 € ja kuni 10 000 €.";
            return false;
        }

        // Разрешено максимум 2 знака после запятой
        if (total != decimal.Round(total, 2))
        {
            error = "Tellimuse summal võib olla kuni kaks komakohta.";
            return false;
        }

        // Количество людей: от 1 до 20
        if (people < 1 || people > 20)
        {
            error = "Sööjate arv peab olema vahemikus 1–20.";
            return false;
        }

        decimal finalTotal = total;

        // Бонус: добавить 10% чаевых
        if (addTip)
        {
            finalTotal *= 1.10m;
        }

        // Сколько платит один человек
        share = decimal.Round(
            finalTotal / people,
            2,
            MidpointRounding.AwayFromZero);

        return true;
    }
}