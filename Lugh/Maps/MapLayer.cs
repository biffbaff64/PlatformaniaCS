// ##################################################

// ##################################################

namespace Lugh.Maps
{
    public class MapLayer
    {
        public string        Name       { get; set; } = string.Empty;
        public float         Opacity    { get; set; } = 1.0f;
        public bool          Visible    { get; set; } = true;
        public MapObjects    Objects    { get; set; } = new MapObjects();
        public MapProperties Properties { get; set; } = new MapProperties();

        private float    _offsetX;
        private float    _offsetY;
        private float    _renderOffsetX;
        private float    _renderOffsetY;
        private bool     _renderOffsetDirty = true;
        private MapLayer _parent;

        public float OffsetX
        {
            get => _offsetX;
            set
            {
                _offsetX = value;
                InvalidateRenderOffset();
            }
        }

        public float OffsetY
        {
            get => _offsetY;
            set
            {
                _offsetY = value;
                InvalidateRenderOffset();
            }
        }

        public float RenderOffsetX
        {
            get
            {
                if ( _renderOffsetDirty ) CalculateRenderOffsets();
                return _renderOffsetX;
            }
            set => _renderOffsetX = value;
        }

        public float RenderOffsetY
        {
            get
            {
                if ( _renderOffsetDirty ) CalculateRenderOffsets();
                return _renderOffsetY;
            }
            set => _renderOffsetY = value;
        }

        public MapLayer Parent
        {
            get => _parent;
            set
            {
                if ( value == this )
                {
                    throw new Exception( "Cannot set self as the parent!" );
                }

                _parent = value;
            }
        }
        public void InvalidateRenderOffset()
        {
            _renderOffsetDirty = true;
        }

        protected void CalculateRenderOffsets()
        {
            if ( _parent != null )
            {
                _parent.CalculateRenderOffsets();

                _renderOffsetX = _parent.RenderOffsetX + _offsetX;
                _renderOffsetY = _parent.RenderOffsetY + _offsetY;
            }
            else
            {
                _renderOffsetX = _offsetX;
                _renderOffsetY = _offsetY;
            }

            _renderOffsetDirty = false;
        }
    }
}
