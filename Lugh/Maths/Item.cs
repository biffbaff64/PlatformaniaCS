
namespace Lugh.Maths;

public class Item
{
    protected int Maximum      { get; set; }
    protected int Minimum      { get; set; }
    protected int RefillAmount { get; set; }

    public Item() : this( 0, 100, 0 )
    {
    }

    public Item( int maximum ) : this( 0, maximum, 0 )
    {
    }

    public Item( int minimum, int maximum ) : this( minimum, maximum, minimum )
    {
    }
        
    public Item( int minimum, int maximum, int total )
    {
        Minimum      = minimum;
        Maximum      = maximum;
        Total        = total;
        RefillAmount = minimum;
    }

    private int _total;
    protected int Total
    {
        get
        {
            Validate();
            return _total;
        }
        set => _total = value;
    }

    public void Add( int amount )
    {
        if ( amount < 0 )
        {
            Subtract( amount );
        }
        else
        {
            Total = Math.Min( Maximum, Total + amount );
        }
    }

    public void Subtract( int amount )
    {
        Total = Math.Max( Minimum, Total - amount );
    }

    public void SetToMaximum()
    {
        Total = Maximum;
    }

    public void SetToMinimum()
    {
        Total = Minimum;
    }
        
    public bool IsFull()
    {
        return Total >= Maximum;
    }

    public bool IsEmpty()
    {
        return Total <= Minimum;
    }

    public bool HasRoom()
    {
        return !IsFull();
    }

    public bool IsOverflowing()
    {
        return Total > Maximum;
    }

    public bool IsUnderflowing()
    {
        return Total < Minimum;
    }

    public void Refill( int amount = 0 )
    {
        Total = ( amount == 0 ) ? RefillAmount : amount;
    }

    public int GetFreeSpace()
    {
        return Math.Max( 0, Maximum - Total );
    }

    public void BoostMax( int amount )
    {
        Maximum += amount;
    }

    private void Validate()
    {
        if ( this.Total < this.Minimum )
        {
            this.Total = this.Minimum;
        }

        if ( this.Total > this.Maximum )
        {
            this.Total = this.Maximum;
        }
    }
}