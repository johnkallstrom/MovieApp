using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace MovieApp.Web.Components.User
{
    public partial class Register
    {
        [CascadingParameter]
        public IModalService Modal { get; set; }

        protected override void OnInitialized()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            Modal.Show<RegisterModal>("Register User", options);
        }
    }
}
