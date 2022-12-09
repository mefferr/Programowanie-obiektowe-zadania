using static Tools;

abstract public class Zwierze
{
    private static int maxId = 0;
    private int id;
    private int wiek;
    private double waga;
    protected bool samiec;
    protected double apetyt = 0.05;
    private string gatunek;
    private string gromada;

    public Zwierze(int wiek, double waga, string gatunek, string gromada)
    {
        maxId += 1;
        id = maxId;
        samiec = RandBool();

        this.wiek = wiek;
        this.waga = waga;
        this.gatunek = gatunek;
        this.gromada = gromada;
    }

    public int GetWiek()
    {
        return wiek;
    }

    public double GetWaga()
    {
        return waga;
    }

    public double Nakarm()
    {
        return waga * apetyt;
    }

    public string Opis()
    {
        return string.Format("Gatunek: {0}, gromada: {1}, wiek: {2} lat, płeć: {3}, waga: {4:0.###} kg, ID: {5}", gatunek, gromada, wiek, Plec(), waga, id);
    }

    public string Plec()
    {
        return samiec ? "samiec" : "samica";
    }

    public override string ToString()
    {
        return Opis();
    }
}
