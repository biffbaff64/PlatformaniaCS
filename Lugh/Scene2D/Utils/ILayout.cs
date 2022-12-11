namespace Scene2DCS.Utils
{
    /// <summary>
    /// Provides methods for an actor to participate in layout and to provide
    /// a minimum, preferred, and maximum size.
    /// </summary>
    public interface ILayout
    {
        /// <summary>
        /// Computes and caches any information needed for drawing and, if
        /// this actor has children, positions and sizes each child, calls
        /// <see cref="Invalidate"/> on any each child whose width or height
        /// has changed, and calls <see cref="Validate"/> on each child.
        /// This method should almost never be called directly, instead
        /// <see cref="Validate"/> should be used.
        /// </summary>
        public void Layout();

        /// <summary>
        /// Invalidates this actor's layout, causing <see cref="Layout"/> to
        /// happen the next time <see cref="Validate"/> is called. This method
        /// should be called when state changes in the actor that requires a
        /// layout but does not change the minimum, preferred, maximum, or actual
        /// size of the actor (meaning it does not affect the parent actor's layout).
        /// </summary>
        public void Invalidate();

        /// <summary>
        /// Invalidates this actor and its ascendants, calling <see cref="Invalidate"/>
        /// on each. This method should be called when state changes in the actor
        /// that affects the minimum, preferred, maximum, or actual size of the actor
        /// (meaning it potentially affects the parent actor's layout).
        /// </summary>
        public void InvalidateHierarchy();

        /// <summary>
        /// Ensures the actor has been laid out. Calls {@link #layout()} if {@link #invalidate()} has been called since the last time
        /// {@link #validate()} was called, or if the actor otherwise needs to be laid out. This method is usually called in
        /// {@link Actor#draw(Batch, float)} by the actor itself before drawing is performed.
        /// </summary>
        public void Validate();

        /// <summary>
        /// Sizes this actor to its preferred width and height, then calls {@link #validate()}.
        /// Generally this method should not be called in an actor's constructor because it calls {@link #layout()}, which means a
        /// subclass would have layout() called before the subclass' constructor. Instead, in constructors simply set the actor's size
        /// to {@link #getPrefWidth()} and {@link #getPrefHeight()}. This allows the actor to have a size at construction time for more
        /// convenient use with groups that do not layout their children.
        /// </summary>
        public void Pack();

        /// <summary>
        /// If true, this actor will be sized to the parent in {@link #validate()}. If the parent is the stage, the actor will be sized
        /// to the stage. This method is for convenience only when the widget's parent does not set the size of its children (such as
        /// the stage).
        /// </summary>
        public void SetFillParent( bool fillParent );

        /** Enables or disables the layout for this actor and all child actors, recursively. When false, {@link #validate()} will not
	 * cause a layout to occur. This can be useful when an actor will be manipulated externally, such as with actions. Default is
	 * true. */
        public void SetLayoutEnabled( bool enabled );

        public float GetMinWidth();

        public float GetMinHeight();

        public float GetPrefWidth();

        public float GetPrefHeight();

        /// <summary>
        /// Note: Zero indicates no max width.
        /// </summary>
        public float GetMaxWidth();

        /// <summary>
        /// Note: Zero indicates no max height.
        /// </summary>
        public float GetMaxHeight();
    }
}