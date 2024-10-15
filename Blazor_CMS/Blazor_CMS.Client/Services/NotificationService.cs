
namespace Blazor_CMS.Client.Services
{
    public class NotificationService
    {
        public event Action<string> OnShow;

        public void ShowMessage(string message)
        {
            OnShow?.Invoke(message);
        }
    }

}
