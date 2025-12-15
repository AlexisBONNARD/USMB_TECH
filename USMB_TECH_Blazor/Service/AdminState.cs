namespace USMB_TECH_Blazor.Service
{
    public class AdminState
    {
        private const string StorageKey = "isAdmin";

        public bool IsAdmin { get; private set; }

        public event Action? OnChange;

        public void SetAdmin(bool value)
        {
            IsAdmin = value;
            NotifyStateChanged();
        }

        public void EnableAdmin()
            => SetAdmin(true);

        public void DisableAdmin()
            => SetAdmin(false);

        private void NotifyStateChanged()
            => OnChange?.Invoke();
    }

}
