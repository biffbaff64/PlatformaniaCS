namespace Scene2DCS
{
    public enum Touchable
    {
        /// <summary>
        /// All touch input events will be received by
        /// the actor and any children.
        /// </summary>
        Enabled,

        /// <summary>
        /// No touch input events will be received by
        /// the actor or any children.
        /// </summary>
        Disabled,
        
        /// <summary>
        /// No touch input events will be received by the actor,
        /// but children will still receive events.
        /// Note that events on the children will still bubble
        /// to the parent.
        /// </summary>
        ChildrenOnly,
    }
}