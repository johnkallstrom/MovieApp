using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class DeleteListConfirmation
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        protected async Task HandleDeleteClick()
        {
            await ModalInstance.CloseAsync();
        }

        protected async Task HandleCancelAsync()
        {
            await ModalInstance.CancelAsync();
        }
    }
}
