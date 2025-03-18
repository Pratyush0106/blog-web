using System.Threading.Tasks;

namespace blogapi.Service
{
    public interface IContactFormService
    {
        Task<bool> SubmitContactFormAsync(ContactFormModel model);
    }
}