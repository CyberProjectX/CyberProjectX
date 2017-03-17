namespace Scripts.Client.Contexts
{
    public abstract class ChangableContext
    {
        public bool IsContextChanged
        {
            get;
            private set;
        }

        protected void MarkAsChanged()
        {
            IsContextChanged = true;
        }

        public void MarkAsVisited()
        {
            IsContextChanged = false;
        }
    }
}
