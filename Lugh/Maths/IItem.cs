namespace Lugh.Maths
{
    public interface IItem
    {
        void Add( float amount );

        void Add( float amount, float wrap );

        void Subtract( float amount );

        void Subtract( float amount, float wrap );

        void SetMinMax( float minimum, float maximum );

        void SetToMaximum();

        void SetToMinimum();

        bool IsFull();

        bool IsEmpty();

        bool HasRoom();

        bool IsOverflowing();

        bool IsUnderflowing();

        void Refill();

        void Refill( float refillAmount );

        float GetRefillAmount();

        void SetRefillAmount( float refill );

        float GetFreeSpace();

        void BoostMax( float amount );

        void Validate();
    }
}