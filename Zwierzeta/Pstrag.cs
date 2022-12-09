using static Tools;

class Pstrag : Zwierze
{
    public Pstrag() : base(RandomInt(2, 12), RandomDouble(5, 12), "pstrąg", "ryba")
    {
        if (samiec)
        {
            apetyt = 0.10;
        }
    }
}
