namespace Lugh.Maths
{
    public class ItemF
    {
        protected float Maximum      { get; set; }
        protected float Minimum      { get; set; }
        protected float RefillAmount { get; set; }

        public ItemF() : this( 0, 100, 0 )
        {
        }

        public ItemF( float minimum, float maximum ) : this( minimum, maximum, minimum )
        {
        }

        public ItemF( float maximum ) : this( 0, maximum, 0 )
        {
        }

        public ItemF( float minimum, float maximum, float total )
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

        public bool IsFull() => ( this.Total >= this.Maximum );

        public bool IsEmpty() => ( this.Total <= this.Minimum );

        public bool HasRoom() => !IsFull();

        public bool IsOverflowing() => this.Total > this.Maximum;

        public bool IsUnderflowing() => this.Total < this.Minimum;

        public void Refill()
        {
            this.Total = this.RefillAmount;
        }

        public void Refill( float refillAmount )
        {
            this.Total = refillAmount;
        }

        public float GetRefillAmount() => this.RefillAmount;

        public void SetRefillAmount( float refill )
        {
            this.RefillAmount = refill;
        }

        public float GetFreeSpace() => Math.Max( 0, Maximum - Total );

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
}