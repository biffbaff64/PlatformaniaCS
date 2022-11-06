﻿namespace Lugh.Utils;

// WIP - To replace Item and ItemF.
//     - Needs to cope with 'int' and 'float'. Any other
//     - types are invalid.
public class GameItem
{
    protected float Maximum      { get; set; }
    protected float Minimum      { get; set; }
    protected float RefillAmount { get; set; }

    public GameItem() : this( 0, 100, 0 )
    {
    }

    public GameItem( float maximum ) : this( 0, maximum, 0 )
    {
    }

    public GameItem( float minimum, float maximum ) : this( minimum, maximum, minimum )
    {
    }

    public GameItem( float minimum, float maximum, float total )
    {
        this.Minimum      = minimum;
        this.Maximum      = maximum;
        this.Total        = total;
        this.RefillAmount = minimum;
    }

    private float _total;
    protected float Total
    {
        get
        {
            Validate();
            return _total;
        }
        set => _total = value;
    }

    public void Add( float amount )
    {
        if ( ( this.Total += amount ) < 0 )
        {
            this.Total = 0;
        }
        else
        {
            if ( this.Total > this.Maximum )
            {
                this.Total = this.Maximum;
            }
        }
    }

    public void Add( float amount, float wrap )
    {
        if ( ( this.Total += amount ) > wrap )
        {
            this.Total = this.Minimum;
        }
    }

    public void Subtract( float amount )
    {
        this.Total = Math.Max( ( this.Total - amount ), this.Minimum );
    }

    public void Subtract( float amount, float wrap )
    {
        if ( ( this.Total -= amount ) < this.Minimum )
        {
            this.Total = wrap;
        }
    }

    public void SetMinMax( float minimum, float maximum )
    {
        this.Minimum = minimum;
        this.Maximum = maximum;
    }

    public void SetToMaximum()
    {
        this.Total = this.Maximum;
    }

    public void SetToMinimum()
    {
        this.Total = this.Minimum;
    }

    public bool IsFull()
    {
        return ( this.Total >= this.Maximum );
    }

    public bool IsEmpty()
    {
        return ( this.Total <= this.Minimum );
    }

    public bool HasRoom()
    {
        return !IsFull();
    }

    public bool IsOverflowing()
    {
        return this.Total > this.Maximum;
    }

    public bool IsUnderflowing()
    {
        return this.Total < this.Minimum;
    }

    public void Refill()
    {
        this.Total = this.RefillAmount;
    }

    public void Refill( float refillAmount )
    {
        this.Total = refillAmount;
    }

    public float GetRefillAmount()
    {
        return this.RefillAmount;
    }

    public void SetRefillAmount( float refill )
    {
        this.RefillAmount = refill;
    }

    public float GetFreeSpace()
    {
        return Math.Max( 0, Maximum - Total );
    }
    
    public void BoostMax( float amount )
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