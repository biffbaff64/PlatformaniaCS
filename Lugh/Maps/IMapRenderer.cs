namespace Lugh.Maps
{
    public interface IMapRenderer
    {
        /// <summary>
        /// Sets the projection matrix and viewbounds from the given camera. If the camera changes, you have to call this method again. The viewbounds are taken from the camera's position and viewport size as well as the scale. This method will only work if the camera's direction vector is (0,0,-1) and its up vector is (0, 1, 0), which are the defaults.
        /// </summary>
        void SetView( OrthographicCamera camera );

        /// <summary>
        /// Sets the projection matrix for rendering, as well as the bounds of the map which should be rendered. Make sure that the frustum spanned by the projection matrix coincides with the viewbounds.
        /// </summary>
        void SetView( Matrix4 projectionMatrix, float viewX, float viewY, float viewWidth, float viewHeight );

        /// <summary>
        /// Renders all the layers of a map.
        /// </summary>
        void Render();

        /// <summary>
        /// Renders the specified layers.
        /// </summary>
        void Render( int[] layers );
    }
}
