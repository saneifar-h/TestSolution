using Microsoft.AspNetCore.Components;

namespace TestSolution.Client.Pages.ChatPage
{
    public interface IChatView : IComponent
    {
        string BaseUri { get; }
        void ChangeState();
    }

    public partial class ChatView : IChatView
    {
        public ChatViewModel ViewModel;
        [Inject] public NavigationManager NavigationManager { get; set; }
        public string BaseUri => NavigationManager.BaseUri;

        public void ChangeState()
        {
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ViewModel ??= new ChatViewModel(this);
        }
    }
}